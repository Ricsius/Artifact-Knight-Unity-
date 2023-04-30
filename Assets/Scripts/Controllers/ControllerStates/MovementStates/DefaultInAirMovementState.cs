
using Assets.Scripts.Controllers.ControllerStates.Managers;
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
            _manager.SetCurrentAnimationParameter(AnimationParameterName.IsIdlingInAir);
        }

        public override void Move(Vector2 direction)
        {
            _manager.SetCurrentAnimationParameter(AnimationParameterName.IsIdlingInAir);
            MoveLogic(direction);
        }

        public override void StopMove()
        {
            _manager.SetCurrentAnimationParameter(AnimationParameterName.IsIdlingInAir);
            StopMoveLogic();
        }
    }
}
