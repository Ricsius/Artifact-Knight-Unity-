using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            InvokeAnimationParameterChangeRequest("IsIdlingInAir");
        }

        public override void Move(Vector2 direction)
        {
            InvokeAnimationParameterChangeRequest("IsMovingInAir");
            MoveLogic(direction);
        }

        public override void StopMove()
        {
            InvokeAnimationParameterChangeRequest("IsIdlingInAir");
            StopMoveLogic();
        }
    }
}
