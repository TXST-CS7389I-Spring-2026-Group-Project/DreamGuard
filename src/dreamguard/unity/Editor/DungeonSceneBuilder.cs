using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace DreamGuard.Editor
{
    /// <summary>
    /// Recreates the Godot Dungeon.tscn layout as Assets/Scenes/Dungeon.unity.
    ///
    /// Uses the real dungeon mesh assets from Assets/Art/Dungeon/ (copied from
    /// playground/godot/Scenes/_Objects/Dungeon/glft/) imported via glTFast
    /// (com.unity.cloud.gltfast).  Positions, rotations, and collision shapes
    /// are taken directly from the Godot .tscn files.  Scale is 1:1.
    ///
    /// Menu: DreamGuard → Build Dungeon Scene
    /// </summary>
    public static class DungeonSceneBuilder
    {
        const string ART = "Assets/Art/Dungeon";

        // ── floor positions (x, z) — y=0 matches Godot placement ────────────────

        static readonly (float x, float z)[] s_Floors =
        {
            // Room_C
            (-2,-2),(2,-2),(-2,2),(2,2),
            (-2,-10),(2,-10),(-2,-6),(-6,-6),(2,-6),(6,-6),
            (2,6),(-2,6),(-2,10),(2,10),
            (-6,2),(-6,6),(6,-2),(6,2),(6,6),
            (10,2),(10,-2),(-6,-2),(-10,-2),(-10,2),
            // Room_N
            (-2,-18),(2,-18),(-2,-14),(-6,-18),(-6,-14),(6,-18),
            (-2,-22),(2,-22),(-6,-22),(6,-22),(6,-14),(2,-14),
            // Room_E
            (14,-2),(14,2),(18,2),(14,6),(18,6),(14,-6),(18,-6),
            (18,-2),(22,-2),(22,-6),(22,6),(22,2),
            // Room_S
            (-2,22),(2,22),(-6,22),(6,22),
            (2,14),(-2,14),(-2,18),(2,18),(6,18),(6,14),(-6,14),(-6,18),
            // Room_W
            (-14,2),(-14,-2),(-18,-2),(-14,-6),(-18,-6),(-14,6),(-18,6),
            (-18,2),(-22,-2),(-22,-6),(-22,6),(-22,2),
            // Hallway SE
            (10,18),(14,18),(18,18),(18,14),(18,10),
            // Hallway SW
            (-10,18),(-14,18),(-18,18),(-18,14),(-18,10),
            // Hallway NE
            (18,-10),(10,-18),(14,-18),(18,-18),(18,-14),
            // Hallway NW
            (-10,-18),(-14,-18),(-18,-18),(-18,-14),(-18,-10),
        };

        // ── wall segments (x, z, rotY) — wall.tscn: BoxShape3D(4,4,1) at y=2 ──

        static readonly (float x, float z, float ry)[] s_Walls =
        {
            (-24, 4,90),(-24, 0,90),(-24,-4,90),
            (-20,-12,90),(-20,-16,90),
            (-16,-20,180),
            (24,-4,-90),(24, 0,-90),(24, 4,-90),
            (16, 12,-90),(-16,-12,-90),(-16, 12,-90),
            (16,-12,-90),(20,-16,-90),(20,-12,-90),
            (20, 12,-90),(20, 16,-90),
            (-20, 12,-90),(-20, 16,-90),
            ( 4,24,180),( 0,24,180),(-4,24,180),
            (16,20,180),(12,20,180),(-12,20,180),(-16,20,180),
            (-4,-24,0),(0,-24,0),(4,-24,0),
            (12,-20,0),(12,-16,0),(12,16,0),
            (-12,16,0),(-12,-16,0),(16,-20,0),(-12,-20,0),
        };

        // ── wall corners (x, z, rotY) — rotations taken directly from Dungeon.tscn

        static readonly (float x, float z, float ry)[] s_Corners =
        {
            (-24, 8,180), (-20, 8,  0), (-20,20,180), ( -8,20,  0),
            (-24,-8, 90), (-20,-8,-90), (-20,-20, 90),
            ( 24,-8,  0), ( 20,-8,180), ( 20,-20,  0), (16,-16,  0),
            (  8,-8,  0), (-12,-8,  0), ( -8, 4,  0), (-4,-12,  0), (-8,-16,  0),
            ( -8,12, 90), ( -8,-8, 90), ( 12,-8, 90), ( 8,-16, 90),
            (-16,-16,90), (-16, 8, 90), (  4, 8, 90), ( 4,-12, 90),
            (  4,-8,180), ( -8, 8,180), (-12,-4,180), (-8,-12,180),
            (-16,-8,180), ( 12, 8,180), (-16,16,180), ( 8, 16,180),
            (  4,12,180), (  8,-4,180),
            ( 12,-4,-90), ( -8,-4,-90), (-12, 8,-90), ( 8,-12,-90),
            (  8, 8,-90), ( -4,-8,-90), ( 16,16,-90), (-8, 16,-90),
            ( -4,12,-90),
            (-12, 4, 90), (  8, 4, 90),
            (  8,12,  0), ( 12, 4,  0), ( 16, 8,  0), (-4,  8,  0),
            ( 16,-8,-90), ( 24, 8,-90), ( 20, 8, 90), (20, 20,-90),
            (  8,24,-90), (  8,20, 90), ( -8,24,180),
            ( -8,-24, 90), (-8,-20,-90), (8,-24,  0), ( 8,-20,180),
        };

        // ── torches (x, y, z, rotY) — torch_mounted.tscn: OmniLight at (0,0.454,0.311)

        static readonly (float x, float y, float z, float ry)[] s_Torches =
        {
            // Room_C
            (-6,1.75f,-7.75f,0),   (5.75f,1.75f,-7.75f,0),
            (-6,1.75f,7.75f,180),  (-7.75f,1.75f,6,90),
            (-19.75f,1.75f,12,90), (-7.75f,1.75f,-6,90),
            (7.75f,1.75f,6,-90),   (7.75f,1.75f,-6,-90),
            (5.75f,1.75f,7.75f,180),
            // Room_N
            (-6,1.75f,-23.75f,0),  (5.75f,1.75f,-23.75f,0),
            (-7.75f,1.75f,-14,90), (-7.75f,1.75f,-22,90),
            (7.75f,1.75f,-14,-90), (7.75f,1.75f,-22,-90),
            (-6,1.75f,-12.25f,180),(5.75f,1.75f,-12.25f,180),
            // Room_E
            (14.25f,1.75f,-7.75f,0),(21.75f,1.75f,-7.75f,0),
            (12.5f,1.75f,6,90),    (12.5f,1.75f,-6,90),
            (23.5f,1.75f,6,-90),   (23.5f,1.75f,-6,-90),
            (14.25f,1.75f,7.75f,180),(22,1.75f,7.75f,180),
            // Room_W
            (-21.75f,1.75f,-7.75f,0),(-14.25f,1.75f,-7.75f,0),
            (-12.5f,1.75f,6,-90),  (-12.5f,1.75f,-6,-90),
            (-23.75f,1.75f,6,90),  (-23.75f,1.75f,-6,90),
            (-14.25f,1.75f,7.75f,180),(-22,1.75f,7.75f,180),
            // Room_S
            (-6,1.75f,12.25f,180), (5.75f,1.75f,12.25f,180),
            (-7.75f,1.75f,14,90),  (-7.75f,1.75f,22,90),
            (7.75f,1.75f,22,-90),  (7.75f,1.75f,14,-90),
            (-6,1.75f,23.75f,0),   (5.75f,1.75f,23.75f,0),
        };

        // ── columns (x, z) — column.tscn: CylinderShape3D(r=0.35,h=1.4) at y=0.7

        static readonly (float x, float z)[] s_Columns =
        {
            (-3,-15),(3,-15),(-3,-19),(3,-19),  // Room_N
            (15,-3),(19,-3),(15,3),(19,3),       // Room_E
            (-15,-3),(-21,-3),(-15,3),(-21,3),  // Room_W
            (-3,15),(3,15),(-3,21),(3,21),       // Room_S
        };

        // ── barrels (x, z) — barrel_large.tscn: CylinderShape3D(r=0.9,h=2) at y=1

        static readonly (float x, float z)[] s_Barrels =
        {
            (22,-6),(14,6),          // Room_E
            (-22,-6),(-22,6),        // Room_W
            (-6,20),(6,14),          // Room_S
        };

        // ── chests (x, z, rotY) — chest_gold.tscn: BoxShape3D(1.6,1.05,1.25) at y=0.52

        static readonly (float x, float z, float ry)[] s_Chests =
        {
            (0,-17,0),    // Room_N
            (17,0,-90),   // Room_E
        };

        // ── rubble (x, z, rotY) — visible=false in Godot; rotations from Dungeon.tscn

        static readonly (float x, float z, float ry)[] s_Rubble =
        {
            (-8,-18,0), (-10,-16,180), (12,6,-90),
            (4,20,0),   (0,26,0),     (-12,4,-90),
        };

        // ── builder ───────────────────────────────────────────────────────────────

        [MenuItem("DreamGuard/Build Dungeon Scene")]
        static void BuildDungeonScene()
        {
            if (!EditorUtility.DisplayDialog(
                    "Build Dungeon Scene",
                    "Create Assets/Scenes/Dungeon.unity from the Godot layout?\n" +
                    "Requires Assets/Art/Dungeon/ meshes and com.unity.cloud.gltfast.",
                    "Build", "Cancel"))
                return;

            var scene = EditorSceneManager.NewScene(
                NewSceneSetup.EmptyScene, NewSceneMode.Single);

            // Cave ambient — mirrors Godot CaveEnv (ambient_light_energy=0.3, dark orange)
            RenderSettings.ambientMode  = UnityEngine.Rendering.AmbientMode.Flat;
            RenderSettings.ambientLight = new Color(0.08f, 0.05f, 0.03f);

            // ── Hierarchy root ────────────────────────────────────────────────────
            var dungeon  = new GameObject("Dungeon");
            var rooms    = Child(dungeon.transform, "Rooms");
            var floorGrp = Child(rooms, "Floors");
            var wallGrp  = Child(rooms, "Walls");
            var propsGrp = Child(rooms, "Props");

            // ── Floors (visual only — collision handled by one large box below) ──
            foreach (var (x, z) in s_Floors)
            {
                var go = Spawn(floorGrp, "floor", x, 0, z, 0);
                go.name = $"Floor_{x}_{z}";
                go.isStatic = true;
            }

            // Single large floor collider matching Godot's FloorBoxShape (48×0.2×48)
            var floorCol = new GameObject("FloorCollider");
            floorCol.transform.SetParent(floorGrp, false);
            floorCol.isStatic = true;
            var fc = floorCol.AddComponent<BoxCollider>();
            fc.size   = new Vector3(48, 0.2f, 48);
            fc.center = new Vector3(0, -0.1f, 0);

            // ── Walls — wall.tscn: BoxShape3D(4,4,1), collider centre y=2 ────────
            foreach (var (x, z, ry) in s_Walls)
            {
                var go = Spawn(wallGrp, "wall", x, 0, z, ry);
                go.name = $"Wall_{x}_{z}";
                go.isStatic = true;
                var bc = go.AddComponent<BoxCollider>();
                bc.size   = new Vector3(4, 4, 1);
                bc.center = new Vector3(0, 2, 0);
            }

            // ── Corners — wall_corner.tscn: BoxShape3D(2.5,4,2.5), centre (-0.75,2,0.75)
            foreach (var (x, z, ry) in s_Corners)
            {
                var go = Spawn(wallGrp, "wall_corner", x, 0, z, ry + 270f);
                go.name = $"Corner_{x}_{z}";
                go.isStatic = true;
                var bc = go.AddComponent<BoxCollider>();
                bc.size   = new Vector3(2.5f, 4, 2.5f);
                bc.center = new Vector3(-0.75f, 2, 0.75f);
            }

            // ── Columns — column.tscn: CylinderShape3D(r=0.35,h=1.4), centre y=0.7
            foreach (var (x, z) in s_Columns)
            {
                var go = Spawn(propsGrp, "column", x, 0, z, 0);
                go.name = $"Column_{x}_{z}";
                go.isStatic = true;
                var cc = go.AddComponent<CapsuleCollider>();
                cc.radius = 0.35f;
                cc.height = 1.4f;
                cc.center = new Vector3(0, 0.7f, 0);
            }

            // ── Barrels — barrel_large.tscn: CylinderShape3D(r=0.9,h=2), centre y=1
            foreach (var (x, z) in s_Barrels)
            {
                var go = Spawn(propsGrp, "barrel_large", x, 0, z, 0);
                go.name = $"Barrel_{x}_{z}";
                var cc = go.AddComponent<CapsuleCollider>();
                cc.radius = 0.9f;
                cc.height = 2f;
                cc.center = new Vector3(0, 1f, 0);
            }

            // ── Chests — chest_gold.tscn: BoxShape3D(1.6,1.05,1.25), centre (0,0.52,0.025)
            foreach (var (x, z, ry) in s_Chests)
            {
                var go = Spawn(propsGrp, "chest_gold", x, 0, z, ry);
                go.name = $"Chest_{x}_{z}";
                var bc = go.AddComponent<BoxCollider>();
                bc.size   = new Vector3(1.6f, 1.05f, 1.25f);
                bc.center = new Vector3(0, 0.52f, 0.025f);
            }

            // ── Rubble (hidden) — rubble_large.tscn: BoxShape3D(8.13,3.5,3.19), centre y=1.75
            var rubbleGrp = Child(dungeon.transform, "Props");
            rubbleGrp.gameObject.SetActive(false);   // matches Godot visible=false
            foreach (var (x, z, ry) in s_Rubble)
            {
                var go = Spawn(rubbleGrp, "rubble_large", x, 0, z, ry);
                go.name = $"Rubble_{x}_{z}";
                var bc = go.AddComponent<BoxCollider>();
                bc.size   = new Vector3(8.13f, 3.5f, 3.19f);
                bc.center = new Vector3(0, 1.75f, 0);
            }

            // ── Torches — torch_mounted.tscn mesh + OmniLight3D(0,0.454,0.311)
            //   energy=5, range=7, color=(0.55,0.4,0.25)
            var torchGrp = Child(dungeon.transform, "Torches");
            int ti = 0;
            foreach (var (x, y, z, ry) in s_Torches)
            {
                var go = Spawn(torchGrp, "torch_mounted", x, y, z, ry);
                go.name = $"Torch_{ti++}";

                var lightGO = new GameObject("OmniLight");
                lightGO.transform.SetParent(go.transform, false);
                lightGO.transform.localPosition = new Vector3(0, 0.454f, 0.311f);
                var lt = lightGO.AddComponent<Light>();
                lt.type      = LightType.Point;
                lt.color     = new Color(0.55f, 0.4f, 0.25f);
                lt.intensity = 5f;
                lt.range     = 7f;
            }

            // ── Player prefab ─────────────────────────────────────────────────────
            InstantiatePlayer(dungeon.transform);

            // ── Save ──────────────────────────────────────────────────────────────
            EnsureFolder("Assets/Scenes");
            EditorSceneManager.SaveScene(scene, "Assets/Scenes/Dungeon.unity");
            AssetDatabase.Refresh();

            Debug.Log("[DreamGuard] Dungeon scene saved → Assets/Scenes/Dungeon.unity");
        }

        // ── helpers ───────────────────────────────────────────────────────────────

        /// <summary>
        /// Instantiates a glTF model from Assets/Art/Dungeon/.
        /// Falls back to a grey cube if the asset isn't imported yet.
        /// </summary>
        static GameObject Spawn(Transform parent, string mesh,
                                float x, float y, float z, float rotY)
        {
            var path   = $"{ART}/{mesh}.gltf";
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            GameObject go;
            if (prefab != null)
            {
                go = (GameObject)Object.Instantiate(prefab);
            }
            else
            {
                Debug.LogWarning($"[DreamGuard] '{path}' not found — using placeholder cube. " +
                                 "Open Unity, let it import Assets/Art/Dungeon/, then rebuild.");
                go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            }

            go.transform.SetParent(parent, false);
            go.transform.localPosition = new Vector3(x, y, z);
            go.transform.localRotation = Quaternion.Euler(0, rotY, 0);
            return go;
        }

        static void InstantiatePlayer(Transform parent)
        {
            const string path = "Assets/Prefabs/Player.prefab";
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefab == null)
            {
                Debug.Log("[DreamGuard] Player prefab not found — building it now.");
                prefab = PlayerPrefabBuilder.BuildAndSave();
            }

            GameObject player;
            if (prefab != null)
            {
                player = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            }
            else
            {
                player = new GameObject("Player");
                var camGO = new GameObject("MainCamera");
                camGO.transform.SetParent(player.transform, false);
                var cam = camGO.AddComponent<Camera>();
                cam.tag             = "MainCamera";
                cam.clearFlags      = CameraClearFlags.SolidColor;
                cam.backgroundColor = new Color(0.02f, 0.02f, 0.04f);
                camGO.AddComponent<AudioListener>();
            }

            player.transform.SetParent(parent, false);
            player.transform.localPosition = new Vector3(0f, 0.138f, -0.024f);
        }

        static Transform Child(Transform parent, string name)
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent, false);
            return go.transform;
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
