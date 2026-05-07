using System.Collections;
using UnityEngine;
using DreamGuard;
using DreamGuard.Orb;
using DreamGuard.Player.UI;

namespace DreamGuard.Experiment
{
    /// <summary>
    /// Passthrough techniques available in the study.
    /// Matches the concrete class names on the Player prefab; resolved at runtime via
    /// FindObjectsByType — no direct assembly reference to passthrough assemblies required.
    /// </summary>
    public enum PassthroughTechniqueType
    {
        /// <summary>No custom passthrough (Room 1 — Meta Quest default passthrough stays active).</summary>
        None,
        /// <summary>DreamGuardWindowedPassthrough — window in the centre of the FOV.</summary>
        Windowed,
        /// <summary>PassthroughSphere — depth-based sphere reveal.</summary>
        Sphere,
        /// <summary>DetectionBasedPassthrough — YOLO detection reveals only detected objects.</summary>
        Detection,
        /// <summary>DreamGuardGridPassthrough — grid overlay technique.</summary>
        Grid,
        /// <summary>DreamGuardVerticalFold — vertical fold technique.</summary>
        VerticalFold,
        /// <summary>DreamGuardMetaDefaultPassthrough — forces the OS Guardian boundary grid on command (best-effort; deprecated API).</summary>
        MetaDefault,
    }

    /// <summary>
    /// Per-room experiment controller for the 4-room DreamGuard study design.
    ///
    /// Place one instance on each Room&lt;n&gt; GameObject in the Dungeon scene.
    /// The script creates and configures its own OrbManager at startup — no manual wiring needed.
    ///
    /// Inspector setup per room:
    ///   • roomId / conditionName — logged to study.csv
    ///   • enterOnStart — true for Room 1 only
    ///   • entranceDoorBlocker — Collider that closes behind the player (null for Room 1)
    ///   • exitDoorBlocker — Collider that unlocks when all orbs are collected (null for Room 4)
    ///   • nextRoomTarget — Transform the HUD arrow points toward after room completes (null for Room 4)
    ///   • passthroughType — which technique fires at the halfway point
    ///
    /// Door setup:
    ///   entranceDoorBlocker starts <b>disabled</b> (player can walk through); enabled on enter.
    ///   exitDoorBlocker starts <b>active</b> (locked); deactivated (disappears) when room is complete.
    ///
    /// Logged events (study.csv): ROOM_ENTER, CONDITION_BLOCK_START, ROOM_HALFWAY, TRIGGER, ROOM_COMPLETE
    /// Logged events (rooms.csv): ENTER on room entry, EXIT on room complete
    /// </summary>
    [AddComponentMenu("DreamGuard/Room Experiment")]
    public class RoomExperiment : MonoBehaviour
    {
        [Header("Room Identity")]
        [Tooltip("Short identifier written to the log (e.g. 'Room1').")]
        [SerializeField] private string roomId = "Room1";

        [Tooltip("Human-readable condition written to CONDITION_BLOCK_START (e.g. 'default', 'windowed').")]
        [SerializeField] private string conditionName = "default";

        [Tooltip("Override the label shown in the HUD counter (e.g. 'Room 1'). " +
                 "Defaults to roomId if left blank.")]
        [SerializeField] private string roomDisplayName = "";

        [Header("Entry")]
        [Tooltip("Enable for Room 1 only. Triggers room entry automatically on Start " +
                 "(one frame delayed) rather than waiting for a physics trigger.")]
        [SerializeField] private bool enterOnStart = false;

        [Header("Doors")]
        [Tooltip("Trigger collider that fires when the player enters this room. " +
                 "Place a BoxCollider (Is Trigger = true) on this GameObject, size it to cover " +
                 "the entrance, and assign it here. Leave null for Room 1 (which uses enterOnStart). " +
                 "Automatically disabled on entry so it cannot re-fire.")]
        [SerializeField] private Collider entranceTrigger;

        [Tooltip("Collider that blocks the entrance after the player walks in. " +
                 "Starts inactive (whole GameObject off). Reactivated on enter. Leave null for Room 1.")]
        [SerializeField] private Collider entranceDoorBlocker;

        [Tooltip("Collider that blocks the exit until all orbs are collected. " +
                 "Must be ENABLED/ACTIVE by default (locked). Deactivated (disappears) when room is complete. Leave null for Room 4.")]
        [SerializeField] private Collider exitDoorBlocker;

