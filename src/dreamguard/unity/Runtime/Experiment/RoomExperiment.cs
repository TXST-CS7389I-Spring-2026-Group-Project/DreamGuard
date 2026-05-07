using System.Collections;
using UnityEngine;
using DreamGuard;
using DreamGuard.Orb;
using DreamGuard.Player.UI;

namespace DreamGuard.Experiment
{
    /// <summary>
    /// Per-room experiment controller for the 4-room DreamGuard study design.
    ///
    /// Place one instance on each Room GameObject (Room1 … Room4) in the Dungeon scene.
    /// Wire the inspector fields for each room, then the script handles:
    ///
    ///   • Room entry detection via trigger collider (or on Start for Room 1).
    ///   • Closing the entrance door behind the player to prevent backtracking.
    ///   • Activating this room's OrbManager so UI components update automatically.
    ///   • Firing the room's passthrough technique at the halfway point (2 orbs).
    ///   • Opening the exit door and redirecting the HUD arrow once all orbs are collected.
    ///   • Writing all required study log events via StudyLogger.
    ///
    /// Logged events (in study.csv):
    ///   ROOM_ENTER, CONDITION_BLOCK_START, ROOM_HALFWAY, TRIGGER, ROOM_COMPLETE
    /// Logged events (in rooms.csv):
    ///   ENTER (on room entry), EXIT (on room complete)
    ///
    /// Door wiring:
    ///   Assign the Collider component of the physical door blocker — the collider that
    ///   stops the player from passing through. The entrance blocker starts disabled
    ///   (so the player can walk in) and is enabled when they enter. The exit blocker
    ///   starts enabled (locked) and is disabled when the room is complete.
    ///
    /// Passthrough technique:
    ///   Assign the MonoBehaviour that implements IDreamGuardPassthrough.
    ///   Room 1 → leave null for Meta Quest default passthrough (full background);
    ///   Room 2 → DreamGuardWindowedPassthrough; Room 3 → PassthroughSphere;
    ///   Room 4 → DetectionBasedPassthrough.
    ///   The technique is enabled at the halfway mark (after the 2nd orb).
    /// </summary>
    public class RoomExperiment : MonoBehaviour
    {
        [Header("Room Identity")]
        [Tooltip("Short identifier written to the log, e.g. 'Room1'.")]
        [SerializeField] private string roomId = "Room1";

        [Tooltip("Human-readable condition name written to CONDITION_BLOCK_START, e.g. 'default'.")]
        [SerializeField] private string conditionName = "default";

        [Header("Entry")]
        [Tooltip("Enable for Room 1 only. Triggers room entry automatically on Start " +
                 "(one frame delayed) instead of waiting for a physics trigger.")]
        [SerializeField] private bool enterOnStart = false;

        [Header("References")]
        [Tooltip("The OrbManager component on this room's GameObject (or its child).")]
        [SerializeField] private OrbManager orbManager;

        [Tooltip("Collider that blocks the entrance after the player walks in. " +
                 "Should be disabled by default so the player can enter. Leave null for Room 1.")]
        [SerializeField] private Collider entranceDoorBlocker;

        [Tooltip("Collider that blocks the exit until all orbs are collected. " +
                 "Should be enabled by default (locked). Leave null for Room 4.")]
        [SerializeField] private Collider exitDoorBlocker;

        [Tooltip("Transform the HUD arrow points toward after this room is complete, " +
                 "guiding the player to the next room. Leave null for Room 4.")]
        [SerializeField] private Transform nextRoomTarget;

        [Header("Passthrough Technique")]
        [Tooltip("MonoBehaviour implementing IDreamGuardPassthrough. Activated at the halfway " +
                 "point (after the 2nd orb). Leave null for Room 1 (default passthrough).")]
        [SerializeField] private MonoBehaviour passthroughTechnique;

        private bool _entered;
        private bool _halfway;
        private bool _complete;

        private void Awake()
        {
            DreamGuardLog.Log($"[RoomExperiment] Awake — roomId={roomId} conditionName={conditionName}");
        }

        private void Start()
        {
            DreamGuardLog.Log($"[RoomExperiment] Start — roomId={roomId} enterOnStart={enterOnStart}");

            if (orbManager == null)
                DreamGuardLog.LogWarning($"[RoomExperiment] orbManager not assigned — roomId={roomId}");

            if (passthroughTechnique != null && !(passthroughTechnique is IDreamGuardPassthrough))
                DreamGuardLog.LogWarning($"[RoomExperiment] passthroughTechnique does not implement IDreamGuardPassthrough — roomId={roomId}");

            if (enterOnStart)
                StartCoroutine(EnterRoomAfterStart());
        }

