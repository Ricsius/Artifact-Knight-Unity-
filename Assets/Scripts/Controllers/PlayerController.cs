
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
        private ChestDetector _chestDetector;
        private float _cameraYOffset;
        private KeyCode _moveRightKey;
        private KeyCode _moveLeftKey;
        private KeyCode _moveUpKey;
        private KeyCode _moveDownKey;
        private KeyCode _jumpKey;
        private KeyCode _useItemKey;
        private KeyCode _interactionKey;
        private KeyCode _nextItemKey;

        protected override void Awake()
        {
            base.Awake();

            _cameraTransform = GameObject.Find("Main Camera").transform;
            _equipment = GetComponent<EquipmentSystem>();
            _itemDetector = GetComponent<ItemDetector>();
            _ladderDetector = GetComponent<LadderDetector>();
            _doorDetector = GetComponent<DoorDetector>();
            _professorDetector = GetComponent<ProfessorDetector>();
            _chestDetector = GetComponent<ChestDetector>();

            _movementStateManager.Manage<DefaultGroundedMovementState>();
            _movementStateManager.Manage<DefaultInAirMovementState>();
            _movementStateManager.Manage<DefaultOnLadderMovementState>();

            _cameraYOffset = 1.5f;
            _moveRightKey = KeyCode.D;
            _moveLeftKey = KeyCode.A;
            _moveUpKey = KeyCode.W;
            _moveDownKey = KeyCode.S;
            _jumpKey = KeyCode.Space;
            _useItemKey = KeyCode.E;
            _interactionKey = KeyCode.F;
            _nextItemKey = KeyCode.Q;
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
                _equipment.UseEquippedItem();
            }

            if (Input.GetKeyUp(_useItemKey))
            {
                _equipment.StopUseEquippedItem();
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

            _professorDetector?.Detect()?.CheckEquipment(_equipment);
            _chestDetector?.Detect()?.Open();
        }
    }
}
