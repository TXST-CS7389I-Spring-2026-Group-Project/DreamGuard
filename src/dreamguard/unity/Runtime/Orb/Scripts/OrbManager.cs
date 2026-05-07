using System;
using System.Collections.Generic;
using UnityEngine;

namespace DreamGuard.Orb
{
    /// <summary>
    /// Per-room orb tracker. One instance lives on each Room GameObject.
    ///
    /// On Start each manager scans its <see cref="orbsParent"/> subtree (or its own
    /// transform if that field is null) for <see cref="DreamGuardOrb"/> children.
    ///
    /// Call <see cref="ActivateAsCurrentRoom"/> (from <c>RoomExperiment</c>) to make
    /// this the globally-visible instance. The static <see cref="Instance"/> and
    /// <see cref="OnActiveManagerChanged"/> event let UI components (OrbCounterUI,
    /// OrbArrowUI) react without hard references to individual rooms.
    /// </summary>
    public class OrbManager : MonoBehaviour
    {
        public static OrbManager Instance { get; private set; }

        /// <summary>Fired when a new room becomes active. Arg: the new active manager.</summary>
        public static event Action<OrbManager> OnActiveManagerChanged;

        [Tooltip("Root transform to scan for DreamGuardOrb children. " +
                 "Assign the 'Orbs' child of the room. Falls back to this transform if null.")]
        [SerializeField] private Transform orbsParent;

        [Tooltip("Display name shown in the HUD counter, e.g. 'Room 1'.")]
        [SerializeField] private string roomDisplayName = "Room";

        public string RoomDisplayName => roomDisplayName;
        public int TotalOrbs { get; private set; }
        public int CollectedOrbs { get; private set; }

        /// <summary>Fired whenever CollectedOrbs or TotalOrbs changes. Args: (collected, total).</summary>
        public event Action<int, int> OnOrbCountChanged;

        private readonly List<DreamGuardOrb> _remainingOrbs = new();

        private void Awake()
        {
            // Multiple OrbManagers coexist in the scene (one per room).
            // Instance is set explicitly via ActivateAsCurrentRoom — not in Awake.
            DreamGuardLog.Log($"[OrbManager] Awake — room='{roomDisplayName}'");
        }

        /// <summary>
        /// Sets the orbs root and display name before Start fires.
        /// Called by RoomExperiment immediately after adding this component in Awake.
        /// </summary>
        public void Initialize(Transform orbsRoot, string displayName)
        {
            orbsParent    = orbsRoot;
            roomDisplayName = displayName;
            DreamGuardLog.Log($"[OrbManager] Initialize — room='{roomDisplayName}'");
        }

        private void Start()
        {
            var root = orbsParent != null ? orbsParent : transform;
            var allOrbs = root.GetComponentsInChildren<DreamGuardOrb>();
            TotalOrbs = allOrbs.Length;
            CollectedOrbs = 0;
            _remainingOrbs.Clear();
            _remainingOrbs.AddRange(allOrbs);
            DreamGuardLog.Log($"[OrbManager] Start — room='{roomDisplayName}' found {TotalOrbs} orb(s)");
            // Do NOT fire OnOrbCountChanged here — wait until ActivateAsCurrentRoom is called.
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
                DreamGuardLog.Log($"[OrbManager] OnDestroy — was active instance room='{roomDisplayName}'");
            }
        }

        /// <summary>
        /// Makes this manager the active instance visible to UI components.
        /// Call from RoomExperiment when the player enters this room.
        /// Fires <see cref="OnActiveManagerChanged"/> and an initial <see cref="OnOrbCountChanged"/>.
        /// </summary>
        public void ActivateAsCurrentRoom()
        {
            Instance = this;
            DreamGuardLog.Log($"[OrbManager] ActivateAsCurrentRoom — room='{roomDisplayName}' {CollectedOrbs}/{TotalOrbs}");
            OnActiveManagerChanged?.Invoke(this);
            OnOrbCountChanged?.Invoke(CollectedOrbs, TotalOrbs);
        }

        /// <summary>Called by DreamGuardOrb immediately before it destroys itself.</summary>
        public void NotifyOrbCollected(DreamGuardOrb orb)
        {
            _remainingOrbs.Remove(orb);
            CollectedOrbs++;
            DreamGuardLog.Log($"[OrbManager] Orb collected — room='{roomDisplayName}' {CollectedOrbs}/{TotalOrbs} remaining={_remainingOrbs.Count}");
            OnOrbCountChanged?.Invoke(CollectedOrbs, TotalOrbs);
        }

        /// <summary>
        /// Read-only view of uncollected orbs. Entries are removed when
        /// NotifyOrbCollected is called; no null entries are expected here
        /// because notification fires before Destroy.
        /// </summary>
        public IReadOnlyList<DreamGuardOrb> RemainingOrbs => _remainingOrbs;
    }
}
