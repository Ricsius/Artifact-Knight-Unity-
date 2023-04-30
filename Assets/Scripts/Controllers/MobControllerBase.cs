
using Assets.Scripts.Controllers.ControllerStates.BehaviourStates;
using Assets.Scripts.Controllers.ControllerStates.Managers;
using System;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class MobControllerBase : ControllerBase
    {
        [field: SerializeField]
        public ControllerBehaviourStateType StartingBehaviorState { get; private set; }
        [field: SerializeField]
        public float SightRange { get; set; }
        public EventHandler<SolidCollisionEventArgs> SolidCollision;
        protected ControllerBehaviourStateManager _behaviorStateManager;

        protected override void Awake()
        {
            base.Awake();

            _behaviorStateManager = new ControllerBehaviourStateManager(gameObject, _movementStateManager, SightRange);

            tag = "Mob";
        }

        protected override void Start()
        {
            base.Start();

            _behaviorStateManager.SetCurrentBehaviorState(StartingBehaviorState);
        }

        protected override void Update()
        {
            base.Update();

            _behaviorStateManager.CurrentBehaviourState.Behaviour();
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            SolidCollision?.Invoke(this, new SolidCollisionEventArgs(collision));
        }
    }
}
