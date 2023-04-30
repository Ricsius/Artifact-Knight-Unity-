
using Assets.Scripts.Controllers.ControllerStates.BehaviourStates.EvilEye;
using Assets.Scripts.Controllers.ControllerStates.MovementStates;

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
    }
}
