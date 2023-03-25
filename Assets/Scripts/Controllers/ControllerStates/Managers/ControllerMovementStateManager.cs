
using Assets.Scripts.Controllers.ControllerStates.MovementStates;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Controllers.ControllerStates.Managers
{
    public class ControllerMovementStateManager
    {
        public float MovementSpeed { get; set; }
        public float JumpForce { get; set; }
        public ControllerMovementStateBase CurrentMovementState { get; private set; }
        public GameObject Owner { get; }
        private Animator _animator;
        private Dictionary<ControllerMovementStateType, ControllerMovementStateBase> _movementStates = new Dictionary<ControllerMovementStateType, ControllerMovementStateBase>();
        private string _currentAnimationParameterName;

        public ControllerMovementStateManager(GameObject owner)
        {
            Owner = owner;
            _animator = Owner.GetComponent<Animator>();
        }

        public bool TrySetCurrentMovementState(ControllerMovementStateType type)
        {
            if (!_movementStates.ContainsKey(type))
            {
                return false;
            }

            if (CurrentMovementState != null)
            {
                CurrentMovementState?.OnDeselect();
                CurrentMovementState.AnimationParameterChangeRequest -= OnAnimationParameterChangeRequest;
            }

            CurrentMovementState = _movementStates[type];
            CurrentMovementState.AnimationParameterChangeRequest += OnAnimationParameterChangeRequest;
            CurrentMovementState.OnSelect();

            return true;
        }

        public void Manage<T>() where T : ControllerMovementStateBase, new()
        {
            ControllerMovementStateBase state = new T();
            
            state.Manager= this;

            _movementStates[state.Type] = state;
        }

        private void OnAnimationParameterChangeRequest(object sender, EventArgs args)
        {
            AnimationParameterChangeRequestEventArgs animationParameterChangeRequestChangeArgs = args as AnimationParameterChangeRequestEventArgs;

            TrySetAnimationParameter(animationParameterChangeRequestChangeArgs.ParameterName);
        }

        private bool TrySetAnimationParameter(string parameterName)
        {
            try
            {
                if (!string.IsNullOrEmpty(_currentAnimationParameterName))
                {
                    _animator.SetBool(_currentAnimationParameterName, false);
                }

                _currentAnimationParameterName = parameterName;
                _animator.SetBool(_currentAnimationParameterName, true);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
