
using Assets.Scripts.Controllers.ControllerStates.BehaviourStates;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Controllers.ControllerStates.Managers
{
    public class ControllerBehaviourStateManager
    {
        public float SightRange { get; set; }
        public ControllerBehaviourStateBase CurrentBehaviourState { get; private set; }
        public GameObject Owner { get; }
        private ControllerMovementStateManager _movementStateManager { get; }

        private Dictionary<ControllerBehaviourStateType, ControllerBehaviourStateBase> _behaviourStates = new Dictionary<ControllerBehaviourStateType, ControllerBehaviourStateBase>();

        public ControllerBehaviourStateManager(GameObject owner, ControllerMovementStateManager movementStateManager) 
        {
            Owner = owner;
            _movementStateManager = movementStateManager;
        }

        public bool TrySetCurrentBehaviorState(ControllerBehaviourStateType type)
        {
            if (!_behaviourStates.ContainsKey(type))
            {
                return false;
            }

            if (CurrentBehaviourState != null)
            {
                CurrentBehaviourState?.OnDeselect();
                CurrentBehaviourState.BehaviourStateChangeRequest -= OnBehaviorStateChangeRequest;
                _behaviourStates[type].CurrentDirection = CurrentBehaviourState.CurrentDirection;
            }

            CurrentBehaviourState = _behaviourStates[type];
            CurrentBehaviourState.BehaviourStateChangeRequest += OnBehaviorStateChangeRequest;
            CurrentBehaviourState.OnSelect();

            return true;
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

        private void OnBehaviorStateChangeRequest(object sender, BehaviourStaceChangeRequestEventArgs args)
        {
            TrySetCurrentBehaviorState(args.BehaviourStateType);
        }
    }
}
