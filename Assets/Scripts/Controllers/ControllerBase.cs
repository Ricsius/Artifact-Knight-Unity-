

using Assets.Scripts.Controllers.ControllerStates.Managers;
using Assets.Scripts.Controllers.ControllerStates.MovementStates;
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
        protected Rigidbody2D _rigidBody;
        protected Collider2D _collider;
        protected ControllerMovementStateManager _movementStateManager;
        [SerializeField]
        private float _movementSpeed;
        [SerializeField]
        private float _jumpForce;


        protected virtual void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();
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
            _movementStateManager.StateTransitionUpdate();
        }
    }
}