        [Tooltip("Transform the HUD arrow points toward after this room is complete, " +
                 "guiding the player to the next room's entrance. Leave null for Room 4.")]
        [SerializeField] private Transform nextRoomTarget;

        [Header("Passthrough Technique")]
        [Tooltip("Which passthrough technique fires at the halfway mark (after the 2nd orb). " +
                 "The matching component is located on the Player prefab at runtime — " +
                 "no scene cross-reference needed. Use None for Room 1 (default passthrough).")]
        [SerializeField] private PassthroughTechniqueType passthroughType = PassthroughTechniqueType.None;

        [Tooltip("Seconds before the passthrough technique is automatically disabled after triggering. " +
                 "Detection needs extra time (~3s) for GPU/camera warmup before inference runs — " +
                 "use 10–12s for Detection, 4s for all other techniques.")]
        [SerializeField] private float passthroughDuration = 4f;

        // ── Runtime state ──────────────────────────────────────────────────────

        private OrbManager _orbManager;
        private IDreamGuardPassthrough _resolvedTechnique;
        private MonoBehaviour _resolvedTechniqueMB;

        private bool _entered;
        private bool _halfway;
        private bool _complete;

        // ── Unity lifecycle ────────────────────────────────────────────────────

        private void Awake()
        {
            DreamGuardLog.Log($"[RoomExperiment] Awake — roomId={roomId}");
            CreateOrbManager();
        }

        private void Start()
        {
            DreamGuardLog.Log($"[RoomExperiment] Start — roomId={roomId} passthroughType={passthroughType}");
            CachePassthroughTechnique();

            if (enterOnStart)
                StartCoroutine(EnterRoomAfterStart());
        }

        private void OnDestroy()
        {
            if (_orbManager != null)
                _orbManager.OnOrbCountChanged -= OnOrbCountChanged;
        }

        // ── OrbManager auto-creation ───────────────────────────────────────────

        private void CreateOrbManager()
        {
            // Re-use an existing component (e.g. if the scene was saved with one already attached).
            _orbManager = GetComponent<OrbManager>();
            if (_orbManager == null)
                _orbManager = gameObject.AddComponent<OrbManager>();

            // Orbs live under a child named "Orbs"; fall back to this transform.
            var orbsRoot = transform.Find("Orbs") ?? transform;
            var label    = string.IsNullOrEmpty(roomDisplayName) ? roomId : roomDisplayName;
            _orbManager.Initialize(orbsRoot, label, roomId);

            DreamGuardLog.Log($"[RoomExperiment] OrbManager ready — root='{orbsRoot.name}' label='{label}'");
        }

        // ── Passthrough resolution ─────────────────────────────────────────────

        /// <summary>
        /// Finds the passthrough component on the Player prefab at startup.
        /// Looks for a MonoBehaviour that implements IDreamGuardPassthrough and whose
        /// class name matches the selected <see cref="passthroughType"/>.
        /// Uses FindObjectsInactive.Include so disabled components are found too.
        /// </summary>
        private void CachePassthroughTechnique()
        {
            if (passthroughType == PassthroughTechniqueType.None)
            {
                DreamGuardLog.Log($"[RoomExperiment] PassthroughType.None — no technique will fire — roomId={roomId}");
                return;
            }

            string targetTypeName = PassthroughTypeToClassName(passthroughType);

            foreach (var mb in FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                if (mb is IDreamGuardPassthrough pt && mb.GetType().Name == targetTypeName)
                {
                    _resolvedTechnique   = pt;
                    _resolvedTechniqueMB = mb;
                    DreamGuardLog.Log($"[RoomExperiment] Resolved passthrough '{targetTypeName}' on '{mb.gameObject.name}' — roomId={roomId}");
                    return;
                }
            }

            DreamGuardLog.LogWarning($"[RoomExperiment] Could not find passthrough component '{targetTypeName}' in scene — roomId={roomId}");
        }

