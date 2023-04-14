using Assets.Scripts.Controllers.ControllerStates.BehaviourStates.Dragon;
using Assets.Scripts.Controllers.ControllerStates.MovementStates;


namespace Assets.Scripts.Controllers
{
    public class DragonController : MobControllerBase
    {
        protected override void Awake()
        {
            base.Awake();

            _movementStateManager.Manage<DefaultFlyingMovementState>();

            _behaviorStateManager.Manage<DragonIdleBehaviourState>();
            _behaviorStateManager.Manage<DragonAggroBehaviourState>();
        }
    }
}
