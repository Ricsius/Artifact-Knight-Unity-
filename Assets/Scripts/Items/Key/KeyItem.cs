
using UnityEngine;

namespace Assets.Scripts.Items.Key
{
    public class KeyItem : ItemBase
    {
        [field: SerializeField]
        public KeyType KeyType { get; private set; }
        protected override void Awake()
        {
            base.Awake();

            Type = ItemType.Key;
        }
    }
}
