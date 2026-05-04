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

        private void Start()
        {
            StartCoroutine(LoadAsync());
        }

        private IEnumerator LoadAsync()
        {
            // Wait a few frames for OVRManager and passthrough to initialize
            // before starting the scene load, so Boot renders passthrough instead
            // of a black frame during the transition.
            for (int i = 0; i < 3; i++)
                yield return null;

            // Limit background loading to Low priority so it doesn't starve the
            // XR compositor. The Dungeon scene is large; without this the loader
            // consumes enough CPU that no frames are submitted and Meta OS kills
            // the session (~5 s timeout → "not responding").
            var prevPriority = Application.backgroundLoadingPriority;
            Application.backgroundLoadingPriority = ThreadPriority.Low;

            var op = SceneManager.LoadSceneAsync(_firstScene);
            // Hold the new scene inactive until fully loaded so the XR compositor
            // keeps receiving frames from Boot (passthrough stays visible).
            op.allowSceneActivation = false;
            while (op.progress < 0.9f)
                yield return null;
            op.allowSceneActivation = true;

            Application.backgroundLoadingPriority = prevPriority;
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
