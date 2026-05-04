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

        [Tooltip("If true, the default MR mode is enabled on startup when the system " +
                 "recommends passthrough based on the user's home environment preference.")]
        [SerializeField] private bool _respectSystemRecommendation = true;

        [Tooltip("The passthrough mode to auto-enable when the system recommends MR. " +
                 "Leave null to skip auto-enable.")]
        [SerializeField] private DreamGuardPassthroughFog _defaultMrMode;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;

            if (_respectSystemRecommendation && _defaultMrMode != null)
                _defaultMrMode.SetFogEnabled(OVRManager.IsPassthroughRecommended());
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            if (Instance == this)
                Instance = null;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            var spawn = FindFirstObjectByType<DreamGuardSpawnPoint>();
            if (spawn == null) return;

            // Apply position and Y-rotation only — never pitch or roll the player.
            transform.position = spawn.transform.position;
            var euler = transform.eulerAngles;
            euler.y = spawn.transform.eulerAngles.y;
            transform.eulerAngles = euler;
        }

        /// <summary>
        /// Enable or disable all OVRPassthroughLayer components on this Player.
        /// Call with <c>false</c> when entering a full-VR scene, <c>true</c> when returning to MR.
        /// </summary>
        public void EnablePassthrough(bool enable)
        {
            foreach (var layer in GetComponentsInChildren<OVRPassthroughLayer>(includeInactive: true))
                layer.enabled = enable;
        }
    }
}
