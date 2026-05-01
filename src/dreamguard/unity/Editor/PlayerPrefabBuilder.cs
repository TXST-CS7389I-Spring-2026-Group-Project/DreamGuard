using UnityEngine;
using UnityEditor;

namespace DreamGuard.Editor
{
    /// <summary>
    /// Saves Assets/Prefabs/Player.prefab — the Unity equivalent of Godot's Player.tscn.
    ///
    /// In Unity, reusable scene fragments are Prefabs (.prefab), not scenes.
    /// A prefab can be instantiated inside any scene, just as a Godot PackedScene
    /// can be instanced inside another scene.
    ///
    /// Menu: DreamGuard → Build Player Prefab
    ///
    /// The prefab contains:
    ///   • OVRCameraRig root (Meta Quest XR rig, with OVRManager passthrough on)
    ///   • DreamGuardLocomotion – thumbstick move + snap turn
    ///   • PassthroughManager child
    ///       └─ OVRPassthroughLayer + DreamGuardPassthrough (A-button toggle)
    ///   • DreamGuardMenu child (B-button menu, laser pointer, technique buttons)
    ///
    /// If the Meta XR SDK prefab is not found, a minimal camera fallback is used.
    ///
    /// Run DreamGuard → Build Menu Prefab first to create the menu prefab,
    /// then Build Player Prefab to embed it on the player.
    /// </summary>
    public static class PlayerPrefabBuilder
    {
        const string PREFAB_PATH = "Assets/Prefabs/Player.prefab";

        [MenuItem("DreamGuard/Build Player Prefab")]
        public static void BuildPlayerPrefab()
        {
            if (!EditorUtility.DisplayDialog(
                    "Build Player Prefab",
                    $"Create / overwrite {PREFAB_PATH}?",
                    "Build", "Cancel"))
                return;

            var prefab = BuildAndSave();
            if (prefab != null)
            {
                EditorGUIUtility.PingObject(prefab);
                Debug.Log($"[DreamGuard] Player prefab saved → {PREFAB_PATH}");
            }
        }

        /// <summary>
        /// Called by DungeonSceneBuilder so both paths share one implementation.
        /// Returns the saved prefab asset (or null on failure).
        /// </summary>
        public static GameObject BuildAndSave()
        {
            EnsureFolder("Assets/Prefabs");

            // Build a temporary scene object, save it as a prefab, then destroy it.
            var temp = Build();
            if (temp == null) return null;

            var saved = PrefabUtility.SaveAsPrefabAsset(temp, PREFAB_PATH);
            Object.DestroyImmediate(temp);

            AssetDatabase.Refresh();
            return saved;
        }

        /// <summary>
        /// Constructs the player hierarchy as a scene GameObject (not yet saved).
        /// Caller is responsible for destroying it.
        /// </summary>
        public static GameObject Build()
        {
            // ── root: try OVRCameraRig SDK prefab first ────────────────────────────
            var ovrPrefab = FindOvrPrefab("OVRCameraRig");
            GameObject root;

            if (ovrPrefab != null)
            {
                root = (GameObject)PrefabUtility.InstantiatePrefab(ovrPrefab);
                PrefabUtility.UnpackPrefabInstance(
                    root, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
            }
            else
            {
                // Minimal fallback: plain camera rig
                root = BuildMinimalRig();
            }

            root.name = "Player";

            // ── OVRManager (passthrough enabled) ──────────────────────────────────
            var manager = root.GetComponent<OVRManager>();
            if (manager == null) manager = root.AddComponent<OVRManager>();
            manager.isInsightPassthroughEnabled = true;

            // ── CharacterController (collision with dungeon geometry) ─────────────
            var cc = root.GetComponent<CharacterController>();
            if (cc == null) cc = root.AddComponent<CharacterController>();
            cc.height     = 1.8f;
            cc.radius     = 0.3f;
            cc.center     = new Vector3(0f, 0.9f, 0f);
            cc.slopeLimit = 45f;
            cc.stepOffset = 0.3f;

            // ── Locomotion ────────────────────────────────────────────────────────
            if (root.GetComponent<DreamGuardLocomotion>() == null)
                root.AddComponent<DreamGuardLocomotion>();

            // ── Passthrough manager child ─────────────────────────────────────────
            if (root.GetComponentInChildren<DreamGuardPassthrough>() == null)
            {
                var ptGO  = new GameObject("PassthroughManager");
                ptGO.transform.SetParent(root.transform, worldPositionStays: false);

                var layer = ptGO.AddComponent<OVRPassthroughLayer>();
                layer.overlayType = OVROverlay.OverlayType.Underlay;
                layer.projectionSurfaceType =
                    OVRPassthroughLayer.ProjectionSurfaceType.UserDefined;

                ptGO.AddComponent<DreamGuardPassthrough>();
            }

            // ── Menu ──────────────────────────────────────────────────────────────
            if (root.GetComponentInChildren<DreamGuardMenu>() == null)
                AttachMenu(root);

            return root;
        }

        // ── menu ─────────────────────────────────────────────────────────────────

        static void AttachMenu(GameObject root)
        {
            // Prefer the saved prefab so button onSelect events survive re-builds.
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(DreamGuardMenuBuilder.PREFAB_PATH);
            GameObject menuGO;
            if (prefab != null)
            {
                menuGO = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            }
            else
            {
                Debug.LogWarning("[DreamGuard] Menu prefab not found at " +
                    DreamGuardMenuBuilder.PREFAB_PATH +
                    " — run DreamGuard → Build Menu Prefab first. " +
                    "Building inline fallback.");
                menuGO = DreamGuardMenuBuilder.Build();
            }
            menuGO.transform.SetParent(root.transform, worldPositionStays: false);
        }

        // ── helpers ───────────────────────────────────────────────────────────────

        static GameObject BuildMinimalRig()
        {
            var root  = new GameObject("Player");
            var camGO = new GameObject("MainCamera");
            camGO.transform.SetParent(root.transform, worldPositionStays: false);
            var cam = camGO.AddComponent<Camera>();
            cam.tag         = "MainCamera";
            cam.clearFlags  = CameraClearFlags.SolidColor;
            cam.backgroundColor = new Color(0.02f, 0.02f, 0.04f);
            camGO.AddComponent<AudioListener>();
            return root;
        }

        static GameObject FindOvrPrefab(string name)
        {
            foreach (var guid in AssetDatabase.FindAssets($"{name} t:Prefab"))
            {
                var path  = AssetDatabase.GUIDToAssetPath(guid);
                if (!path.Contains("com.meta.xr") && !path.Contains("Oculus")) continue;
                var asset = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                if (asset != null && asset.name == name) return asset;
            }
            return null;
        }

        static void EnsureFolder(string path)
        {
            if (AssetDatabase.IsValidFolder(path)) return;
            var parts  = path.Split('/');
            var parent = parts[0];
            for (int i = 1; i < parts.Length; i++)
            {
                var full = parent + "/" + parts[i];
                if (!AssetDatabase.IsValidFolder(full))
                    AssetDatabase.CreateFolder(parent, parts[i]);
                parent = full;
            }
        }
    }
}
