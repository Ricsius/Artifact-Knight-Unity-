
using Assets.Scripts.Controllers.ControllerStates.BehaviourStates;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Controllers.ControllerStates.Managers
{
    public class ControllerBehaviourStateManager
    {
        public Vector2 CurrentDirection { get; set; }
        public float SightRange { get; private set; }
        public ControllerBehaviourStateBase CurrentBehaviourState { get; private set; }
        public GameObject Owner { get; }
        private ControllerMovementStateManager _movementStateManager { get; }

        private Dictionary<ControllerBehaviourStateType, ControllerBehaviourStateBase> _behaviourStates = new Dictionary<ControllerBehaviourStateType, ControllerBehaviourStateBase>();

        public ControllerBehaviourStateManager(GameObject owner, ControllerMovementStateManager movementStateManager, float sightRange) 
        {
            Owner = owner;
            _movementStateManager = movementStateManager;
            SightRange = sightRange;
        }

        public void SetCurrentBehaviorState(ControllerBehaviourStateType type)
        {
            CurrentBehaviourState?.OnDeselect();
            CurrentBehaviourState = _behaviourStates[type];
            CurrentBehaviourState.OnSelect();
        }

        public void Manage<T>() where T : ControllerBehaviourStateBase, new()
        {
            ControllerBehaviourStateBase state = new T();

            state.Manager = this;
            state.MovementStateManager = _movementStateManager;

            _behaviourStates[state.Type] = state;

            if (CurrentBehaviourState == null)
            {
                CurrentBehaviourState = state;
            }
        }
    }
}
