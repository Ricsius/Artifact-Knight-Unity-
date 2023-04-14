using Assets.Scripts.Controllers;
using Assets.Scripts.Environment.Checkpoint;
using Assets.Scripts.Spawners;
using Assets.Scripts.Systems.Health;
using Assets.Scripts.Ui.Indicators.Targets;
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
                ControllerBase originalController = transformation.GetComponent<ControllerBase>();
                CheckpointManagerTarget checkpointManagerTarget = Owner.GetComponent<CheckpointManagerTarget>();
                float speed = originalController.MovementSpeed;
                float jumpForce = originalController.JumpForce;

                DestroyImmediate(originalController);
                DestroyImmediate(checkpointManagerTarget);

                PlayerController playerController = transformation.AddComponent<PlayerController>();

                playerController.MovementSpeed = speed;
                playerController.JumpForce = jumpForce;

                transformation.AddComponent<PlayerIndicatorGroupTarget>();
                transformation.AddComponent<CheckpointManagerTarget>();
            }

            Owner.transform.parent = transformation.transform;
            Owner.gameObject.SetActive(false);
        }

        private void OnTransformationSpawned(object sender, SpawnedEventArgs args)
        {
            GameObject transformation = args.SpawnedObject;

            TransformOwnerInto(transformation);
        }
    }
}
