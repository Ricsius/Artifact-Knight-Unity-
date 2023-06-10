using Assets.Scripts.Spawners;
using UnityEngine;

namespace Assets.Scripts.TestScenario.Requirement
{
    public class SpawnTestRequirement : TestRequirementBase
    {
        [SerializeField]
        private int _requiredSpawnCount;
        private int _spawnCount;

        protected virtual void Awake()
        {
            _subject.GetComponent<SpawnerBase>().Spawned += OnSpawned;
        }
        public override bool Check()
        {
            return _spawnCount >= _requiredSpawnCount;
        }

        private void OnSpawned(object sender, SpawnedEventArgs args)
        {
            ++_spawnCount;
        }
    }
}
