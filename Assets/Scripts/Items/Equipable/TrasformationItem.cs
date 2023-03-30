using Assets.Scripts.Controllers;
using Assets.Scripts.Controllers.ControllerStates.MovementStates;
using Assets.Scripts.Detectors;
using Assets.Scripts.Environment.Checkpoint;
using Assets.Scripts.Spawners;
using Assets.Scripts.Timers;
using Assets.Scripts.Ui.Indicators;
using System;
using UnityEngine;

namespace Assets.Scripts.Items.Equipable
{
    public class TrasformationItem : EquipableItem
    {
        [SerializeField]
        private float _transformationDuration;
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
            DeathTimer deathTimer = transformation.AddComponent<DeathTimer>();

            deathTimer.DeathTime = _transformationDuration;

            _owner.gameObject.SetActive(false);
            _owner.transform.parent = transformation.transform;

            if (_isOwnedByPlayer)
            {
                ControllerBase transformationController = transformation.GetComponent<ControllerBase>();
                float speed = transformationController.MovementSpeed;
                float jumpForce = transformationController.JumpForce;

                Destroy(transformationController);
                //ToDo: Fix Dragon warnings
                PlayerController playerController = transformation.AddComponent<PlayerController>();

                playerController.MovementSpeed = speed;
                playerController.JumpForce = jumpForce;

                transformation.AddComponent<PlayerTransformationIndicatorTarget>();
                transformation.AddComponent<CheckpointManagerTarget>();
            }

            deathTimer.StartTimer();
        }
    }
}
