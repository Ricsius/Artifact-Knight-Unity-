
using Assets.Scripts.Detectors;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Controllers.ControllerStates.BehaviourStates
{
    public class DefaultPatrolBehaviourState : ControllerBehaviourStateBase
    {
        private MobControllerBase _controller;
        public DefaultPatrolBehaviourState()
            : base(ControllerBehaviourStateType.Patrol)
        {
            
        }

        protected override void Init()
        {
            base.Init();

            _controller = _manager.Owner.GetComponent<MobControllerBase>();
        }

        public override void OnSelect()
        {
            if (_manager.CurrentDirection == Vector2.zero)
            {
                _manager.CurrentDirection = _transform.right;
            }

            _controller.SolidCollision += OnSolidCollision;
        }

        public override void OnDeselect()
        {
            _controller.SolidCollision -= OnSolidCollision;
        }

        public override void Behaviour()
        {
            if (DetectGameObjectsFront().Any(o => SpecialGameObjectRecognition.IsPlayer(o)))
            {
                _manager.SetCurrentBehaviorState(ControllerBehaviourStateType.Aggro);
            }
            else
            {
                MovementStateManager.CurrentMovementState.Move(_manager.CurrentDirection);
            }
        }

        private void OnSolidCollision(object sender, SolidCollisionEventArgs args)
        {
            Vector2 newDirection = new Vector2(-_manager.CurrentDirection.x, _manager.CurrentDirection.y);

            _manager.CurrentDirection = newDirection;
        }
    }
}
