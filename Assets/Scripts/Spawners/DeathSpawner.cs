
using Assets.Scripts.Systems.Health;
using System;

namespace Assets.Scripts.Spawners
{
    public class DeathSpawner : SpawnerBase
    {
        private HealthSystem _healthSystem;

        private void Awake()
        {
            _healthSystem = GetComponent<HealthSystem>();
        }
        private void OnEnable()
        {
            _healthSystem.Death += OnDeath;
        }

        private void OnDisable()
        {
            _healthSystem.Death -= OnDeath;
        }

        private void OnDeath(object sender, EventArgs args)
        {
            Spawn();
        }
    }
}
