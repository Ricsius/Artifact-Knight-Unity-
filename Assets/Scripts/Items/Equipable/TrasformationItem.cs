using Assets.Scripts.Controllers;
using Assets.Scripts.Environment.Checkpoint;
using Assets.Scripts.Spawners;
using Assets.Scripts.Systems.Health;
using Assets.Scripts.Ui.Indicators;
using System;
using UnityEngine;

namespace Assets.Scripts.Items.Equipable
{
    public class TrasformationItem : EquipableItem
    {
        [SerializeField]
        private float _transformationDuration;
        private SpawnerBase _spawner;
        

        protected override void Awake()
        {
            base.Awake();

            _spawner= GetComponent<SpawnerBase>();
            _spawner.Spawned += OnTransformationSpawned;
        }

        protected override void Effect()
        {
            _spawner.Spawn();
        }

        private void TransformOwnerInto(GameObject transformation)
        {
            HealthSystem originalHealthSystem = transformation.GetComponent<HealthSystem>();
            int maxHealthPoints = originalHealthSystem.MaxHealthPoints;

            DestroyImmediate(originalHealthSystem);

            TransformationHealthSystem transformationHealthSystem = transformation.AddComponent<TransformationHealthSystem>();

            transformationHealthSystem.MaxHealthPoints = maxHealthPoints;
            transformationHealthSystem.Heal(maxHealthPoints);
            transformationHealthSystem.TransformationDuration = _transformationDuration;

            if (_isOwnedByPlayer)
            {
                PlayerIndicatorTarget playerIndicatorTarget= Owner.GetComponent<PlayerIndicatorTarget>();
                CheckpointManagerTarget checkpointManagerTarget = Owner.GetComponent<CheckpointManagerTarget>();
                ControllerBase originalController = transformation.GetComponent<ControllerBase>();
                float speed = originalController.MovementSpeed;
                float jumpForce = originalController.JumpForce;

                DestroyImmediate(playerIndicatorTarget);
                DestroyImmediate(checkpointManagerTarget);
                DestroyImmediate(originalController);

                PlayerController playerController = transformation.AddComponent<PlayerController>();

                playerController.MovementSpeed = speed;
                playerController.JumpForce = jumpForce;

                transformation.AddComponent<PlayerTransformationIndicatorTarget>();
                transformation.AddComponent<CheckpointManagerTarget>();
            }

            Owner.gameObject.SetActive(false);
            Owner.transform.parent = transformation.transform;
        }

        private void OnTransformationSpawned(object sender, EventArgs args)
        {
            SpawnedEventArgs spawnedArgs = args as SpawnedEventArgs;
            GameObject transformation = spawnedArgs.SpawnedObject;

            TransformOwnerInto(transformation);
        }
    }
}
