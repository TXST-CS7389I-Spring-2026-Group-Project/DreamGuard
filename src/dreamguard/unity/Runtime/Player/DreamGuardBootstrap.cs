using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DreamGuard
{
    /// <summary>
    /// Placed in the Boot scene (build index 0) alongside the Player prefab.
    /// Once the Player has initialized its persistent OVR rig, asynchronously
    /// loads the configured first game scene so the XR compositor keeps
    /// receiving frames during the load (prevents "Not Responding" on Quest).
    /// </summary>
    public class DreamGuardBootstrap : MonoBehaviour
    {
        [Tooltip("Name of the first game scene to load after boot.")]
        [SerializeField] private string _firstScene = "Dungeon";

        private void Awake()
        {
            LogcatLog.Initialize();
            DreamGuardLog.Log($"[DreamGuardBootstrap] Awake — firstScene='{_firstScene}'");
        }

        private void Start()
        {
            DreamGuardLog.Log("[DreamGuardBootstrap] Start — launching LoadAsync");
            StartCoroutine(LoadAsync());
        }

        private IEnumerator LoadAsync()
        {
            DreamGuardLog.Log("[DreamGuardBootstrap] Waiting for VR focus");
            // Wait until the app has XR focus before loading. Without this,
            // Build-and-Run launches the app unfocused (headset not yet worn /
            // Quest home hasn't handed off), no frames are submitted, and the
            // Meta compositor kills the session after ~5 s ("not responding").
            while (!OVRPlugin.hasVrFocus)
                yield return null;
            DreamGuardLog.Log("[DreamGuardBootstrap] VR focus acquired");

            // Wait a few frames for OVRManager and passthrough to initialize
            // before starting the scene load, so Boot renders passthrough instead
            // of a black frame during the transition.
            // for (int i = 0; i < 3; i++)
            //     yield return null;

            DreamGuardLog.Log($"[DreamGuardBootstrap] Starting async load of '{_firstScene}'");
            var op = SceneManager.LoadSceneAsync(_firstScene);
            // Hold the new scene inactive until fully loaded so the XR compositor
            // keeps receiving frames from Boot (passthrough stays visible).
            op.allowSceneActivation = false;
            while (op.progress < 0.9f)
                yield return null;
            DreamGuardLog.Log($"[DreamGuardBootstrap] Activating '{_firstScene}'  " +
                $"SOFT={Shader.IsKeywordEnabled("META_DEPTH_SOFT_OCCLUSION_ENABLED")}  " +
                $"HARD={Shader.IsKeywordEnabled("META_DEPTH_HARD_OCCLUSION_ENABLED")}");
            op.allowSceneActivation = true;

        }

#if UNITY_EDITOR
        // When pressing Play from a non-Boot scene in the Editor, redirect to
        // scene 0 (Boot) so the Player prefab initialises before game scenes load.
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void EnsureBootFirst()
        {
            if (SceneManager.GetActiveScene().buildIndex != 0)
                SceneManager.LoadScene(0);
        }
#endif
    }
}
