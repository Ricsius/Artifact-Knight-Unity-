using Assets.Scripts.Controllers.ControllerStates.BehaviourStates.Eyenomancer;
using Assets.Scripts.Controllers.ControllerStates.MovementStates;
using Assets.Scripts.Spawners;
using Assets.Scripts.Systems.Health;
using System;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class EyenomancerController : MobControllerBase
    {
        [field: SerializeField]
        public float SummonCooldown { get; private set; }
        [field: SerializeField]
        public float SummonLimit { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            _movementStateManager.Manage<DefaultGroundedMovementState>();
            _movementStateManager.Manage<DefaultInAirMovementState>();

            _behaviorStateManager.Manage<EyenomancerIdleBehaviourState>();
            _behaviorStateManager.Manage<EyenomancerAggroBehaviourState>();
        }
    }
}
