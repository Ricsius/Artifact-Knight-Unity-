
using Assets.Scripts.Controllers.ControllerStates.MovementStates;
using Assets.Scripts.Detectors;
using Assets.Scripts.Environment;
using Assets.Scripts.Items;
using Assets.Scripts.Items.Key;
using Assets.Scripts.Systems.Equipment;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class PlayerController : ControllerBase
    {
        private Transform _cameraTransform;
        private EquipmentSystem _equipment;
        private ItemDetector _itemDetector;
        private LadderDetector _ladderDetector;
        private DoorDetector _doorDetector;
        private ProfessorDetector _professorDetector;
        private const float _cameraYOffset = 1.5f;
        private KeyCode _moveRightKey = KeyCode.D;
        private KeyCode _moveLeftKey = KeyCode.A;
        private KeyCode _moveUpKey = KeyCode.W;
        private KeyCode _moveDownKey = KeyCode.S;
        private KeyCode _jumpKey = KeyCode.Space;
        private KeyCode _useItemKey = KeyCode.E;
        private KeyCode _interactionKey = KeyCode.F;
        private KeyCode _nextItemKey = KeyCode.Q;

        protected override void Awake()
        {
            base.Awake();

            tag = "Player";

            _cameraTransform = GameObject.Find("MainCamera").transform;
            _equipment = GetComponent<EquipmentSystem>();
            _itemDetector = GetComponent<ItemDetector>();
            _ladderDetector = GetComponent<LadderDetector>();
            _doorDetector = GetComponent<DoorDetector>();
            _professorDetector = GetComponent<ProfessorDetector>();

            _movementStateManager.Manage<DefaultGroundedMovementState>();
            _movementStateManager.Manage<DefaultInAirMovementState>();
            _movementStateManager.Manage<DefaultOnLadderMovementState>();
        }
        protected override void Update()
        {
            base.Update();

            _cameraTransform.position = new Vector3(transform.position.x, transform.position.y + _cameraYOffset, _cameraTransform.position.z);

            if (Input.GetKeyDown(_nextItemKey))
            {
                _equipment.EquipNextItem();
            }

            if ((Input.GetKeyUp(_moveUpKey) || Input.GetKeyUp(_moveDownKey))
                && _rigidBody.gravityScale == 0)
            {
                _movementStateManager.CurrentMovementState.StopMove();
            }

            if (Input.GetKeyUp(_moveRightKey))
            {
                _movementStateManager.CurrentMovementState.StopMove();
            }

            if (Input.GetKeyUp(_moveLeftKey))
            {
                _movementStateManager.CurrentMovementState.StopMove();
            }

            if (Input.GetKeyDown(_jumpKey) && _movementStateManager.CurrentMovementState.Type == ControllerMovementStateType.Grounded)
            {
                _movementStateManager.CurrentMovementState.Jump();
            }

            if (Input.GetKeyDown(_interactionKey))
            {
                Interact();
            }

            if (Input.GetKeyDown(_useItemKey))
            {
                _equipment.UseEquipedItem();
            }

            if (Input.GetKeyUp(_useItemKey))
            {
                _equipment.StopUseEquipedItem();
            }
        }
        protected virtual void FixedUpdate()
        {

            if (Input.GetKey(_moveUpKey) && !Input.GetKey(_moveDownKey) && _rigidBody.gravityScale == 0)
            {
                _movementStateManager.CurrentMovementState.Move(Vector2.up);
            }

            if (Input.GetKey(_moveDownKey) && !Input.GetKey(_moveUpKey) && _rigidBody.gravityScale == 0)
            {
                _movementStateManager.CurrentMovementState.Move(Vector2.down);
            }

            if (Input.GetKey(_moveUpKey) && Input.GetKey(_moveDownKey) && _rigidBody.gravityScale == 0)
            {
                _movementStateManager.CurrentMovementState.StopMove();
            }

            if (Input.GetKey(_moveRightKey) && Input.GetKey(_moveLeftKey))
            {
                _movementStateManager.CurrentMovementState.StopMove();
            }

            if (Input.GetKey(_moveRightKey) && !Input.GetKey(_moveLeftKey))
            {
                _movementStateManager.CurrentMovementState.Move(Vector2.right);
            }

            if (Input.GetKey(_moveLeftKey) && !Input.GetKey(_moveRightKey))
            {
                _movementStateManager.CurrentMovementState.Move(Vector2.left);
            }
        }

        private void Interact()
        {
            ItemBase item = _itemDetector?.Detect();

            if (item != null)
            {
                _equipment.AddItem(item);
            }

            if (_ladderDetector != null && _ladderDetector.Detect().Any())
            {
                _movementStateManager.TrySetCurrentMovementState(ControllerMovementStateType.OnLadder);
            }

            Door door = _doorDetector?.Detect();

            if (door != null)
            {
                KeyItem key = _equipment.GetKey(door.KeyTypeToOpen);

                if (key != null)
                {
                    door.TryOpen(key);
                }
            }

            Professor professor = _professorDetector?.Detect();

            if (professor != null)
            {
                professor.CheckEquipment(_equipment);
            }
        }
    }
}
