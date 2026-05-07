using System.IO;
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
}
