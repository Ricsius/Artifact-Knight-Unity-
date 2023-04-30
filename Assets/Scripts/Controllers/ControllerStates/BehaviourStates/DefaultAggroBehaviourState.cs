
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
                    Vector2 newDirection = new Vector2(directionX, _manager.CurrentDirection.y);

                    _manager.CurrentDirection = newDirection;

                    MovementStateManager.CurrentMovementState.Move(_manager.CurrentDirection);
                }
                else
                {
                    _manager.SetCurrentBehaviorState(ControllerBehaviourStateType.Patrol);
                }
            }
        }

        public override void OnDeselect()
        {
            Vector2 newDirection = new Vector2(-_manager.CurrentDirection.x, _manager.CurrentDirection.y);

            _manager.CurrentDirection = newDirection;
        }
    }
}
