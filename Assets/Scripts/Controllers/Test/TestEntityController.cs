
using Assets.Scripts.Controllers.ControllerStates.MovementStates;
using Assets.Scripts.Detectors;
using Assets.Scripts.Environment;
using Assets.Scripts.Items.Key;
using Assets.Scripts.Items;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Systems.Equipment;
using System;
using System.Linq;

namespace Assets.Scripts.Controllers.Test
{
    public class TestEntityController : ControllerBase
    {
        private EquipmentSystem _equipment;
        private ItemDetector _itemDetector;
        private LadderDetector _ladderDetector;
        private DoorDetector _doorDetector;
        private ProfessorDetector _professorDetector;
        private ChestDetector _chestDetector;
        [SerializeField]
        private List<TestAction> _actions;
        [SerializeField]
        private TestAction _currentAction;
        private int _currentActionIndex;
        private float _timeTillNextAction;
        private Dictionary<TestActionType, Action> _actionMappings;

        protected override void Awake()
        {
            base.Awake();

            _equipment = GetComponent<EquipmentSystem>();
            _itemDetector = GetComponent<ItemDetector>();
            _ladderDetector = GetComponent<LadderDetector>();
            _doorDetector = GetComponent<DoorDetector>();
            _professorDetector = GetComponent<ProfessorDetector>();
            _chestDetector = GetComponent<ChestDetector>();

            _actions = GetComponentsInChildren<TestAction>().ToList();

            _actionMappings = new Dictionary<TestActionType, Action>
            {
                { TestActionType.Nothing, () => { } },
                { TestActionType.MoveUp, () => _movementStateManager.CurrentMovementState.Move(Vector2.up) },
                { TestActionType.MoveDown, () => _movementStateManager.CurrentMovementState.Move(Vector2.down) },
                { TestActionType.MoveLeft, () => _movementStateManager.CurrentMovementState.Move(Vector2.left) },
                { TestActionType.MoveRight, () => _movementStateManager.CurrentMovementState.Move(Vector2.right) },
                { TestActionType.UseEquippedItem, () => _equipment.UseEquippedItem() },
                { TestActionType.StopUseEquippedItem, () => _equipment.StopUseEquippedItem() },
            };

            _currentActionIndex =  -1;

            _movementStateManager.Manage<DefaultGroundedMovementState>();
            _movementStateManager.Manage<DefaultInAirMovementState>();

            SelectNextAction();
        }

        protected override void Update()
        {
            base.Update();

            _timeTillNextAction -= Time.deltaTime;

            if (_timeTillNextAction > 0)
            {
                _actionMappings[_currentAction.Type]();
            }
            else
            {
                SelectNextAction();
            }
        }

        private void SelectNextAction()
        {
            _currentActionIndex = (_currentActionIndex + 1) % _actions.Count;
            _currentAction= _actions[_currentActionIndex];
            _timeTillNextAction = _currentAction.Duration;
        }
    }
}
