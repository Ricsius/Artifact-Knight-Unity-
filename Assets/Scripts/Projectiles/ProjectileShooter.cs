
using Assets.Scripts.Spawners;
using System;
using UnityEngine;

namespace Assets.Scripts.Projectiles
{
    public class ProjectileShooter : MonoBehaviour
    {
        public float ShootingForce;
        private SpawnerBase _spawner;
        private void Awake()
        {
            _spawner = GetComponent<SpawnerBase>();
        }

        private void OnEnable()
        {
            _spawner.Spawned += OnSpawned;
        }

        private void OnDisable()
        {
            _spawner.Spawned -= OnSpawned;
        }

        private void Shoot(GameObject projectile)
        {
            Vector3 offset = transform.right * (projectile.GetComponent<Collider2D>().bounds.extents.x);

            projectile.transform.position += offset;
            projectile.GetComponent<Rigidbody2D>().AddForce(transform.right * ShootingForce, ForceMode2D.Impulse);
        }

        private void OnSpawned(object sender, SpawnedEventArgs args)
        {
            GameObject projectile = args.SpawnedObject;
           
            Shoot(projectile);
        }
    }
}
