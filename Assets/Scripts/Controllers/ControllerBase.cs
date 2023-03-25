

using Assets.Scripts.Controllers.ControllerStates.Managers;
using Assets.Scripts.Controllers.ControllerStates.MovementStates;
using Assets.Scripts.Detectors;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class ControllerBase : MonoBehaviour
    {
        [field: SerializeField]
        public ControllerMovementStateType StartingMovementState { get; private set; }

        public float MovementSpeed 
        { 
            get 
            { 
                return _movementStateManager.MovementSpeed; 
            } 
            set 
            { 
                _movementStateManager.MovementSpeed = value; 
            } 
        }

        public float JumpForce { get; set; }
        protected bool _ladderCheck;
        protected bool _groundCheck;
        protected Rigidbody2D _rigidBody;
        protected Collider2D _collider;
        protected Animator _animator;
        protected GroundDetector _groundDetector;
        protected LadderDetector _ladderDetector;
        protected ControllerMovementStateManager _movementStateManager;
        [SerializeField]
        private float _movementSpeed;
        [SerializeField]
        private float _jumpForce;


        protected virtual void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();
            _animator = GetComponent<Animator>();
            _groundDetector = GetComponent<GroundDetector>();
            _ladderDetector = GetComponent<LadderDetector>();
            _movementStateManager = new ControllerMovementStateManager(gameObject);

            _movementStateManager.MovementSpeed = _movementSpeed;
            _movementStateManager.JumpForce = _jumpForce;
        }

        protected virtual void Start()
        {
            _movementStateManager.TrySetCurrentMovementState(StartingMovementState);
        }

        protected virtual void Update()
        {
            if (_groundDetector != null)
            {
                _groundCheck = _groundDetector.Detect().Any();
                
                if (_groundCheck && _movementStateManager.CurrentMovementState.Type == ControllerMovementStateType.InAir)
                {
                    _movementStateManager.TrySetCurrentMovementState(ControllerMovementStateType.Grounded);
                }
                else if (!_groundCheck && _movementStateManager.CurrentMovementState.Type == ControllerMovementStateType.Grounded)
                {
                    _movementStateManager.TrySetCurrentMovementState(ControllerMovementStateType.InAir);
                }
            }

            if (_ladderDetector != null)
            {
                _ladderCheck = _ladderDetector.Detect().Any();

                if (!_ladderCheck && _movementStateManager.CurrentMovementState.Type == ControllerMovementStateType.OnLadder)
                {
                    _movementStateManager.TrySetCurrentMovementState(ControllerMovementStateType.InAir);
                }
            }
        }
    }
}
