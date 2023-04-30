
using Assets.Scripts.Controllers.ControllerStates.BehaviourStates;
using Assets.Scripts.Controllers.ControllerStates.MovementStates;

namespace Assets.Scripts.Controllers
{
    public class DefaultMobController : MobControllerBase
    {
        protected override void Awake()
        {
            base.Awake();

            _movementStateManager.Manage<DefaultGroundedMovementState>();
            _movementStateManager.Manage<DefaultInAirMovementState>();

            _behaviorStateManager.Manage<DefaultPatrolBehaviourState>();
            _behaviorStateManager.Manage<DefaultAggroBehaviourState>();
        }
    }
}
