
using Assets.Scripts.Controllers.ControllerStates.Managers;
using UnityEngine;

namespace Assets.Scripts.Controllers.ControllerStates.MovementStates
{
    public class DefaultFlyingMovementState : ControllerMovementStateBase
    {
        public DefaultFlyingMovementState() 
            : base(ControllerMovementStateType.InAir)
        {
        }

        public override void OnSelect()
        {
            _manager.SetCurrentAnimationParameter(AnimationParameterName.IsIdlingInAir);
        }

        public override void Move(Vector2 direction)
        {
            _manager.SetCurrentAnimationParameter(AnimationParameterName.IsMovingInAir);
            MoveLogic(direction);
        }

        public override void StopMove()
        {
            _manager.SetCurrentAnimationParameter(AnimationParameterName.IsIdlingInAir);
            StopMoveLogic();
        }
    }
}
