
using System;
using UnityEngine;

namespace Assets.Scripts.Spawners
{
    public class SpawnerBase : MonoBehaviour
    {
        [field: SerializeField]
        public GameObject SpawnEffect { get; set; }
        [field: SerializeField]
        public GameObject ObjectToSpawn { get; set; }
        public event EventHandler Spawned;

        public void Spawn()
        {
            if (SpawnEffect != null)
            {
                GameObject spawnedEffect = Instantiate(SpawnEffect, transform.position, transform.rotation);
                spawnedEffect.transform.localScale = transform.localScale;
            }

            if (ObjectToSpawn != null)
            {
                GameObject spawnedObject = Instantiate(ObjectToSpawn, transform.position, transform.rotation);
                spawnedObject.transform.localScale = transform.localScale;

                Spawned?.Invoke(this, new SpawnedEventArgs(spawnedObject));
            }
        }
    }
}
