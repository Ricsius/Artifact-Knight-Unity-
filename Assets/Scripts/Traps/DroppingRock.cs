using Assets.Scripts.Detectors;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Traps
{
    public class DroppingRock : MonoBehaviour
    {
        [field: SerializeField]
        public float DetectionVelocityTreshold { get; set; }
        [field: SerializeField]
        public float DetectionRange { get; set; }
        private Vector2 _startingPosition;
        private Vector2 _boxCastOffset;
        private Rigidbody2D _rigidbody;
        private Collider2D _collider;
        private GroundDetector _groundDetector;

        private void Awake()
        {
            _startingPosition = transform.position;
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider= GetComponent<Collider2D>();
            _groundDetector= GetComponent<GroundDetector>();
            _boxCastOffset = Vector2.down * (_collider.bounds.extents.y + .3f);
        }

        void Update()
        {
            bool goingUp = _rigidbody.gravityScale < 0;
            bool goingDown = _rigidbody.gravityScale > 0;

            if (!(goingUp || goingDown))
            {
                Vector2 center = (Vector2)transform.position + _boxCastOffset;
                RaycastHit2D raycastHit = Physics2D.BoxCast(center, _collider.bounds.size, 0, Vector2.down, DetectionRange);
                Rigidbody2D otherRigidBody = raycastHit.rigidbody;

                if (otherRigidBody != null && Mathf.Abs(otherRigidBody.velocity.x) >= DetectionVelocityTreshold)
                {
                    _rigidbody.gravityScale = 1;
                    _rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                }

                return;
            }

            if (goingUp && transform.position.y >= _startingPosition.y)
            {
                transform.position = _startingPosition;
                _rigidbody.gravityScale = 0;
                _rigidbody.velocity= Vector2.zero;
                _rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

                return;
            }

            bool groundCheck = _groundDetector.Detect().Any();

            if (groundCheck)
            {
                _rigidbody.gravityScale = -1;

                return;
            }
        }
    }
}
