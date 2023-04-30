
using Assets.Scripts.Controllers.ControllerStates.MovementStates;
using Assets.Scripts.Detectors;
using System.Collections.Generic;
using System.Linq;
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
        private GroundDetector _groundDetector;
        private LadderDetector _ladderDetector;
        private Dictionary<ControllerMovementStateType, ControllerMovementStateBase> _movementStates = new Dictionary<ControllerMovementStateType, ControllerMovementStateBase>();
        private bool _groundCheck;
        private bool _ladderCheck;
        private string _currentAnimationParameterName;

        public ControllerMovementStateManager(GameObject owner)
        {
            Owner = owner;
            _animator = Owner.GetComponent<Animator>();
            _groundDetector = Owner.GetComponent<GroundDetector>();
            _ladderDetector = Owner.GetComponent<LadderDetector>();
        }

        public void Manage<T>() where T : ControllerMovementStateBase, new()
        {
            ControllerMovementStateBase state = new T();

            state.Manager = this;

            _movementStates[state.Type] = state;
        }

        public bool TrySetCurrentMovementState(ControllerMovementStateType type)
        {
            if (!_movementStates.ContainsKey(type))
            {
                return false;
            }

            bool settingIsRejected = (type == ControllerMovementStateType.Grounded && _groundDetector == null)
                || (type == ControllerMovementStateType.OnLadder && _ladderDetector == null);

            if (settingIsRejected)
            {
                type = ControllerMovementStateType.InAir;
            }

            CurrentMovementState?.OnDeselect();
            CurrentMovementState = _movementStates[type];
            CurrentMovementState.OnSelect();

            return !settingIsRejected;
        }

        public void StateTransitionUpdate()
        {
            if (_groundDetector != null)
            {
                _groundCheck = _groundDetector.Detect().Any();

                if (_groundCheck && CurrentMovementState.Type == ControllerMovementStateType.InAir)
                {
                    TrySetCurrentMovementState(ControllerMovementStateType.Grounded);
                }
                else if (!_groundCheck && CurrentMovementState.Type == ControllerMovementStateType.Grounded)
                {
                    TrySetCurrentMovementState(ControllerMovementStateType.InAir);
                }
            }

            if (_ladderDetector != null && CurrentMovementState.Type == ControllerMovementStateType.OnLadder)
            {
                _ladderCheck = _ladderDetector.Detect().Any();

                if (!_ladderCheck)
                {
                    TrySetCurrentMovementState(ControllerMovementStateType.InAir);
                }
            }
        }

        public void SetCurrentAnimationParameter(AnimationParameterName parameterName)
        {
            if (!string.IsNullOrEmpty(_currentAnimationParameterName))
            {
                _animator.SetBool(_currentAnimationParameterName, false);
            }

            _currentAnimationParameterName = parameterName.ToString();
            _animator.SetBool(_currentAnimationParameterName, true);
        }
    }
}