        private void OnDestroy()
        {
            if (orbManager != null)
                orbManager.OnOrbCountChanged -= OnOrbCountChanged;
        }

        // ── Entry ─────────────────────────────────────────────────────────────────

        /// <summary>
        /// Waits one frame so StudyInputHandler.Start() has had a chance to call
        /// StudyLogger.BeginSession() before we log ROOM_ENTER.
        /// </summary>
        private IEnumerator EnterRoomAfterStart()
        {
            yield return null;
            EnterRoom();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            if (_entered) return;
            DreamGuardLog.Log($"[RoomExperiment] OnTriggerEnter — roomId={roomId}");
            EnterRoom();
        }

        private void EnterRoom()
        {
            if (_entered) return;
            _entered = true;

            DreamGuardLog.Log($"[RoomExperiment] EnterRoom — roomId={roomId}");

            // Close the entrance door so the player cannot go back.
            if (entranceDoorBlocker != null)
            {
                entranceDoorBlocker.enabled = true;
                DreamGuardLog.Log($"[RoomExperiment] Entrance door closed — roomId={roomId}");
            }

            // Activate this room's orb manager as the global current room.
            if (orbManager != null)
            {
                orbManager.ActivateAsCurrentRoom();
                orbManager.OnOrbCountChanged += OnOrbCountChanged;
            }

            // Clear the arrow fallback — the arrow should now track this room's orbs.
            OrbArrowUI.Instance?.SetFallbackTarget(null);

            // Log events.
            StudyLogger.LogRoomEnter(roomId);
            StudyLogger.LogConditionBlockStart(roomId, conditionName);
        }

        // ── Orb progress ──────────────────────────────────────────────────────────

        private void OnOrbCountChanged(int collected, int total)
        {
            // Halfway: ceil(total / 2) — for 4 orbs this fires at 2 collected.
            if (!_halfway && total > 0 && collected >= (total + 1) / 2)
            {
                _halfway = true;
                DreamGuardLog.Log($"[RoomExperiment] Halfway reached — roomId={roomId} {collected}/{total}");
                StudyLogger.LogRoomHalfway(roomId, collected, total);
                TriggerPassthrough();
            }

            if (!_complete && total > 0 && collected >= total)
            {
                _complete = true;
                DreamGuardLog.Log($"[RoomExperiment] Room complete — roomId={roomId} {collected}/{total}");
                StudyLogger.LogRoomComplete(roomId);

                // Open the exit door.
                if (exitDoorBlocker != null)
                {
                    exitDoorBlocker.enabled = false;
                    DreamGuardLog.Log($"[RoomExperiment] Exit door opened — roomId={roomId}");
                }

                // Redirect the HUD arrow toward the next room.
                if (nextRoomTarget != null)
                {
                    OrbArrowUI.Instance?.SetFallbackTarget(nextRoomTarget);
                    DreamGuardLog.Log($"[RoomExperiment] Arrow fallback set to '{nextRoomTarget.name}' — roomId={roomId}");
                }
            }
        }

        // ── Passthrough ───────────────────────────────────────────────────────────

        private void TriggerPassthrough()
        {
            if (passthroughTechnique == null)
            {
                DreamGuardLog.Log($"[RoomExperiment] No passthrough technique assigned — roomId={roomId} (default passthrough stays active)");
                return;
            }

            if (passthroughTechnique is IDreamGuardPassthrough pt)
            {
                DreamGuardLog.Log($"[RoomExperiment] Triggering passthrough — roomId={roomId} technique={passthroughTechnique.GetType().Name}");
                // Ensure the technique's GameObject is active before calling SetEnabled.
                if (!passthroughTechnique.gameObject.activeSelf)
                    passthroughTechnique.gameObject.SetActive(true);
                pt.SetEnabled(true);
                StudyLogger.LogTrigger(conditionName, $"room_id={roomId}");
            }
            else
            {
                DreamGuardLog.LogWarning($"[RoomExperiment] passthroughTechnique '{passthroughTechnique.GetType().Name}' " +
                    $"does not implement IDreamGuardPassthrough — roomId={roomId}");
            }
        }
    }
}
