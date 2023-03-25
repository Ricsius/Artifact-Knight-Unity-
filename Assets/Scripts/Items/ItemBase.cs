
using UnityEngine;

namespace Assets.Scripts.Items
{
    public abstract class ItemBase : MonoBehaviour
    {
        public ItemType Type { get; protected set; }
        [field: SerializeField]
        public int ScoreValue { get; private set; }

        protected virtual void Awake()
        {
            tag = "Item";
        }

        public virtual void OnAddedToEquipment(GameObject newOwner)
        {
        }
    }
}
