using Assets.Scripts.Detectors;
using System.Linq;

namespace Assets.Scripts.Controllers.ControllerStates.BehaviourStates.Eyenomancer
{
    public class EyenomancerIdleBehaviourState : ControllerBehaviourStateBase
    {
        public EyenomancerIdleBehaviourState()
            : base(ControllerBehaviourStateType.Idle)
        { 
        }

        public override void OnSelect()
        {
            MovementStateManager.CurrentMovementState.StopMove();
        }

        public override void Behaviour()
        {
            bool isPlayerDetected = DetectGameObjectsBehind().Any(o => SpecialGameObjectRecognition.IsPlayer(o)) || DetectGameObjectsFront().Any(o => SpecialGameObjectRecognition.IsPlayer(o));
            
            if (isPlayerDetected)
            {
                InvokeBehaviourStateChangeRequest(ControllerBehaviourStateType.Aggro);
            }
        }
    }
}
