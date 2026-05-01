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
    ///   • DreamGuardMenu on a child GameObject (B-button menu, laser pointer)
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
            AttachMenu(rig);
            CreateGround();

            Undo.CollapseUndoOperations(group);
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());

            Debug.Log("[DreamGuard] Scene setup complete. " +
                      "Press B on the right controller to open the passthrough menu.");
        }

        /// <summary>
        /// Adds the fog-based passthrough boundary to the VR scene.
        /// Replaces (or skips) the simple window passthrough if already present.
        ///
        /// This sets up DreamGuardPassthroughFog which renders a fog ring around
        /// the player — full VR inside, gradual passthrough reveal outside.
        /// </summary>
        [MenuItem("DreamGuard/Setup Fog Passthrough Boundary")]
        private static void SetupFogPassthrough()
        {
            if (!EditorUtility.DisplayDialog(
                    "Fog Passthrough Setup",
                    "This will add the fog passthrough boundary to the camera rig. Continue?",
                    "Yes", "Cancel"))
                return;

            Undo.SetCurrentGroupName("DreamGuard Fog Passthrough Setup");
            int group = Undo.GetCurrentGroup();

            CreateOrGetLight();
            var rig = CreateOrGetCameraRig();
            AttachLocomotion(rig);
            AttachPassthroughFog(rig);
            AttachMenu(rig);
            CreateGround();

            Undo.CollapseUndoOperations(group);
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());

            Debug.Log("[DreamGuard] Fog passthrough boundary set up. " +
                      "Adjust Inner Radius and Fog Band Width on DreamGuardPassthroughFog.");
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

        // ── menu ─────────────────────────────────────────────────────────────────

        private static void AttachMenu(GameObject rig)
        {
            if (rig.GetComponentInChildren<DreamGuardMenu>() != null) return;

            // Prefer the saved prefab so button onSelect events survive re-builds.
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(DreamGuardMenuBuilder.PREFAB_PATH);
            GameObject menuGO;
            if (prefab != null)
            {
                menuGO = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                Undo.RegisterCreatedObjectUndo(menuGO, "Create DreamGuardMenu");
            }
            else
            {
                Debug.LogWarning("[DreamGuard] Menu prefab not found at " +
                    DreamGuardMenuBuilder.PREFAB_PATH +
                    " — run DreamGuard → Build Menu Prefab first. " +
                    "Building inline fallback.");
                menuGO = DreamGuardMenuBuilder.Build();
                Undo.RegisterCreatedObjectUndo(menuGO, "Create DreamGuardMenu");
            }
            menuGO.transform.SetParent(rig.transform, worldPositionStays: false);
        }

        /// <summary>
        /// Adds the floor/ceiling grid passthrough boundary to the VR scene.
        ///
        /// DreamGuardGridPassthrough renders a world-aligned grid on the dungeon
        /// floor and ceiling.  Near the player the grid is solid VR; further away
        /// the fill between grid lines dissolves to passthrough, with the lines
        /// themselves fading last.
        /// </summary>
        [MenuItem("DreamGuard/Setup Grid Passthrough")]
        private static void SetupGridPassthrough()
        {
            if (!EditorUtility.DisplayDialog(
                    "Grid Passthrough Setup",
                    "This will add the grid passthrough boundary to the camera rig. Continue?",
                    "Yes", "Cancel"))
                return;

            Undo.SetCurrentGroupName("DreamGuard Grid Passthrough Setup");
            int group = Undo.GetCurrentGroup();

            CreateOrGetLight();
            var rig = CreateOrGetCameraRig();
            AttachLocomotion(rig);
            AttachGridPassthrough(rig);
            AttachMenu(rig);
            CreateGround();

            Undo.CollapseUndoOperations(group);
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());

            Debug.Log("[DreamGuard] Grid passthrough set up. " +
                      "Adjust Floor Y, Ceiling Y, Inner Radius, and Gradient Width " +
                      "on DreamGuardGridPassthrough.");
        }

        // ── fog passthrough ───────────────────────────────────────────────────────

        private static void AttachPassthroughFog(GameObject rig)
        {
            if (rig.GetComponentInChildren<DreamGuardPassthroughFog>() != null) return;

            var fogGO = new GameObject("PassthroughFogBoundary");
            Undo.RegisterCreatedObjectUndo(fogGO, "Create PassthroughFogBoundary");
            fogGO.transform.SetParent(rig.transform, worldPositionStays: false);

            // OVRPassthroughLayer in Underlay mode fills the background with
            // passthrough; DreamGuardPassthroughFog's shader cuts the VR alpha
            // layer to reveal it at the outer boundary.
            var layer = Undo.AddComponent<OVRPassthroughLayer>(fogGO);
            layer.overlayType = OVROverlay.OverlayType.Underlay;
            // Projection type left at default (Reconstruction) – the fog dome
            // is a regular Unity mesh, not an OVR projection surface.

            Undo.AddComponent<DreamGuardPassthroughFog>(fogGO);
        }

        // ── grid passthrough ──────────────────────────────────────────────────────

        private static void AttachGridPassthrough(GameObject rig)
        {
            if (rig.GetComponentInChildren<DreamGuardGridPassthrough>() != null) return;

            var gridGO = new GameObject("GridPassthroughManager");
            Undo.RegisterCreatedObjectUndo(gridGO, "Create GridPassthroughManager");
            gridGO.transform.SetParent(rig.transform, worldPositionStays: false);

            // OVRPassthroughLayer in Underlay mode fills the compositor background
            // with passthrough wherever the VR framebuffer alpha == 0.
            var layer = Undo.AddComponent<OVRPassthroughLayer>(gridGO);
            layer.overlayType = OVROverlay.OverlayType.Underlay;

            Undo.AddComponent<DreamGuardGridPassthrough>(gridGO);
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
