using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Editor utility: takes close-up perspective screenshots of the first door and
/// first orb found in the active scene using the Scene View's Shaded draw mode
/// (Handles.DrawCamera with DrawCameraMode.Textured).
/// Menu: Tools/DreamGuard/Screenshot Door and Orb
/// Output: &lt;project root&gt;/DoorScreenshot.png, OrbScreenshot.png
///
/// NOTE: Requires an open Scene View to provide a GUI context for Handles.DrawCamera.
/// Screenshots are saved on the next Scene View repaint (one frame delay).
/// </summary>
public static class ObjectScreenshot
{
    private const int   TextureWidth   = 1024;
    private const int   TextureHeight  = 1024;
    private const float FieldOfView    = 40f;
    private const float DistanceFactor = 2.5f;

    // ── Persistent render job state ───────────────────────────────────────────

    private struct Job
    {
        public Camera        camera;
        public GameObject    cameraGO;
        public RenderTexture rt;
        public string        outputPath;
    }

    private static List<Job>   s_jobs;
    private static GameObject  s_lightGO;
    private static AmbientMode s_savedAmbientMode;
    private static Color       s_savedAmbientLight;
    private static Color       s_savedAmbientSky;

    // ─────────────────────────────────────────────────────────────────────────

    [MenuItem("Tools/DreamGuard/Screenshot Door and Orb")]
    public static void TakeObjectScreenshots()
    {
        if (s_jobs != null)
        {
            Debug.LogWarning("[ObjectScreenshot] Already in progress — wait for the current run to finish.");
            return;
        }

        SceneView sv = SceneView.lastActiveSceneView;
        if (sv == null)
        {
            Debug.LogError("[ObjectScreenshot] No active Scene View found. Open the Scene View tab and try again.");
            return;
        }

        // ── 1. Override ambient lighting ──────────────────────────────────────
        s_savedAmbientMode  = RenderSettings.ambientMode;
        s_savedAmbientLight = RenderSettings.ambientLight;
        s_savedAmbientSky   = RenderSettings.ambientSkyColor;
        RenderSettings.ambientMode  = AmbientMode.Flat;
        RenderSettings.ambientLight = Color.white;

        // ── 2. Temporary directional light (45° from front-left above) ────────
        s_lightGO = new GameObject("_TempObjectLight");
        Light light = s_lightGO.AddComponent<Light>();
        light.type      = LightType.Directional;
        light.intensity = 1f;
        light.color     = Color.white;
        light.shadows   = LightShadows.None;
        s_lightGO.transform.rotation = Quaternion.Euler(45f, -45f, 0f);
        Debug.Log("[ObjectScreenshot] Temporary light created.");

        // ── 3. Find targets ───────────────────────────────────────────────────
        GameObject door = FindByNameContaining("Door");
        GameObject orb  = FindByComponentName("DreamGuardOrb");

        if (door == null) Debug.LogWarning("[ObjectScreenshot] No door found in active scene.");
        if (orb  == null) Debug.LogWarning("[ObjectScreenshot] No orb found in active scene.");

        if (door == null && orb == null)
        {
            Cleanup(restoreLighting: false);
            return;
        }

        // ── 4. Build one render job per target ────────────────────────────────
        s_jobs = new List<Job>();
        if (door != null) s_jobs.Add(BuildJob(door, "DoorScreenshot.png"));
        if (orb  != null) s_jobs.Add(BuildJob(orb,  "OrbScreenshot.png"));

        // ── 5. Queue rendering for the next Scene View repaint ────────────────
        SceneView.duringSceneGui += RenderInSceneView;
        sv.Repaint();
        Debug.Log("[ObjectScreenshot] Queued — screenshots will be saved on the next Scene View repaint.");
    }

