using System.Collections.Generic;
using System.IO;
using System.Text;
using DreamGuard.Experiment;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Editor utility: takes a top-down orthographic screenshot of the dungeon
/// with temporary full-white ambient lighting and a straight-down directional light.
/// Menu: Tools/DreamGuard/Dungeon Top-Down Screenshot
/// Output: &lt;project root&gt;/DungeonTopDown.png  (also revealed in Finder/Explorer)
/// </summary>
public static class DungeonScreenshot
{
    private const int TextureWidth  = 2048;
    private const int TextureHeight = 2048;
    private const float HeightPadding  = 10f;   // metres above the highest point
    private const float BoundsPadding  = 2f;    // extra padding around scene extents

    [MenuItem("Tools/DreamGuard/Dungeon Top-Down Screenshot")]
    public static void TakeScreenshot()
    {
        // ── 1. Compute scene bounds from all active renderers ─────────────────
        Bounds bounds = CalculateSceneBounds();
        if (bounds.size == Vector3.zero)
        {
            Debug.LogError("[DungeonScreenshot] No renderers found in the active scene.");
            return;
        }
        Debug.Log($"[DungeonScreenshot] Scene bounds: center={bounds.center}, size={bounds.size}");

        // ── 2. Save and override lighting ─────────────────────────────────────
        AmbientMode savedAmbientMode   = RenderSettings.ambientMode;
        Color       savedAmbientLight  = RenderSettings.ambientLight;
        Color       savedAmbientSky    = RenderSettings.ambientSkyColor;

        RenderSettings.ambientMode  = AmbientMode.Flat;
        RenderSettings.ambientLight = Color.white;

        // ── 3. Temporary overhead directional light ───────────────────────────
        GameObject lightGO = new GameObject("_TempTopDownLight");
        Light light = lightGO.AddComponent<Light>();
        light.type      = LightType.Directional;
        light.intensity = 1f;
        light.color     = Color.white;
        light.shadows   = LightShadows.None;
        lightGO.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        Debug.Log("[DungeonScreenshot] Temporary directional light created.");

        // ── 4. Temporary orthographic camera ──────────────────────────────────
        GameObject cameraGO = new GameObject("_TempTopDownCamera");
        Camera cam = cameraGO.AddComponent<Camera>();
        cam.orthographic    = true;
        cam.clearFlags      = CameraClearFlags.SolidColor;
        cam.backgroundColor = new Color(0.08f, 0.08f, 0.08f, 1f); // dark grey bg
        cam.nearClipPlane   = 0.1f;
        cam.farClipPlane    = bounds.size.y + HeightPadding * 2f + 200f;

        float camY = bounds.max.y + HeightPadding;
        cameraGO.transform.SetPositionAndRotation(
            new Vector3(bounds.center.x, camY, bounds.center.z),
            Quaternion.Euler(90f, 0f, 0f)
        );

        // Cover the wider horizontal extent with a bit of padding
        float halfX = bounds.extents.x + BoundsPadding;
        float halfZ = bounds.extents.z + BoundsPadding;
        cam.orthographicSize = Mathf.Max(halfX, halfZ);
        Debug.Log($"[DungeonScreenshot] Camera: pos={cameraGO.transform.position}, orthoSize={cam.orthographicSize}");

        // ── 5. Render to texture and encode to PNG ────────────────────────────
        RenderTexture rt = new RenderTexture(TextureWidth, TextureHeight, 24, RenderTextureFormat.ARGB32);
        cam.targetTexture = rt;
        cam.Render();

        RenderTexture.active = rt;
        Texture2D tex = new Texture2D(TextureWidth, TextureHeight, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, TextureWidth, TextureHeight), 0, 0);
        tex.Apply();

        string outputPath = Path.GetFullPath(Path.Combine(Application.dataPath, "../DungeonTopDown.png"));
        File.WriteAllBytes(outputPath, tex.EncodeToPNG());
        Debug.Log($"[DungeonScreenshot] Screenshot saved to: {outputPath}");

        // Write companion JSON so analysis scripts can map world coords → pixels exactly.
        float orthoSize = cam.orthographicSize;
        float aspect    = (float)TextureWidth / TextureHeight;

        // ── Collect room dimensions and wall segment size ─────────────────────
        var roomInfos      = CollectRoomInfos();
        var wallSegmentSize = CollectWallSegmentSize(roomInfos);
        Debug.Log($"[DungeonScreenshot] Collected {roomInfos.Count} rooms, wall segment size={wallSegmentSize}");

