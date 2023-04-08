
using Assets.Scripts.Detectors;
using Assets.Scripts.Projectiles;
using Assets.Scripts.Spawners;
using System;
using UnityEngine;

namespace Assets.Scripts.Items.Equipable
{
    public class RangedWeapon : EquipableItem
    {
        protected SpawnerBase _spawner;

        protected override void Awake()
        {
            base.Awake();
            _spawner = GetComponentInChildren<SpawnerBase>();
        }

        protected virtual void Start()
        {
            bool isShootingHomingProjectile = _spawner.ObjectToSpawn?.GetComponent<HomingProjectile>() != null;

            _spawner.Spawned += OnProjectileSpawned;
        }

        protected override void Effect()
        {
            _spawner.Spawn();
        }

        private void OnProjectileSpawned(object sender, EventArgs args)
        {
            SpawnedEventArgs spawnedArgs = args as SpawnedEventArgs;
            HomingProjectile homingProjectile = spawnedArgs.SpawnedObject.GetComponent<HomingProjectile>();

            if (homingProjectile != null)
            {
                if (_isOwnedByPlayer)
                {
                    homingProjectile.TargetSelector = o => SpecialGameObjectRecognition.IsMob(o);
                }
                else
                {
                    homingProjectile.TargetSelector = o => SpecialGameObjectRecognition.IsPlayer(o);
                }
            }
        }
    }
}
