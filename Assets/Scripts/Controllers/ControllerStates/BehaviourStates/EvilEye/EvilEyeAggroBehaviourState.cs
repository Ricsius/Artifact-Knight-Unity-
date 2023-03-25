using Assets.Scripts.Controllers.ControllerStates.Managers;
using Assets.Scripts.Detectors;
using System.Linq;
using UnityEngine;


namespace Assets.Scripts.Controllers.ControllerStates.BehaviourStates.EvilEye
{
    public class EvilEyeAggroBehaviourState : ControllerBehaviourStateBase
    {
        public EvilEyeAggroBehaviourState() 
            : base(ControllerBehaviourStateType.Aggro)
        {
        }

        public override void Behaviour()
        {
            Transform playerTransform = DetectGameObjectsInCircle().FirstOrDefault(o => SpecialGameObjectRecognition.IsPlayer(o))?.transform;

            if (playerTransform != null)
            {
                CurrentDirection = (playerTransform.position - _transform.position).normalized;

                MovementStateManager.CurrentMovementState.Move(CurrentDirection);
            }
            else
            {
                InvokeBehaviourStateChangeRequest(ControllerBehaviourStateType.Idle);
            }

            
        }
    }
}
