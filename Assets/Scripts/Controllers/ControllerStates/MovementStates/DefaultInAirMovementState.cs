
using UnityEngine;

namespace Assets.Scripts.Controllers.ControllerStates.MovementStates
{
    public class DefaultInAirMovementState : ControllerMovementStateBase
    {
        public DefaultInAirMovementState()
            : base(ControllerMovementStateType.InAir)
        {
        }

        public override void OnSelect()
        {
            InvokeAnimationParameterChangeRequest("IsIdlingInAir");
        }

        public override void Move(Vector2 direction)
        {
            InvokeAnimationParameterChangeRequest("IsIdlingInAir");
            MoveLogic(direction);
        }

        public override void StopMove()
        {
            InvokeAnimationParameterChangeRequest("IsIdlingInAir");
            StopMoveLogic();
        }
    }
}
