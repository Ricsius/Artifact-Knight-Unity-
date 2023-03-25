
using Assets.Scripts.Detectors;
using System.Linq;

namespace Assets.Scripts.Controllers.ControllerStates.BehaviourStates
{
    public class DefaultPatrolBehaviourState : ControllerBehaviourStateBase
    {
        public DefaultPatrolBehaviourState()
            : base(ControllerBehaviourStateType.Patrol)
        {
            
        }

        public override void OnSelect()
        {
            CurrentDirection = _transform.right;
        }

        public override void Behaviour()
        {
            if (DetectGameObjectsFront().Any(o => SpecialGameObjectRecognition.IsPlayer(o)))
            {
                InvokeBehaviourStateChangeRequest(ControllerBehaviourStateType.Aggro);
            }
            else
            {
                MovementStateManager.CurrentMovementState.Move(CurrentDirection);
            }
        }
    }
}
