
using Assets.Scripts.Controllers.ControllerStates.Managers;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Controllers.ControllerStates.MovementStates
{
    public abstract class ControllerMovementStateBase
    {
        public ControllerMovementStateType Type { get; }
        public ControllerMovementStateManager Manager 
        { 
            set
            {
                _manager= value;

                Init();
            }
        }
        public event EventHandler AnimationParameterChangeRequest;
        protected Transform _transform;
        protected Rigidbody2D _rigidBody;
        protected Collider2D _collider;
        protected ControllerMovementStateManager _manager;
        
        public ControllerMovementStateBase(ControllerMovementStateType type)
        {
            Type = type;
        }

        public virtual void OnSelect()
        {
        }
        public virtual void OnDeselect()
        {
        }
        public virtual void Move(Vector2 direction)
        {
        }
        public virtual void StopMove()
        {
        }
        public virtual void Jump()
        {
        }

        protected void MoveLogic(Vector2 direction)
        {
            float rotationAngleX = _transform.eulerAngles.x;
            float rotationAngleZ = _transform.eulerAngles.z;
            float sign = ((_transform.eulerAngles.y > 0 && direction.x == 0) || direction.x < 0) ? 1 : 0;
            float rotationAngleY = sign * 180f;
            
            Vector2 velocity = _rigidBody.velocity;

            if (direction.x != 0 && direction.y == 0)
            {
                velocity = new Vector2(direction.x * _manager.MovementSpeed, _rigidBody.velocity.y);
            }

            if (direction.y != 0 && direction.x == 0)
            {
                velocity = new Vector2(_rigidBody.velocity.x, direction.y * _manager.MovementSpeed);
            }

            if (direction.x != 0 && direction.y != 0)
            {
                velocity = new Vector2(direction.x * _manager.MovementSpeed, direction.y * _manager.MovementSpeed);
            }
            
            _transform.eulerAngles = new Vector3(rotationAngleX, rotationAngleY, rotationAngleZ);
            _rigidBody.velocity = velocity;
            Vector2 boxCastSize = new Vector2(.01f, _collider.bounds.size.y);
            RaycastHit2D[] raycastHits = Physics2D.BoxCastAll(_transform.position, boxCastSize, 0, _transform.right, _collider.bounds.extents.x + boxCastSize.x);

            if (raycastHits.Any(rh => rh.transform != _transform && !rh.collider.isTrigger))
            {
                StopMove();
            }

        }

        protected void JumpLogic()
        {
            _rigidBody.AddForce(Vector2.up * _manager.JumpForce, ForceMode2D.Impulse);
        }

        protected void StopMoveLogic()
        {
            float y = _rigidBody.gravityScale > 0 ? _rigidBody.velocity.y : 0;

            _rigidBody.velocity = new Vector2(0, y);
        }

        protected void InvokeAnimationParameterChangeRequest(string parameterName)
        {
            AnimationParameterChangeRequest?.Invoke(this, new AnimationParameterChangeRequestEventArgs(parameterName));
        }

        protected virtual void Init()
        {
            _transform = _manager.Owner.transform;
            _rigidBody = _manager.Owner.GetComponent<Rigidbody2D>();
            _collider = _manager.Owner.GetComponent<Collider2D>();
        }
    }
}
