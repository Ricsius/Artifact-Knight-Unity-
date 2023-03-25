
using Assets.Scripts.Controllers.ControllerStates.BehaviourStates;
using Assets.Scripts.Controllers.ControllerStates.Managers;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class MobControllerBase : ControllerBase
    {
        [field: SerializeField]
        public ControllerBehaviourStateType StartingBehaviorState { get; protected set; }
        [field: SerializeField]
        public float SightRange { get; set; }
        
        protected ControllerBehaviourStateManager _behaviorStateManager;

        protected override void Awake()
        {
            base.Awake();

            _behaviorStateManager = new ControllerBehaviourStateManager(gameObject, _movementStateManager);

            _behaviorStateManager.SightRange = SightRange;
        }

        protected override void Start()
        {
            base.Start();

            _behaviorStateManager.TrySetCurrentBehaviorState(StartingBehaviorState);
        }

        protected override void Update()
        {
            base.Update();

            _behaviorStateManager.CurrentBehaviourState.Behaviour();
        }
    }
}
