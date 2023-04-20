
using UnityEngine;

namespace Assets.Scripts.Items
{
    public abstract class ItemBase : MonoBehaviour
    {
        public ItemType Type { get; protected set; }
        [field: SerializeField]
        public int ScoreValue { get; private set; }
        protected Collider2D _collider;
        protected SpriteRenderer _spriteRenderer;

        protected virtual void Awake()
        {
            tag = "Item";
            _collider = GetComponent<Collider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public virtual void OnAddedToEquipment(GameObject newOwner)
        {
            tag= "Owned_Item";
        }

        public virtual void OnRemovedFromEquipment()
        {
            tag = "Item";
        }

        public void Hide()
        {
            _collider.enabled = false;
            _spriteRenderer.enabled = false;
        }

        public void Reveal()
        {
            _collider.enabled = true;
            _spriteRenderer.enabled = true;
        }
    }
}
