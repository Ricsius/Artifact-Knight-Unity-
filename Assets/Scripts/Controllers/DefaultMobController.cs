
using Assets.Scripts.Controllers.ControllerStates.BehaviourStates;
using Assets.Scripts.Controllers.ControllerStates.MovementStates;
using UnityEngine;

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
        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            if (_behaviorStateManager.CurrentBehaviourState.Type != ControllerBehaviourStateType.Aggro)
            {
                Vector2 currentDirextion = _behaviorStateManager.CurrentBehaviourState.CurrentDirection;
                Vector2 newDirection = new Vector2(-currentDirextion.x, currentDirextion.y);

                _behaviorStateManager.CurrentBehaviourState.CurrentDirection = newDirection;
            }
        }
    }
}