        var sb = new StringBuilder();
        sb.AppendLine("{");
        sb.AppendLine($"  \"center_x\": {bounds.center.x},");
        sb.AppendLine($"  \"center_z\": {bounds.center.z},");
        sb.AppendLine($"  \"ortho_size\": {orthoSize},");
        sb.AppendLine($"  \"aspect\": {aspect},");
        sb.AppendLine($"  \"texture_width\": {TextureWidth},");
        sb.AppendLine($"  \"texture_height\": {TextureHeight},");
        sb.AppendLine($"  \"wall_segment\": {{");
        sb.AppendLine($"    \"width\": {wallSegmentSize.x},");
        sb.AppendLine($"    \"height\": {wallSegmentSize.y},");
        sb.AppendLine($"    \"thickness\": {wallSegmentSize.z}");
        sb.AppendLine($"  }},");
        sb.AppendLine($"  \"rooms\": [");
        for (int i = 0; i < roomInfos.Count; i++)
        {
            var (roomId, roomWidth, roomLength) = roomInfos[i];
            bool last = i == roomInfos.Count - 1;
            sb.AppendLine($"    {{");
            sb.AppendLine($"      \"id\": \"{roomId}\",");
            sb.AppendLine($"      \"width\": {roomWidth},");
            sb.AppendLine($"      \"length\": {roomLength}");
            sb.AppendLine(last ? $"    }}" : $"    }},");
        }
        sb.AppendLine($"  ]");
        sb.Append("}");
        string json = sb.ToString();
        string jsonPath = Path.ChangeExtension(outputPath, ".json");
        File.WriteAllText(jsonPath, json);
        Debug.Log($"[DungeonScreenshot] Camera params saved to: {jsonPath}");

        // ── 6. Cleanup ────────────────────────────────────────────────────────
        RenderTexture.active  = null;
        cam.targetTexture     = null;
        Object.DestroyImmediate(rt);
        Object.DestroyImmediate(tex);
        Object.DestroyImmediate(cameraGO);
        Object.DestroyImmediate(lightGO);

        RenderSettings.ambientMode   = savedAmbientMode;
        RenderSettings.ambientLight  = savedAmbientLight;
        RenderSettings.ambientSkyColor = savedAmbientSky;
        Debug.Log("[DungeonScreenshot] Cleanup complete; lighting restored.");

        // ── 7. Reveal in OS file browser ──────────────────────────────────────
        EditorUtility.RevealInFinder(outputPath);
    }

    private static Bounds CalculateSceneBounds()
    {
        Renderer[] renderers = Object.FindObjectsByType<Renderer>(FindObjectsSortMode.None);
        if (renderers.Length == 0)
            return default;

        Bounds b = renderers[0].bounds;
        for (int i = 1; i < renderers.Length; i++)
            b.Encapsulate(renderers[i].bounds);

        return b;
    }

    /// <summary>
    /// Returns (roomId, widthX, lengthZ) for every RoomExperiment in the scene,
    /// sorted by roomId. Width and length are derived from the floor-tile renderers
    /// in the room's "Floors" child.
    /// </summary>
    private static List<(string id, float width, float length)> CollectRoomInfos()
    {
        var result = new List<(string id, float width, float length)>();
        var rooms  = Object.FindObjectsByType<RoomExperiment>(FindObjectsSortMode.None);

        foreach (var room in rooms)
        {
            // Read the private serialised field via SerializedObject
            var so     = new SerializedObject(room);
            string rid = so.FindProperty("roomId")?.stringValue ?? room.gameObject.name;

            Transform floorsT = room.transform.Find("Floors");
            if (floorsT == null)
            {
                Debug.LogWarning($"[DungeonScreenshot] Room '{rid}' has no 'Floors' child — skipping dimensions.");
                result.Add((rid, 0f, 0f));
                continue;
            }

            Renderer[] renderers = floorsT.GetComponentsInChildren<Renderer>();
            if (renderers.Length == 0)
            {
                Debug.LogWarning($"[DungeonScreenshot] Room '{rid}' Floors child has no renderers — skipping dimensions.");
                result.Add((rid, 0f, 0f));
                continue;
            }

            Bounds fb = renderers[0].bounds;
            for (int i = 1; i < renderers.Length; i++)
                fb.Encapsulate(renderers[i].bounds);

            result.Add((rid, fb.size.x, fb.size.z));
            Debug.Log($"[DungeonScreenshot] Room '{rid}': width={fb.size.x} length={fb.size.z}");
        }

        result.Sort((a, b) => string.Compare(a.id, b.id, System.StringComparison.Ordinal));
        return result;
    }

    /// <summary>
    /// Returns the world-space size of one wall segment by inspecting the first wall
    /// renderer found inside any room's "Walls" child. Falls back to (4, 4, 1) if nothing
    /// is found (matching the prefab's default BoxCollider size).
    /// </summary>
    private static Vector3 CollectWallSegmentSize(List<(string id, float width, float length)> roomInfos)
    {
        var rooms = Object.FindObjectsByType<RoomExperiment>(FindObjectsSortMode.None);
        foreach (var room in rooms)
        {
            Transform wallsT = room.transform.Find("Walls");
            if (wallsT == null || wallsT.childCount == 0) continue;

            for (int i = 0; i < wallsT.childCount; i++)
            {
                Renderer r = wallsT.GetChild(i).GetComponentInChildren<Renderer>();
                if (r == null) continue;
                Vector3 s = r.bounds.size;
                // Wall panels are wider than they are thick — the thin axis is the depth
                return new Vector3(
                    Mathf.Max(s.x, s.z),   // width  (along the wall face)
                    s.y,                    // height
                    Mathf.Min(s.x, s.z)    // thickness
                );
            }
        }

        Debug.LogWarning("[DungeonScreenshot] Could not find a wall renderer — using fallback size (4, 4, 1).");
        return new Vector3(4f, 4f, 1f);
    }
}
