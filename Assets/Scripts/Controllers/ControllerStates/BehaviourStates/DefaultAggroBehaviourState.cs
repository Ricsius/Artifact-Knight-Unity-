
using Assets.Scripts.Controllers.ControllerStates.Managers;
using Assets.Scripts.Detectors;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Controllers.ControllerStates.BehaviourStates
{
    public class DefaultAggroBehaviourState : ControllerBehaviourStateBase
    {
        public DefaultAggroBehaviourState()
            : base(ControllerBehaviourStateType.Aggro)
        {
        }

        public override void Behaviour()
        {
            {
                GameObject playerObject = DetectGameObjectsFront().FirstOrDefault(o => SpecialGameObjectRecognition.IsPlayer(o));

                if (playerObject != null)
                {
                    float directionX = _transform.position.x <= playerObject.transform.position.x ? 1 : -1;
                    Vector2 newDirection = new Vector2(directionX, CurrentDirection.y);

                    CurrentDirection = newDirection;

                    MovementStateManager.CurrentMovementState.Move(CurrentDirection);
                }
                else
                {
                    InvokeBehaviourStateChangeRequest(ControllerBehaviourStateType.Patrol);
                }
            }
        }

        public override void OnDeselect()
        {
            Vector2 newDirection = new Vector2(-CurrentDirection.x, CurrentDirection.y);

            CurrentDirection = newDirection;
        }
    }
}
