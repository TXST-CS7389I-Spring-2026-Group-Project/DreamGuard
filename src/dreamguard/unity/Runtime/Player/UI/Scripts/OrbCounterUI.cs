using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using DreamGuard.Orb;

namespace DreamGuard.Player.UI
{
    /// <summary>
    /// HUD label displaying "Room N: X/Y orbs collected".
    ///
    /// Attach to a GameObject that also has a TMP_Text component, inside a
    /// WorldSpace Canvas parented to the Player camera rig.
    ///
    /// Reacts to OrbManager.OnActiveManagerChanged so the label automatically
    /// updates when the player moves to a new room, without requiring a scene reload.
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
            OrbManager.OnActiveManagerChanged += OnActiveManagerChanged;
            // OrbManager.Start() has not fired yet on first enable; wait a frame.
            StartCoroutine(ConnectAfterManagerStart());
            DreamGuardLog.Log("[OrbCounterUI] OnEnable");
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            OrbManager.OnActiveManagerChanged -= OnActiveManagerChanged;
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
        /// Reacts to room transitions: unsubscribes from the old room's manager and
        /// subscribes to the new one immediately (no frame delay needed).
        /// </summary>
        private void OnActiveManagerChanged(OrbManager manager)
        {
            DreamGuardLog.Log($"[OrbCounterUI] OnActiveManagerChanged — room='{manager?.RoomDisplayName}'");
            Unsubscribe();
            if (manager == null) return;
            _subscribedManager = manager;
            _subscribedManager.OnOrbCountChanged += Refresh;
            Refresh(_subscribedManager.CollectedOrbs, _subscribedManager.TotalOrbs);
        }

        /// <summary>
        /// Waits one frame so that OrbManager.Start() has had a chance to run before
        /// we read from it. Skips if OnActiveManagerChanged already connected us.
        /// </summary>
        private IEnumerator ConnectAfterManagerStart()
        {
            yield return null;

            // Already connected via OnActiveManagerChanged — nothing to do.
            if (_subscribedManager != null) yield break;

            if (OrbManager.Instance == null)
            {
                DreamGuardLog.LogWarning("[OrbCounterUI] No active OrbManager yet — counter will show 0/0 until a room is entered");
                _label.text = "Room: 0/0 orbs collected";
                yield break;
            }

            _subscribedManager = OrbManager.Instance;
            _subscribedManager.OnOrbCountChanged += Refresh;
            Refresh(_subscribedManager.CollectedOrbs, _subscribedManager.TotalOrbs);
            DreamGuardLog.Log($"[OrbCounterUI] Subscribed via coroutine — {_subscribedManager.CollectedOrbs}/{_subscribedManager.TotalOrbs}");
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
            string prefix = (_subscribedManager != null && !string.IsNullOrEmpty(_subscribedManager.RoomDisplayName))
                ? _subscribedManager.RoomDisplayName
                : "Room";
            _label.text = $"{prefix}: {collected}/{total} orbs collected";
            DreamGuardLog.Log($"[OrbCounterUI] Refresh — {prefix}: {collected}/{total}");
        }
    }
}
