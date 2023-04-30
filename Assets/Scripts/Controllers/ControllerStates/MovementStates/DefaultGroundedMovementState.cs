
using Assets.Scripts.Controllers.ControllerStates.Managers;
using UnityEngine;

namespace Assets.Scripts.Controllers.ControllerStates.MovementStates
{
    public class DefaultGroundedMovementState : ControllerMovementStateBase
    {
        public DefaultGroundedMovementState()
            : base(ControllerMovementStateType.Grounded)
        {
        }

        public override void OnSelect()
        {
            _manager.SetCurrentAnimationParameter(AnimationParameterName.IsIdlingOnGround);
        }

        public override void Move(Vector2 direction)
        {
            _manager.SetCurrentAnimationParameter(AnimationParameterName.IsMovingOnGround);
            MoveLogic(direction);
        }

        public override void StopMove()
        {
            _manager.SetCurrentAnimationParameter(AnimationParameterName.IsIdlingOnGround);
            StopMoveLogic();
        }

        public override void Jump()
        {
            JumpLogic();
        }
    }
}
