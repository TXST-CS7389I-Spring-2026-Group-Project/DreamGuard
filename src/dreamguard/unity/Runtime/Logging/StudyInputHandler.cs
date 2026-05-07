using UnityEngine;

namespace DreamGuard
{
    /// <summary>
    /// MonoBehaviour that owns the study session lifecycle and captures the
    /// confederate's intrusion-mark input.
    ///
    /// Attach to any persistent GameObject (e.g. the Player rig).
    ///
    /// Controller mapping (Meta Quest):
    ///   Left controller primary button = X  (OVRInput.Button.One, Controller.LTouch)
    ///   Right controller primary button = A (OVRInput.Button.One, Controller.RTouch)
    ///
    /// The confederate presses the mapped button the instant they cross into the
    /// participant's play space.  StudyLogger writes INTRUSION_MARK; downstream
    /// analysis computes latency = TRIGGER.timestamp − INTRUSION_MARK.timestamp.
    /// </summary>
    public class StudyInputHandler : MonoBehaviour
    {
        [Header("Session Parameters")]
        [SerializeField] private string participantId = "P00";
        [SerializeField] private string condition     = "baseline";

        [Header("Intrusion-Mark Button")]
        [Tooltip("Which controller the confederate uses to mark an intrusion.")]
        [SerializeField] private OVRInput.Controller intrusionController = OVRInput.Controller.LTouch;
        [Tooltip("Which button on that controller triggers INTRUSION_MARK.")]
        [SerializeField] private OVRInput.Button intrusionButton = OVRInput.Button.One; // X on left, A on right

        [Header("Tracking Transforms")]
        [Tooltip("Root transform of the player rig (world-space position logged to position.csv).")]
        [SerializeField] private Transform playerTransform;
        [Tooltip("Camera / CenterEyeAnchor transform (logged to headset.csv).")]
        [SerializeField] private Transform headsetTransform;

        private void Start()
        {
            DreamGuardLog.Log($"[StudyInputHandler] Start — participant={participantId} condition={condition}");
            StudyLogger.BeginSession(participantId, condition);
        }

        private void Update()
        {
            if (OVRInput.GetDown(intrusionButton, intrusionController))
            {
                DreamGuardLog.Log("[StudyInputHandler] Intrusion mark button pressed.");
                StudyLogger.LogIntrusionMark();
            }

            if (StudyLogger.IsActive)
            {
                if (playerTransform != null)
                    StudyLogger.LogPlayerPosition(playerTransform.position);

                StudyLogger.LogRightController(
                    OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch),
                    OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch));

                StudyLogger.LogLeftController(
                    OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch),
                    OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch));

                if (headsetTransform != null)
                    StudyLogger.LogHeadset(headsetTransform.position, headsetTransform.rotation);
            }
        }

        private void OnApplicationPause(bool paused)
        {
            if (paused && StudyLogger.IsActive)
            {
                DreamGuardLog.LogWarning("[StudyInputHandler] App paused — ending study session.");
                StudyLogger.EndSession();
            }
        }

        private void OnDestroy()
        {
            if (StudyLogger.IsActive)
            {
                DreamGuardLog.Log("[StudyInputHandler] OnDestroy — ending study session.");
                StudyLogger.EndSession();
            }
        }
    }
}
