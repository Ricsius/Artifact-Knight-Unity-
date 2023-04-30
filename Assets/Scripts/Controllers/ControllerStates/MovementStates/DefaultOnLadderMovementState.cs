
using Assets.Scripts.Controllers.ControllerStates.Managers;
using UnityEngine;

namespace Assets.Scripts.Controllers.ControllerStates.MovementStates
{
    public class DefaultOnLadderMovementState : ControllerMovementStateBase
    {
        public DefaultOnLadderMovementState()
            : base(ControllerMovementStateType.OnLadder)
        {
        }

        public override void OnSelect()
        {
            _rigidBody.gravityScale = 0;

            _manager.SetCurrentAnimationParameter(AnimationParameterName.IsIdlingOnLadder);
            StopMove();
        }

        public override void OnDeselect()
        {
            _rigidBody.gravityScale = 1;
        }

        public override void Move(Vector2 direction)
        {
            if (direction.x != 0 && direction.y == 0)
            {
                _manager.SetCurrentAnimationParameter(AnimationParameterName.IsIdlingOnLadder);
            }
            else
            {
                _manager.SetCurrentAnimationParameter(AnimationParameterName.IsMovingOnLadder);
            }


            MoveLogic(direction);
        }

        public override void StopMove()
        {
            _manager.SetCurrentAnimationParameter(AnimationParameterName.IsIdlingOnLadder);
            StopMoveLogic();
        }
    }
}
