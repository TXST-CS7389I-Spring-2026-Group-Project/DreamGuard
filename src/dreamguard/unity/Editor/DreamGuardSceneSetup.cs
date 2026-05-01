using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace DreamGuard.Editor
{
    /// <summary>
    /// One-click scene builder for the DreamGuard VR prototype.
    ///
    /// Menu: DreamGuard → Setup VR Scene
    ///
    /// Creates (or reconfigures) in the active scene:
    ///   • OVRCameraRig with OVRManager (passthrough enabled)
    ///   • DreamGuardLocomotion  on the rig root
    ///   • DreamGuardPassthrough on a child GameObject
    ///   • Ground plane (10 × 10 m)
    ///   • Directional light (if none exists)
    /// </summary>
    public static class DreamGuardSceneSetup
    {
        [MenuItem("DreamGuard/Setup VR Scene")]
        private static void SetupScene()
        {
            if (!EditorUtility.DisplayDialog(
                    "DreamGuard Scene Setup",
                    "This will add/replace VR objects in the active scene. Continue?",
                    "Yes", "Cancel"))
                return;

            Undo.SetCurrentGroupName("DreamGuard Scene Setup");
            int group = Undo.GetCurrentGroup();

            CreateOrGetLight();
            var rig = CreateOrGetCameraRig();
            AttachLocomotion(rig);
            AttachPassthrough(rig);
            CreateGround();

            Undo.CollapseUndoOperations(group);
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());

            Debug.Log("[DreamGuard] Scene setup complete. " +
                      "Press A on the right controller to toggle the passthrough window.");
        }

        // ── light ──────────────────────────────────────────────────────────────────

        private static void CreateOrGetLight()
        {
            if (Object.FindFirstObjectByType<Light>() != null) return;

            var go = new GameObject("Directional Light");
            Undo.RegisterCreatedObjectUndo(go, "Create Light");
            var light = go.AddComponent<Light>();
            light.type = LightType.Directional;
            light.intensity = 1f;
            go.transform.rotation = Quaternion.Euler(50f, -30f, 0f);
        }

        // ── OVR camera rig ────────────────────────────────────────────────────────

        private static GameObject CreateOrGetCameraRig()
        {
            // Reuse if already in scene
            var existing = Object.FindFirstObjectByType<OVRCameraRig>();
            if (existing != null) return existing.gameObject;

            // Try to instantiate the SDK prefab
            var prefab = FindPrefab("OVRCameraRig");
            GameObject rig;
            if (prefab != null)
            {
                rig = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                Undo.RegisterCreatedObjectUndo(rig, "Create OVRCameraRig");
            }
            else
            {
                // Fallback: build a minimal rig manually
                rig = BuildMinimalRig();
            }

            rig.name = "OVRCameraRig";
            rig.transform.position = new Vector3(0f, 0f, 0f);

            // Ensure OVRManager exists and passthrough is on
            var manager = rig.GetComponent<OVRManager>() ?? rig.AddComponent<OVRManager>();
            manager.isInsightPassthroughEnabled = true;

            return rig;
        }

        private static GameObject BuildMinimalRig()
        {
            var root = new GameObject("OVRCameraRig");
            Undo.RegisterCreatedObjectUndo(root, "Create OVRCameraRig");
            root.AddComponent<OVRCameraRig>();

            // OVRCameraRig.Awake() normally creates its children; trigger it by
            // ensuring the component initialises in editor via EnsureGameObjectIntegrity.
            var rigComp = root.GetComponent<OVRCameraRig>();
            // EnsureGameObjectIntegrity is internal; adding the component is enough —
            // it will self-repair when the scene is played.
            return root;
        }

        private static GameObject FindPrefab(string name)
        {
            var guids = AssetDatabase.FindAssets($"{name} t:Prefab");
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                // Prefer the prefab that lives inside the Meta XR SDK packages
                if (path.Contains("com.meta.xr") || path.Contains("Oculus"))
                {
                    var asset = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                    if (asset != null && asset.name == name) return asset;
                }
            }
            // Broader fallback
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var asset = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                if (asset != null && asset.name == name) return asset;
            }
            return null;
        }

        // ── locomotion ────────────────────────────────────────────────────────────

        private static void AttachLocomotion(GameObject rig)
        {
            var loco = rig.GetComponent<DreamGuardLocomotion>();
            if (loco != null) return;

            Undo.AddComponent<DreamGuardLocomotion>(rig);
        }

        // ── passthrough ───────────────────────────────────────────────────────────

        private static void AttachPassthrough(GameObject rig)
        {
            // Reuse if already set up
            if (rig.GetComponentInChildren<DreamGuardPassthrough>() != null) return;

            var ptGO = new GameObject("PassthroughManager");
            Undo.RegisterCreatedObjectUndo(ptGO, "Create PassthroughManager");
            ptGO.transform.SetParent(rig.transform, worldPositionStays: false);

            // OVRPassthroughLayer must be on the same GO as DreamGuardPassthrough
            // (required by [RequireComponent])
            var layer = Undo.AddComponent<OVRPassthroughLayer>(ptGO);
            layer.overlayType = OVROverlay.OverlayType.Underlay;
            layer.projectionSurfaceType = OVRPassthroughLayer.ProjectionSurfaceType.UserDefined;

            Undo.AddComponent<DreamGuardPassthrough>(ptGO);
        }

        // ── ground ────────────────────────────────────────────────────────────────

        private static void CreateGround()
        {
            var existing = GameObject.Find("Ground");
            if (existing != null) return;

            var ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
            Undo.RegisterCreatedObjectUndo(ground, "Create Ground");
            ground.name = "Ground";
            ground.transform.position = Vector3.zero;
            // Unity's Plane is 10 × 10 m by default — perfect.

            // Simple grey material so the floor is visible in the editor
            var mat = new Material(Shader.Find("Universal Render Pipeline/Lit"))
            {
                color = new Color(0.35f, 0.35f, 0.35f)
            };
            ground.GetComponent<Renderer>().sharedMaterial = mat;
        }
    }
}
