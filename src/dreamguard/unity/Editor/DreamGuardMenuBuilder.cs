using UnityEngine;
using UnityEditor;

namespace DreamGuard.Editor
{
    /// <summary>
    /// Creates Assets/Prefabs/DreamGuardMenu.prefab — a floating VR menu.
    ///
    /// Menu: DreamGuard → Build Menu Prefab
    ///
    /// The prefab contains:
    ///   • DreamGuardMenu root (with LineRenderer laser)
    ///   • MenuPanel child
    ///       └─ Background (Quad)
    ///       └─ BtnOff     (DreamGuardMenuButton → DreamGuardPassthrough.SetActive(false))
    ///       └─ BtnWindow  (DreamGuardMenuButton → DreamGuardPassthrough.Toggle)
    ///
    /// To add a new passthrough technique after building:
    ///   1. Open the prefab in the Unity Prefab Editor.
    ///   2. Duplicate an existing button child of MenuPanel.
    ///   3. Rename it, update DreamGuardMenuButton.buttonLabel.
    ///   4. Wire the onSelect UnityEvent to the new technique's method.
    ///   5. Save the prefab — all player instances update automatically.
    /// </summary>
    public static class DreamGuardMenuBuilder
    {
        public const string PREFAB_PATH = "Assets/Prefabs/DreamGuardMenu.prefab";

        [MenuItem("DreamGuard/Build Menu Prefab")]
        public static void BuildMenuPrefab()
        {
            if (!EditorUtility.DisplayDialog(
                    "Build Menu Prefab",
                    $"Create / overwrite {PREFAB_PATH}?",
                    "Build", "Cancel"))
                return;

            var prefab = BuildAndSave();
            if (prefab != null)
            {
                EditorGUIUtility.PingObject(prefab);
                Debug.Log($"[DreamGuard] Menu prefab saved → {PREFAB_PATH}");
            }
        }

        /// <summary>Called by PlayerPrefabBuilder so both paths share one implementation.</summary>
        public static GameObject BuildAndSave()
        {
            EnsureFolder("Assets/Prefabs");
            var temp = Build();
            if (temp == null) return null;
            var saved = PrefabUtility.SaveAsPrefabAsset(temp, PREFAB_PATH);
            Object.DestroyImmediate(temp);
            AssetDatabase.Refresh();
            return saved;
        }

        /// <summary>
        /// Constructs the menu hierarchy as a scene GameObject (not yet saved).
        /// Caller is responsible for destroying it.
        /// </summary>
        public static GameObject Build()
        {
            var root = new GameObject("DreamGuardMenu");
            var menu = root.AddComponent<DreamGuardMenu>();

            // ── laser line renderer ────────────────────────────────────────────────
            var laserGO = new GameObject("Laser");
            laserGO.transform.SetParent(root.transform, false);
            var lr = laserGO.AddComponent<LineRenderer>();
            lr.positionCount  = 2;
            lr.useWorldSpace  = true;
            lr.startWidth     = 0.003f;
            lr.endWidth       = 0.001f;
            lr.startColor     = new Color(0.4f, 0.7f, 1f, 1f);
            lr.endColor       = new Color(0.4f, 0.7f, 1f, 0f);

            var laserShader = Shader.Find("Universal Render Pipeline/Unlit");
            if (laserShader != null)
                lr.material = new Material(laserShader);

            // ── panel ──────────────────────────────────────────────────────────────
            var panel = new GameObject("MenuPanel");
            panel.transform.SetParent(root.transform, false);

            // Background quad (no collider)
            var bg = GameObject.CreatePrimitive(PrimitiveType.Quad);
            bg.name = "Background";
            bg.transform.SetParent(panel.transform, false);
            bg.transform.localScale    = new Vector3(0.22f, 0.30f, 1f);
            bg.transform.localPosition = Vector3.zero;
            Object.DestroyImmediate(bg.GetComponent<MeshCollider>());
            var bgMat = new Material(Shader.Find("Universal Render Pipeline/Unlit") ?? Shader.Find("Unlit/Color"));
            bgMat.color = new Color(0.05f, 0.05f, 0.1f);
            bg.GetComponent<Renderer>().sharedMaterial = bgMat;

            // Title text
            MakeLabel(panel.transform, "DreamGuard Mode", new Vector3(0f, 0.115f, 0.004f), 0.006f);

            // Buttons — onSelect must be wired manually in the prefab inspector
            // since we can't reference scene objects from an editor-time builder.
            string[] labels = { "Off", "Window Passthrough" };
            float buttonHeight = 0.038f;
            float startY = (labels.Length - 1) * buttonHeight * 0.5f;
            for (int i = 0; i < labels.Length; i++)
                MakeButton(panel.transform, labels[i], new Vector3(0f, startY - i * buttonHeight, 0.004f));

            // ── wire serialized refs ───────────────────────────────────────────────
            var so = new SerializedObject(menu);
            so.FindProperty("menuPanel").objectReferenceValue = panel.transform;
            so.FindProperty("laserLine").objectReferenceValue = lr;
            so.ApplyModifiedPropertiesWithoutUndo();

            return root;
        }

        // ── helpers ───────────────────────────────────────────────────────────────

        static void MakeButton(Transform parent, string label, Vector3 localPos)
        {
            var go = new GameObject($"Btn_{label.Replace(" ", "")}");
            go.transform.SetParent(parent, false);
            go.transform.localPosition = localPos;

            // Visual with trigger collider so the laser detects it without blocking physics
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.name = "Visual";
            cube.transform.SetParent(go.transform, false);
            cube.transform.localScale = new Vector3(0.18f, 0.028f, 0.003f);
            cube.GetComponent<BoxCollider>().isTrigger = true;
            var mat = new Material(Shader.Find("Universal Render Pipeline/Unlit") ?? Shader.Find("Unlit/Color"));
            mat.color = new Color(0.12f, 0.12f, 0.20f);
            cube.GetComponent<Renderer>().sharedMaterial = mat;

            // Text label
            MakeLabel(go.transform, label, new Vector3(0f, 0f, 0.005f), 0.004f);

            // Button component
            var btn = go.AddComponent<DreamGuardMenuButton>();
            var bso = new SerializedObject(btn);
            bso.FindProperty("buttonLabel").stringValue = label;
            bso.ApplyModifiedPropertiesWithoutUndo();
        }

        static void MakeLabel(Transform parent, string text, Vector3 localPos, float scale)
        {
            var go = new GameObject("Label");
            go.transform.SetParent(parent, false);
            go.transform.localPosition = localPos;
            go.transform.localScale    = Vector3.one * scale;
            var tm = go.AddComponent<TextMesh>();
            tm.text      = text;
            tm.fontSize  = 24;
            tm.color     = Color.white;
            tm.anchor    = TextAnchor.MiddleCenter;
            tm.alignment = TextAlignment.Center;
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
