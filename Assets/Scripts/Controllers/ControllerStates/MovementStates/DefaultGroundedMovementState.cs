
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
            InvokeAnimationParameterChangeRequest("IsIdlingOnGround");
        }

        public override void Move(Vector2 direction)
        {
            InvokeAnimationParameterChangeRequest("IsMovingOnGround");
            MoveLogic(direction);
        }

        public override void StopMove()
        {
            InvokeAnimationParameterChangeRequest("IsIdlingOnGround");
            StopMoveLogic();
        }

        public override void Jump()
        {
            JumpLogic();
        }
    }
}
