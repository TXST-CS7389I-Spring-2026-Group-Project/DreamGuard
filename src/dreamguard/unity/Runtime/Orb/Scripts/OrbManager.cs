using System;
using System.Collections.Generic;
using UnityEngine;

namespace DreamGuard.Orb
{
    /// <summary>
    /// Scene-level singleton that tracks all DreamGuardOrb instances.
    ///
    /// Populated at Start via FindObjectsByType. Each DreamGuardOrb calls
    /// <see cref="NotifyOrbCollected"/> when it is collected, keeping the
    /// count and remaining list up to date.
    ///
    /// This component is NOT DontDestroyOnLoad — a new instance is expected
    /// each time a scene containing orbs is loaded.
    /// </summary>
    public class OrbManager : MonoBehaviour
    {
        public static OrbManager Instance { get; private set; }

        public int TotalOrbs { get; private set; }
        public int CollectedOrbs { get; private set; }

        /// <summary>Fired whenever CollectedOrbs or TotalOrbs changes. Args: (collected, total).</summary>
        public event Action<int, int> OnOrbCountChanged;

        private readonly List<DreamGuardOrb> _remainingOrbs = new();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                DreamGuardLog.LogWarning("[OrbManager] Duplicate instance — destroying this one");
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DreamGuardLog.Log("[OrbManager] Awake");
        }

        private void Start()
        {
            var allOrbs = FindObjectsByType<DreamGuardOrb>(FindObjectsSortMode.None);
            TotalOrbs = allOrbs.Length;
            CollectedOrbs = 0;
            _remainingOrbs.Clear();
            _remainingOrbs.AddRange(allOrbs);
            DreamGuardLog.Log($"[OrbManager] Start — found {TotalOrbs} orb(s) in scene");
            OnOrbCountChanged?.Invoke(CollectedOrbs, TotalOrbs);
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
                DreamGuardLog.Log("[OrbManager] OnDestroy");
            }
        }

        /// <summary>Called by DreamGuardOrb immediately before it destroys itself.</summary>
        public void NotifyOrbCollected(DreamGuardOrb orb)
        {
            _remainingOrbs.Remove(orb);
            CollectedOrbs++;
            DreamGuardLog.Log($"[OrbManager] Orb collected — {CollectedOrbs}/{TotalOrbs} remaining={_remainingOrbs.Count}");
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
