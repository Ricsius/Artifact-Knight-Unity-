using System;
using UnityEngine;

namespace Assets.Scripts.Systems.Equipment
{
    public class ItemEventArgs : EventArgs
    {
        public GameObject Item { get; }
        public ItemEventArgs(GameObject item)
        {
            Item = item;
        }
    }
}
