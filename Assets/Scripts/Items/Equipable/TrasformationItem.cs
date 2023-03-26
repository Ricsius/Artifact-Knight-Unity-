using Assets.Scripts.Controllers;
using Assets.Scripts.Controllers.ControllerStates.MovementStates;
using Assets.Scripts.Detectors;
using Assets.Scripts.Spawners;
using Assets.Scripts.Timers;
using Assets.Scripts.Ui.Indicators;
using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Items.Equipable
{
    public class TrasformationItem : EquipableItem
    {
        [SerializeField]
        private float _transformationDuration;
        [SerializeField]
        private float _transformationMovementSpeed;
        [SerializeField]
        private float _transformationJumpForce;
        [SerializeField]
        private ControllerMovementStateType _transformationStartingMovementState;
        [SerializeField]
        private ControllerBase _defaultTrasformationController;
        private GameObject _owner;
        private bool _isOwnedByPlayer;
        private SpawnerBase _spawner;
        

        protected override void Awake()
        {
            base.Awake();

            _spawner= GetComponent<SpawnerBase>();
            _spawner.Spawned += OnTransformationSpawned;
        }
        public override void OnAddedToEquipment(GameObject newOwner)
        {
            _owner = newOwner;
            _isOwnedByPlayer = SpecialGameObjectRecognition.IsPlayer(_owner);
        }

        protected override void Effect()
        {
            _spawner.Spawn();
        }

        private void OnTransformationSpawned(object sender, EventArgs args)
        {
            SpawnedEventArgs spawnedArgs = args as SpawnedEventArgs;
            GameObject transformation = spawnedArgs.SpawnedObject;
            ControllerBase transformationController;
            DeathTimer deathTimer = transformation.AddComponent<DeathTimer>();

            deathTimer.DeathTime = _transformationDuration;

            _owner.gameObject.SetActive(false);
            _owner.transform.parent = transformation.transform;

            if (_isOwnedByPlayer)
            {
                transformationController = transformation.AddComponent<PlayerController>();
                transformation.AddComponent<PlayerTransformationIndicatorTarget>();
            }
            else
            {
                transformationController = transformation.AddComponent(_defaultTrasformationController.GetType()) as ControllerBase;
            }

            transformationController.MovementSpeed = _transformationMovementSpeed;
            transformationController.JumpForce= _transformationJumpForce;

            deathTimer.StartTimer();
        }
    }
}