    /// <summary>
    /// Runs inside a valid GUI context (SceneView repaint).
    /// Calls Handles.DrawCamera with DrawCameraMode.Textured (= Shaded) so the
    /// camera renders using the same draw mode as the Scene View.
    /// </summary>
    private static void RenderInSceneView(SceneView _)
    {
        SceneView.duringSceneGui -= RenderInSceneView;

        if (s_jobs == null) return;

        var savedActive = RenderTexture.active;

        foreach (Job job in s_jobs)
        {
            // Camera already has job.rt as targetTexture — Handles.DrawCamera
            // will render into it using the Shaded (Textured) draw mode.
            Handles.DrawCamera(
                new Rect(0, 0, TextureWidth, TextureHeight),
                job.camera,
                DrawCameraMode.Textured
            );

            // Read back from the render texture.
            RenderTexture.active = job.rt;
            var tex = new Texture2D(TextureWidth, TextureHeight, TextureFormat.RGB24, false);
            tex.ReadPixels(new Rect(0, 0, TextureWidth, TextureHeight), 0, 0);
            tex.Apply();

            File.WriteAllBytes(job.outputPath, tex.EncodeToPNG());
            Debug.Log($"[ObjectScreenshot] Saved (shaded): {job.outputPath}");

            Object.DestroyImmediate(tex);
        }

        RenderTexture.active = savedActive;

        var savedJobs = s_jobs;
        Cleanup(restoreLighting: true);
        s_jobs = null;

        foreach (var job in savedJobs)
            EditorUtility.RevealInFinder(job.outputPath);
    }

    // ─────────────────────────────────────────────────────────────────────────

    private static Job BuildJob(GameObject target, string fileName)
    {
        Bounds bounds = GetObjectBounds(target);
        if (bounds.size == Vector3.zero)
            bounds = new Bounds(target.transform.position, Vector3.one);

        Debug.Log($"[ObjectScreenshot] '{target.name}' bounds: center={bounds.center}, size={bounds.size}");

        float   size     = Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z);
        float   distance = size * DistanceFactor;
        Vector3 dir      = new Vector3(0f, 0.25f, -1f).normalized;
        Vector3 camPos   = bounds.center + dir * distance;

        var cameraGO = new GameObject("_TempObjectCamera");
        Camera cam   = cameraGO.AddComponent<Camera>();
        cam.orthographic    = false;
        cam.fieldOfView     = FieldOfView;
        cam.clearFlags      = CameraClearFlags.SolidColor;
        cam.backgroundColor = new Color(0.08f, 0.08f, 0.08f, 1f);
        cam.nearClipPlane   = 0.01f;
        cam.farClipPlane    = distance * 6f;
        cameraGO.transform.position = camPos;
        cameraGO.transform.LookAt(bounds.center);

        var rt = new RenderTexture(TextureWidth, TextureHeight, 24, RenderTextureFormat.ARGB32);
        cam.targetTexture = rt;

        Debug.Log($"[ObjectScreenshot] Camera for '{target.name}': pos={camPos}, distance={distance:F2}");

        return new Job
        {
            camera     = cam,
            cameraGO   = cameraGO,
            rt         = rt,
            outputPath = Path.GetFullPath(Path.Combine(Application.dataPath, "../" + fileName))
        };
    }

    private static void Cleanup(bool restoreLighting)
    {
        if (s_lightGO != null)
        {
            Object.DestroyImmediate(s_lightGO);
            s_lightGO = null;
        }

        if (s_jobs != null)
        {
            foreach (var job in s_jobs)
            {
                if (job.camera != null)  job.camera.targetTexture = null;
                if (job.rt     != null)  Object.DestroyImmediate(job.rt);
                if (job.cameraGO != null) Object.DestroyImmediate(job.cameraGO);
            }
        }

        if (restoreLighting)
        {
            RenderSettings.ambientMode     = s_savedAmbientMode;
            RenderSettings.ambientLight    = s_savedAmbientLight;
            RenderSettings.ambientSkyColor = s_savedAmbientSky;
            Debug.Log("[ObjectScreenshot] Cleanup complete; lighting restored.");
        }
    }

    private static GameObject FindByNameContaining(string namePart)
    {
        foreach (var go in Object.FindObjectsByType<GameObject>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            if (go.name.IndexOf(namePart, System.StringComparison.OrdinalIgnoreCase) >= 0)
                return go;
        }
        return null;
    }

    private static GameObject FindByComponentName(string typeName)
    {
        foreach (var mb in Object.FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            if (mb.GetType().Name == typeName)
                return mb.gameObject;
        }
        return null;
    }

    private static Bounds GetObjectBounds(GameObject go)
    {
        Renderer[] renderers = go.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0)
            return new Bounds(go.transform.position, Vector3.zero);

        Bounds b = renderers[0].bounds;
        for (int i = 1; i < renderers.Length; i++)
            b.Encapsulate(renderers[i].bounds);
        return b;
    }
}
