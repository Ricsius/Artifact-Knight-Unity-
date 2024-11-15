﻿
using Assets.Scripts.Detectors;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Controllers.ControllerStates.BehaviourStates.EvilEye
{
    public class EvilEyeIdleBehaviourState : ControllerBehaviourStateBase
    {
        public EvilEyeIdleBehaviourState() 
            : base(ControllerBehaviourStateType.Idle)
        {

        }

        public override void OnSelect()
        {
            _manager.CurrentDirection = Vector2.zero;
            MovementStateManager.CurrentMovementState.StopMove();
        }

        public override void Behaviour()
        {
            Transform playerTransform = DetectGameObjectsInCircle().FirstOrDefault(o => SpecialGameObjectRecognition.IsPlayer(o))?.transform;

            if (playerTransform != null)
            {
                _manager.SetCurrentBehaviorState(ControllerBehaviourStateType.Aggro);
            }
        }
    }
}
