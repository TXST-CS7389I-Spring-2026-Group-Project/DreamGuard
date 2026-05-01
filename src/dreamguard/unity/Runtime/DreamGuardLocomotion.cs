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
        [SerializeField] private float snapAngle = 45f;
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
            Vector2 axis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

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
            float x = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x;

            if (Mathf.Abs(x) < snapDeadzone)
            {
                _snapReady = true;
                return;
            }

            if (!_snapReady) return;
            _snapReady = false;

            transform.Rotate(Vector3.up, x > 0f ? snapAngle : -snapAngle, Space.World);
        }
    }
}
