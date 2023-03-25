
using Assets.Scripts.Detectors;
using Assets.Scripts.Spawners;
using System;
using UnityEngine;

namespace Assets.Scripts.Environment
{
    public class Checkpoint : MonoBehaviour
    {
        [field: SerializeField]
        public GameObject ActivatedCheckpoint { get; set; }
        public event EventHandler Activated;
        private SpawnerBase _spawner;

        protected virtual void Awake()
        {
            _spawner= GetComponent<SpawnerBase>();
        }
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (SpecialGameObjectRecognition.IsPlayer(collision.gameObject))
            {
                Activate();
            }
        }

        public void Activate()
        {
            _spawner.Spawn();
            Activated?.Invoke(this, new EventArgs());
            Destroy(gameObject);
        }
    }
}
