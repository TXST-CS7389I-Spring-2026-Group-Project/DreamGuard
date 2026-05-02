using UnityEngine;

namespace DreamGuard
{
    /// <summary>
    /// Smooth locomotion and snap turning for Meta Quest controllers.
    /// Attach to the root GameObject that should physically move (e.g. OVRCameraRig).
    ///
    /// Left thumbstick  → forward/strafe relative to head direction
    /// Right thumbstick → snap turn (default 45°)
    /// </summary>
    public class DreamGuardLocomotion : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 2f;

        [Header("Snap Turn")]
        [SerializeField] private float snapAngle = 5f;
        [SerializeField] private float snapDeadzone = 0.5f;

        private Transform _headTransform;
        private CharacterController _controller;
        private float _verticalVelocity;
        private bool _snapReady = true;

        private void Start()
        {
            // OVRCameraRig exposes CenterEyeAnchor; fall back to Camera.main
            var rig = GetComponent<OVRCameraRig>();
            _headTransform = rig != null ? rig.centerEyeAnchor : Camera.main?.transform;
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            Move();
            SnapTurn();
        }

        private void Move()
        {
            // Left stick  → full 2D movement (forward/back + strafe)
            // Right stick Y → also drives forward/backward (whichever is larger wins)
            Vector2 left  = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
            float   rightY = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y;

            float forward = Mathf.Abs(rightY) > Mathf.Abs(left.y) ? rightY : left.y;
            Vector2 axis  = new Vector2(left.x, forward);

            Vector3 fwd = _headTransform ? _headTransform.forward : transform.forward;
            Vector3 rgt = _headTransform ? _headTransform.right : transform.right;
            fwd.y = 0f;
            rgt.y = 0f;
            fwd.Normalize();
            rgt.Normalize();

            if (_controller != null)
            {
                if (_controller.isGrounded)
                    _verticalVelocity = 0f;
                else
                    _verticalVelocity += Physics.gravity.y * Time.deltaTime;

                Vector3 move = (fwd * axis.y + rgt * axis.x) * moveSpeed;
                move.y = _verticalVelocity;
                _controller.Move(move * Time.deltaTime);
            }
            else if (axis.sqrMagnitude >= 0.01f)
            {
                transform.position += (fwd * axis.y + rgt * axis.x) * (moveSpeed * Time.deltaTime);
            }
        }

        private void SnapTurn()
        {
            // Right stick X → snap turn
            float x = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x;

            if (Mathf.Abs(x) < snapDeadzone)
            {
                _snapReady = true;
                return;
            }

            if (!_snapReady) return;
            _snapReady = false;

            // Rotate around the head so the player doesn't slide sideways on snap
            float angle = x > 0f ? snapAngle : -snapAngle;
            Vector3 pivot = _headTransform != null ? _headTransform.position : transform.position;
            transform.RotateAround(pivot, Vector3.up, angle);
        }
    }
}
