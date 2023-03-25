using Assets.Scripts.Controllers;
using Assets.Scripts.Spawners;
using System;
using UnityEngine;

namespace Assets.Scripts.Items.Equipable
{
    public class StatueOfDragonflight : EquipableItem
    {
        public SpawnerBase _spawner;
        private ControllerBase _controller;

        protected override void Awake()
        {
            base.Awake();

            _spawner= GetComponent<SpawnerBase>();
            _spawner.Spawned += (object sender, EventArgs args) =>
            {
                SpawnedEventArgs spawnedArgs = args as SpawnedEventArgs;
                GameObject dragon = spawnedArgs.SpawnedObject;
                ControllerBase dragonController = dragon.AddComponent(_controller.GetType()) as ControllerBase;

                dragonController.MovementSpeed = 3;
                _controller.gameObject.SetActive(false);
                _controller.transform.parent = dragon.transform;
            };
        }
        public override void OnAddedToEquipment(GameObject newOwner)
        {
            _controller = newOwner.GetComponent<ControllerBase>();
        }

        protected override void Effect()
        {
            _spawner.Spawn();
        }
    }
}
