using System;
using UnityEngine;

namespace Assets.Scripts.Spawners
{
    public class SpawnedEventArgs : EventArgs
    {
        public GameObject SpawnedObject { get; set; }

        public SpawnedEventArgs(GameObject gameObject)
        {
            SpawnedObject = gameObject;
        }
    }
}
