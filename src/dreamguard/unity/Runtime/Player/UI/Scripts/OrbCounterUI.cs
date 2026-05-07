using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using DreamGuard.Orb;

namespace DreamGuard.Player.UI
{
    /// <summary>
    /// HUD label displaying "Collected X/Y Orbs".
    ///
    /// Attach to a GameObject that also has a TMP_Text component, inside a
    /// WorldSpace Canvas parented to the Player camera rig.
    ///
    /// Automatically reconnects to the scene's OrbManager after each scene load,
    /// since the Player persists (DontDestroyOnLoad) but OrbManager is scene-local.
    /// </summary>
    [RequireComponent(typeof(TMP_Text))]
    public class OrbCounterUI : MonoBehaviour
    {
        private TMP_Text _label;
        private OrbManager _subscribedManager;

        private void Awake()
        {
            _label = GetComponent<TMP_Text>();
            DreamGuardLog.Log("[OrbCounterUI] Awake");
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            // OrbManager.Start() has not fired yet on first enable; wait a frame.
            StartCoroutine(ConnectAfterManagerStart());
            DreamGuardLog.Log("[OrbCounterUI] OnEnable");
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Unsubscribe();
            DreamGuardLog.Log("[OrbCounterUI] OnDisable");
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            DreamGuardLog.Log($"[OrbCounterUI] OnSceneLoaded '{scene.name}' — reconnecting to OrbManager");
            Unsubscribe();
            StartCoroutine(ConnectAfterManagerStart());
        }

        /// <summary>
        /// Waits one frame so that OrbManager.Start() has had a chance to run and
        /// populate TotalOrbs before we read from it.
        /// </summary>
        private IEnumerator ConnectAfterManagerStart()
        {
            yield return null;

            if (OrbManager.Instance == null)
            {
                DreamGuardLog.LogWarning("[OrbCounterUI] No OrbManager in scene — counter will show 0/0");
                _label.text = "Collected 0/0 Orbs";
                yield break;
            }

            _subscribedManager = OrbManager.Instance;
            _subscribedManager.OnOrbCountChanged += Refresh;
            Refresh(_subscribedManager.CollectedOrbs, _subscribedManager.TotalOrbs);
            DreamGuardLog.Log($"[OrbCounterUI] Subscribed — {_subscribedManager.CollectedOrbs}/{_subscribedManager.TotalOrbs}");
        }

        private void Unsubscribe()
        {
            if (_subscribedManager != null)
            {
                _subscribedManager.OnOrbCountChanged -= Refresh;
                _subscribedManager = null;
            }
        }

        private void Refresh(int collected, int total)
        {
            _label.text = $"Collected {collected}/{total} Orbs";
            DreamGuardLog.Log($"[OrbCounterUI] Refresh — {collected}/{total}");
        }
    }
}