        private static string PassthroughTypeToClassName(PassthroughTechniqueType type) => type switch
        {
            PassthroughTechniqueType.Windowed    => "DreamGuardWindowedPassthrough",
            PassthroughTechniqueType.Sphere      => "PassthroughSphere",
            PassthroughTechniqueType.Detection   => "DetectionBasedPassthrough",
            PassthroughTechniqueType.Grid        => "DreamGuardGridPassthrough",
            PassthroughTechniqueType.VerticalFold  => "DreamGuardVerticalFold",
            PassthroughTechniqueType.MetaDefault   => "DreamGuardMetaDefaultPassthrough",
            _                                      => "",
        };

        // ── Entry ──────────────────────────────────────────────────────────────

        /// <summary>
        /// Waits one frame so StudyInputHandler.Start() has had a chance to call
        /// StudyLogger.BeginSession() before ROOM_ENTER is logged.
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

            if (entranceTrigger != null)
            {
                entranceTrigger.enabled = false;
                DreamGuardLog.Log($"[RoomExperiment] Entrance trigger disabled — roomId={roomId}");
            }

            if (entranceDoorBlocker != null)
            {
                entranceDoorBlocker.gameObject.SetActive(true);
                DreamGuardLog.Log($"[RoomExperiment] Entrance door closed — roomId={roomId}");
            }

            _orbManager.ActivateAsCurrentRoom();
            _orbManager.OnOrbCountChanged += OnOrbCountChanged;

            // Arrow tracks this room's orbs now; clear any previous fallback.
            OrbArrowUI.Instance?.SetFallbackTarget(null);

            StudyLogger.LogRoomEnter(roomId);
            StudyLogger.LogConditionBlockStart(roomId, conditionName);
        }

        // ── Orb progress ───────────────────────────────────────────────────────

        private void OnOrbCountChanged(int collected, int total)
        {
            // Halfway = ceil(total / 2): for 4 orbs this is 2.
            if (!_halfway && total > 0 && collected >= (total + 1) / 2)
            {
                _halfway = true;
                DreamGuardLog.Log($"[RoomExperiment] Halfway — roomId={roomId} {collected}/{total}");
                StudyLogger.LogRoomHalfway(roomId, collected, total);
                TriggerPassthrough();
            }

            if (!_complete && total > 0 && collected >= total)
            {
                _complete = true;
                DreamGuardLog.Log($"[RoomExperiment] Complete — roomId={roomId} {collected}/{total}");
                StudyLogger.LogRoomComplete(roomId);

                if (exitDoorBlocker != null)
                {
                    exitDoorBlocker.gameObject.SetActive(false);
                    DreamGuardLog.Log($"[RoomExperiment] Exit door disappeared — roomId={roomId}");
                }

                if (exitDoorBlocker != null)
                {
                    OrbArrowUI.Instance?.SetFallbackTarget(exitDoorBlocker.transform);
                    DreamGuardLog.Log($"[RoomExperiment] Arrow pointing to exit door '{exitDoorBlocker.name}' — roomId={roomId}");
                }
            }
        }

        // ── Passthrough ────────────────────────────────────────────────────────

        private void TriggerPassthrough()
        {
            if (passthroughType == PassthroughTechniqueType.None)
            {
                DreamGuardLog.Log($"[RoomExperiment] PassthroughType.None — skipping — roomId={roomId}");
                return;
            }

            if (_resolvedTechnique == null)
            {
                DreamGuardLog.LogWarning($"[RoomExperiment] No passthrough resolved for '{passthroughType}' — roomId={roomId}");
                return;
            }

            // Mirror DreamGuardMenu.ActivateTechnique: activate the GO once if needed,
            // then call SetEnabled. This ensures OVRPassthroughLayer initialises correctly.
            if (!_resolvedTechniqueMB.gameObject.activeSelf)
                _resolvedTechniqueMB.gameObject.SetActive(true);

            _resolvedTechnique.SetEnabled(true);
            StudyLogger.LogTrigger(conditionName, $"room_id={roomId}");
            DreamGuardLog.Log($"[RoomExperiment] Passthrough triggered — type={passthroughType} roomId={roomId}");

            StartCoroutine(DisablePassthroughAfterDelay(passthroughDuration));
        }

        private IEnumerator DisablePassthroughAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);

            if (_resolvedTechnique == null) yield break;

            _resolvedTechnique.SetEnabled(false);
            DreamGuardLog.Log($"[RoomExperiment] Passthrough disabled after {delay}s — type={passthroughType} roomId={roomId}");
        }
    }
}
