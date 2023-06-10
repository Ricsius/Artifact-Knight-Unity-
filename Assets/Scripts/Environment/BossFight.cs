using Assets.Scripts.Detectors;
using Assets.Scripts.Items;
using Assets.Scripts.Spawners;
using Assets.Scripts.Systems.Health;
using Assets.Scripts.UI.Indicators;
using Assets.Scripts.UI.Indicators.Targets;
using System;
using UnityEngine;

namespace Assets.Scripts.Environment
{
    public class BossFight : MonoBehaviour
    {
        [SerializeField]
        private bool _isBossStationary;
        [SerializeField]
        private ItemBase _reward;
        private SpawnerBase _spawner;
        private GameObject _currentBossInstance;

        protected virtual void Awake()
        {
            _spawner = GetComponentInChildren<SpawnerBase>();

            _spawner.Spawned += OnBossSpawn;
            _reward.gameObject.SetActive(false);
        }

        protected virtual void OnTriggerEnter2D(Collider2D collider)
        {
            if (SpecialGameObjectRecognition.IsPlayer(collider.gameObject))
            {
                _spawner.Spawn();
            }
        }

        protected virtual void OnTriggerExit2D(Collider2D collider)
        {
            if (SpecialGameObjectRecognition.IsPlayer(collider.gameObject))
            {
                Destroy(_currentBossInstance);
            }
        }

        private void OnBossDeath(object sender, EventArgs args)
        {
            _reward.gameObject.SetActive(true);

            Destroy(this);
        }

        private void OnBossSpawn(object sender, SpawnedEventArgs args)
        {
            _currentBossInstance = args.SpawnedObject;
            _currentBossInstance.AddComponent<BossIndicatorGroupTarget>();

            HealthSystem bossHealthSystem = _currentBossInstance.GetComponent<HealthSystem>();

            bossHealthSystem.Death += OnBossDeath;

            if (_isBossStationary)
            {
                Rigidbody2D rigidbody = _currentBossInstance.GetComponent<Rigidbody2D>();

                if (rigidbody != null) 
                {
                    rigidbody.constraints = rigidbody.constraints | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
                }
            }
        }
    }
}
