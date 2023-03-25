
using UnityEngine;

namespace Assets.Scripts.Spawners
{
    public class TimedSpawner : SpawnerBase
    {
        [field: SerializeField]
        public float SpawnInterval { get; set; }
        [field: SerializeField]
        public bool AutoReset { get; set; }
        [field: SerializeField]
        public bool DestroyOnSpawn { get; set; }
        private float _timeTillSpawn;
        private bool _onCooldown => _timeTillSpawn > 0;
        protected virtual void Awake()
        {
            _timeTillSpawn = SpawnInterval;
        }
        protected virtual void Update()
        {
            if (_onCooldown)
            {
                _timeTillSpawn -= Time.deltaTime;
            }
            else
            {
                Spawn();

                if (AutoReset)
                {
                    ResetSpawnInterval();

                }

                if (DestroyOnSpawn)
                {
                    Destroy(gameObject);
                }
            }
        }

        public void ResetSpawnInterval()
        {
            _timeTillSpawn = SpawnInterval;
        }
    }
}
