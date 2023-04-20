
using Assets.Scripts.Controllers.ControllerStates.BehaviourStates.EvilEye;
using Assets.Scripts.Controllers.ControllerStates.MovementStates;

using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class EvilEyeController : MobControllerBase
    {
        protected override void Awake()
        {
            base.Awake();

            _movementStateManager.Manage<DefaultFlyingMovementState>();

            _behaviorStateManager.Manage<EvilEyeIdleBehaviourState>();
            _behaviorStateManager.Manage<EvilEyeAggroBehaviourState>();
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
        }
    }
}
