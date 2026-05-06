using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DreamGuard
{
    /// <summary>
    /// Persists the Player (camera rig, OVRManager, and all OVRPassthroughLayer
    /// instances) across scene loads, per Meta's recommendation for multi-scene
    /// MR projects.
    ///
    /// Attach to the root Player GameObject. Only the first instance survives;
    /// any duplicate loaded by a subsequent scene is immediately destroyed.
    ///
    /// To disable passthrough in a full-VR scene, call from a scene startup script:
    ///     DreamGuardPlayer.Instance.EnablePassthrough(false);
    /// Re-enable when returning to an MR scene:
    ///     DreamGuardPlayer.Instance.EnablePassthrough(true);
    /// </summary>
    [DisallowMultipleComponent]
    public class DreamGuardPlayer : MonoBehaviour
    {
        public static DreamGuardPlayer Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                DreamGuardLog.LogWarning("[DreamGuardPlayer] Duplicate instance — destroying this one");
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DreamGuardLog.Log("[DreamGuardPlayer] Awake — persisting across scenes");
        }

        // The base passthrough layer is always kept active so the Meta compositor never
        // enters the red/green error state.  When no passthrough technique is selected,
        // camera.backgroundColor.a=1 makes the VR scene render opaque over the underlay,
        // so the passthrough feed is invisible even though the layer is enabled.
        private OVRPassthroughLayer _basePassthroughLayer;

        private void Start()
        {
            DreamGuardLog.Log("[DreamGuardPlayer] Start");
            SceneManager.sceneLoaded += OnSceneLoaded;
            OVRManager.InputFocusAcquired += OnOVRInputFocusAcquired;
            OVRManager.InputFocusLost     += OnOVRInputFocusLost;
            OVRManager.eyeFovPremultipliedAlphaModeEnabled = false;
            DreamGuardLog.Log("[DreamGuardPlayer] eyeFovPremultipliedAlphaModeEnabled → false");

            // The Meta compositor requires at least one active OVRPassthroughLayer whenever
            // passthrough is initialised (declared Required in the Android manifest via
            // OculusProjectConfig._insightPassthroughSupport=2).  Without one the compositor
            // shows the red/green error pattern regardless of any other setting.
            // Create a permanent underlay layer here so the compositor is always satisfied.
            // Visibility is controlled via textureOpacity:
            //   0 → passthrough feed invisible (VR mode, dungeon renders normally)
            //   1 → passthrough feed visible through background pixels (MR mode)
            EnsureBasePassthroughLayer();

            SetPassthroughBackground(false);
            LogAllCameras("Start");
        }

        private void EnsureBasePassthroughLayer()
        {
            // Reuse an existing layer on this GameObject if one was added in the prefab.
            _basePassthroughLayer = GetComponentInChildren<OVRPassthroughLayer>(includeInactive: true);
            if (_basePassthroughLayer == null)
            {
                var go = new GameObject("PassthroughBase");
                go.transform.SetParent(transform, worldPositionStays: false);
                _basePassthroughLayer = go.AddComponent<OVRPassthroughLayer>();
            }
            _basePassthroughLayer.overlayType    = OVROverlay.OverlayType.Underlay;
            _basePassthroughLayer.textureOpacity = 0f; // hidden in VR mode; set to 1 for MR
            _basePassthroughLayer.enabled        = true;
            _basePassthroughLayer.passthroughLayerResumed.AddListener(OnBasePassthroughLayerResumed);
            DreamGuardLog.Log($"[DreamGuardPlayer] Base passthrough layer ready: '{_basePassthroughLayer.gameObject.name}' textureOpacity=0");
        }

        private void OnBasePassthroughLayerResumed(OVRPassthroughLayer layer)
        {
            DreamGuardLog.LogWarning("[DreamGuardPlayer] PassthroughBase passthroughLayerResumed fired — re-applying VR background and logging all cameras");
            LogAllCameras("LayerResumed");
            SetPassthroughBackground(_passthroughBgActive);
        }

        private void LogAllCameras(string context)
        {
            var allCams = Camera.allCameras;
            DreamGuardLog.LogWarning($"[DreamGuardPlayer] LogAllCameras({context}) — {allCams.Length} camera(s)");
            foreach (var c in allCams)
            {
                DreamGuardLog.LogWarning($"[DreamGuardPlayer]   cam='{c.name}'  tag='{c.tag}'  " +
                    $"clearFlags={c.clearFlags}  bgAlpha={c.backgroundColor.a:F2}  " +
                    $"enabled={c.enabled}  depth={c.depth}");
            }
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            OVRManager.InputFocusAcquired -= OnOVRInputFocusAcquired;
            OVRManager.InputFocusLost     -= OnOVRInputFocusLost;
            if (_basePassthroughLayer != null)
                _basePassthroughLayer.passthroughLayerResumed.RemoveListener(OnBasePassthroughLayerResumed);
            if (Instance == this)
                Instance = null;
        }

        // ── OVR focus events ─────────────────────────────────────────────────────

        private void OnOVRInputFocusAcquired()
        {
            DreamGuardLog.LogWarning("[DreamGuardPlayer] OVRManager.InputFocusAcquired — logging all passthrough layer states");
            if (_allLayers == null) return;
            foreach (var layer in _allLayers)
            {
                if (layer == null) continue;
                DreamGuardLog.LogWarning($"[DreamGuardPlayer]   Layer '{layer.gameObject.name}': " +
                    $"goActive={layer.gameObject.activeSelf}  enabled={layer.enabled}  " +
                    $"overlay={layer.overlayType}  projection={layer.projectionSurfaceType}");
            }
            // Ensure the base layer is always enabled after focus returns.
            if (_basePassthroughLayer != null && !_basePassthroughLayer.enabled)
            {
                DreamGuardLog.LogWarning("[DreamGuardPlayer] Re-enabling base passthrough layer after focus acquisition");
                _basePassthroughLayer.enabled = true;
            }
        }

        private void OnOVRInputFocusLost()
        {
            DreamGuardLog.Log("[DreamGuardPlayer] OVRManager.InputFocusLost");
        }

        // ── OVR state monitoring ─────────────────────────────────────────────────

        private bool _depthKeywordWasActive = false;
        private bool _ovrManagerWasEnabled = true;
        private bool _passthroughInitializedWas = false;
        private float _globalScanTimer = 0f;

        private bool _passthroughBgActive = false;

        // Per-layer state cache: keyed by instance ID so list order doesn't matter.
        private readonly Dictionary<int, bool> _layerEnabledCache  = new();
        private readonly Dictionary<int, bool> _layerGoActiveCache = new();
        private OVRPassthroughLayer[] _allLayers;

        private void Update()
        {
            // ── Depth keywords ────────────────────────────────────────────────
            bool soft = Shader.IsKeywordEnabled("META_DEPTH_SOFT_OCCLUSION_ENABLED");
            bool hard = Shader.IsKeywordEnabled("META_DEPTH_HARD_OCCLUSION_ENABLED");
            bool depthActive = soft || hard;
            if (depthActive != _depthKeywordWasActive)
            {
                if (depthActive)
                    DreamGuardLog.LogWarning($"[DreamGuardPlayer] Depth keyword became ACTIVE: " +
                        $"SOFT={soft} HARD={hard}  scene={SceneManager.GetActiveScene().name}");
                else
                    DreamGuardLog.LogWarning($"[DreamGuardPlayer] Depth keyword became INACTIVE");
            }
            _depthKeywordWasActive = depthActive;

            // ── Passthrough initialization state ─────────────────────────────
            bool ptInit = OVRManager.IsInsightPassthroughInitialized();
            if (ptInit != _passthroughInitializedWas)
            {
                DreamGuardLog.LogWarning($"[DreamGuardPlayer] IsInsightPassthroughInitialized changed: " +
                    $"{_passthroughInitializedWas} → {ptInit}");
                _passthroughInitializedWas = ptInit;
                // Log all layer states at the moment passthrough initializes — this is when
                // the OVR SDK resumes layers and is the most likely trigger for the RGT bug.
                DumpAllKnownLayerStates("PassthroughInit");
            }

            // ── OVRManager enabled ────────────────────────────────────────────
            bool ovrEnabled = OVRManager.instance != null && OVRManager.instance.enabled;
            if (ovrEnabled != _ovrManagerWasEnabled)
            {
                DreamGuardLog.LogWarning($"[DreamGuardPlayer] OVRManager.enabled changed: " +
                    $"{_ovrManagerWasEnabled} → {ovrEnabled}");
                _ovrManagerWasEnabled = ovrEnabled;
            }

            // ── Tracked passthrough layer states ──────────────────────────────
            // Rebuild list on first call (layers are on the persistent Player GO).
            if (_allLayers == null)
            {
                _allLayers = GetComponentsInChildren<OVRPassthroughLayer>(includeInactive: true);
                DreamGuardLog.Log($"[DreamGuardPlayer] Monitoring {_allLayers.Length} OVRPassthroughLayer(s)");
                foreach (var l in _allLayers)
                {
                    int id = l.GetInstanceID();
                    bool goActive = l.gameObject.activeSelf;
                    DreamGuardLog.Log($"[DreamGuardPlayer]   Layer '{l.gameObject.name}': " +
                        $"goActive={goActive}  enabled={l.enabled}  " +
                        $"overlay={l.overlayType}  projection={l.projectionSurfaceType}");
                    _layerEnabledCache[id]  = l.enabled;
                    _layerGoActiveCache[id] = goActive;
                }
            }

            foreach (var layer in _allLayers)
            {
                if (layer == null) continue;
                int id       = l_id(layer);
                bool nowEnabled  = layer.enabled;
                bool nowGoActive = layer.gameObject.activeSelf;

                bool prevEnabled  = _layerEnabledCache.TryGetValue(id, out var e) ? e : nowEnabled;
                bool prevGoActive = _layerGoActiveCache.TryGetValue(id, out var g) ? g : nowGoActive;

                if (nowEnabled != prevEnabled || nowGoActive != prevGoActive)
                {
                    DreamGuardLog.LogWarning($"[DreamGuardPlayer] Layer CHANGED: " +
                        $"'{layer.gameObject.name}'  " +
                        $"goActive={prevGoActive}→{nowGoActive}  " +
                        $"enabled={prevEnabled}→{nowEnabled}  " +
                        $"SOFT={Shader.IsKeywordEnabled("META_DEPTH_SOFT_OCCLUSION_ENABLED")}  " +
                        $"HARD={Shader.IsKeywordEnabled("META_DEPTH_HARD_OCCLUSION_ENABLED")}");
                    _layerEnabledCache[id]  = nowEnabled;
                    _layerGoActiveCache[id] = nowGoActive;
                }
            }

            // ── Global scan for unexpected OVRPassthroughLayers ───────────────
            // Run every 2 s to detect SDK-created layers not in our tracked set.
            _globalScanTimer += Time.deltaTime;
            if (_globalScanTimer >= 2f)
            {
                _globalScanTimer = 0f;
                var all = FindObjectsByType<OVRPassthroughLayer>(
                    FindObjectsInactive.Include, FindObjectsSortMode.None);
                int trackedCount = _allLayers?.Length ?? 0;
                if (all.Length != trackedCount)
                {
                    DreamGuardLog.LogWarning($"[DreamGuardPlayer] GLOBAL SCAN: found {all.Length} OVRPassthroughLayer(s) vs {trackedCount} tracked");
                    foreach (var l in all)
                        DreamGuardLog.LogWarning($"[DreamGuardPlayer]   [{l.GetInstanceID()}] '{l.gameObject.name}' " +
                            $"goActive={l.gameObject.activeSelf}  enabled={l.enabled}  " +
                            $"overlay={l.overlayType}  projection={l.projectionSurfaceType}");
                }
            }

        }

        private void DumpAllKnownLayerStates(string reason)
        {
            var cam = Camera.main;
            DreamGuardLog.LogWarning($"[DreamGuardPlayer] DumpLayerStates({reason}) " +
                $"cam.clearFlags={cam?.clearFlags}  cam.bgAlpha={cam?.backgroundColor.a:F2}  " +
                $"ptInit={OVRManager.IsInsightPassthroughInitialized()}  " +
                $"SOFT={Shader.IsKeywordEnabled("META_DEPTH_SOFT_OCCLUSION_ENABLED")}  " +
                $"HARD={Shader.IsKeywordEnabled("META_DEPTH_HARD_OCCLUSION_ENABLED")}");
            if (_allLayers == null) return;
            foreach (var layer in _allLayers)
            {
                if (layer == null) continue;
                DreamGuardLog.LogWarning($"[DreamGuardPlayer]   Layer '{layer.gameObject.name}': " +
                    $"goActive={layer.gameObject.activeSelf}  enabled={layer.enabled}  " +
                    $"overlay={layer.overlayType}  projection={layer.projectionSurfaceType}");
            }
        }

        private static int l_id(OVRPassthroughLayer l) => l.GetInstanceID();

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            DreamGuardLog.Log($"[DreamGuardPlayer] OnSceneLoaded  scene='{scene.name}'  mode={mode}  " +
                $"SOFT={Shader.IsKeywordEnabled("META_DEPTH_SOFT_OCCLUSION_ENABLED")}  " +
                $"HARD={Shader.IsKeywordEnabled("META_DEPTH_HARD_OCCLUSION_ENABLED")}");
            SetPassthroughBackground(false);
            var spawn = FindFirstObjectByType<DreamGuardSpawnPoint>();
            if (spawn == null)
            {
                DreamGuardLog.Log($"[DreamGuardPlayer] No DreamGuardSpawnPoint in '{scene.name}' — position unchanged");
                return;
            }

            // Apply position and Y-rotation only — never pitch or roll the player.
            transform.position = spawn.transform.position;
            var euler = transform.eulerAngles;
            euler.y = spawn.transform.eulerAngles.y;
            transform.eulerAngles = euler;
        }

        /// <summary>
        /// Enable or disable all technique-specific OVRPassthroughLayer components on this Player.
        /// The base passthrough layer is never disabled — it must remain active so the Meta
        /// compositor does not enter the red/green error state.
        /// For full-VR mode call <see cref="SetPassthroughBackground"/> with <c>false</c> instead:
        /// that keeps the layer active but hides it behind an opaque VR background (bgAlpha=1).
        /// </summary>
        public void EnablePassthrough(bool enable)
        {
            foreach (var layer in GetComponentsInChildren<OVRPassthroughLayer>(includeInactive: true))
            {
                if (layer == _basePassthroughLayer) continue; // never disable the base layer
                layer.enabled = enable;
            }
        }

        /// <summary>
        /// Switch between full-VR and passthrough-background modes.
        ///
        /// Pass <c>true</c>  (MR mode)  — sets bgAlpha=0, so background pixels reveal the
        /// passthrough underlay.  VR geometry still renders opaque on top.
        /// Pass <c>false</c> (VR mode)  — sets bgAlpha=1, so the camera background is a
        /// solid colour and the passthrough underlay is hidden behind it.
        /// The base OVRPassthroughLayer remains enabled in both modes.
        /// </summary>
        public void SetPassthroughBackground(bool passthroughActive)
        {
            _passthroughBgActive = passthroughActive;

            if (_basePassthroughLayer != null)
            {
                _basePassthroughLayer.textureOpacity = passthroughActive ? 1f : 0f;
                DreamGuardLog.Log($"[DreamGuardPlayer] SetPassthroughBackground({passthroughActive}) → textureOpacity={_basePassthroughLayer.textureOpacity:F0}");
            }

            // Camera background alpha drives the XR swapchain clear:
            //   1 → swapchain clears to alpha=1 → compositor shows VR everywhere
            //   0 → swapchain clears to alpha=0 → compositor shows underlay through background pixels
            var cam = Camera.main;
            if (cam == null)
            {
                DreamGuardLog.LogWarning("[DreamGuardPlayer] SetPassthroughBackground: Camera.main is null");
                return;
            }
            var bg = cam.backgroundColor;
            bg.a = passthroughActive ? 0f : 1f;
            cam.backgroundColor = bg;
            DreamGuardLog.Log($"[DreamGuardPlayer] SetPassthroughBackground({passthroughActive}) → cam.bgAlpha={bg.a:F0}");
        }
    }
}
