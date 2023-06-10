
using Assets.Scripts.Items;
using Assets.Scripts.Items.Equipable;
using Assets.Scripts.Items.Key;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Systems.Equipment
{
    public class EquipmentSystem : MonoBehaviour
    {
        public EquipableItem EquipedItem => _equippedItemNode?.Value as EquipableItem;
        public event EventHandler<ItemEventArgs> NewItemEquipped;
        public event EventHandler<ItemEventArgs> NewItemAdded;
        public event EventHandler<ItemEventArgs> ItemRemoved;
        private Collider2D _collider;
        private SpriteRenderer _spriteRenderer;
        private LinkedListNode<ItemBase> _equippedItemNode;
        private SpriteRenderer _equippedItemRenderer;
        private Dictionary<ItemType, LinkedList<ItemBase>> _inventory;
        [SerializeField]
        private List<ItemBase> _preparedItems;
        private bool _equippedItemInUse;

        protected virtual void Awake()
        {
            _collider = GetComponent<Collider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _inventory = new Dictionary<ItemType, LinkedList<ItemBase>>();

            foreach (ItemType itemType in Enum.GetValues(typeof(ItemType)))
            {
                _inventory.Add(itemType, new LinkedList<ItemBase>());
            }
        }

        protected virtual void Start()
        {
            foreach (ItemBase item in _preparedItems)
            {
                ItemBase cloneItem = Instantiate(item);

                AddItem(cloneItem);
            }
        }
        
        protected virtual void OnDisable() 
        {
            StopUseEquippedItem();
        }

        public void AddItem(ItemBase item)
        {
            _inventory[item.Type].AddLast(item);

            item.OnAddedToEquipment(gameObject);

            if (item.Type == ItemType.Equipable)
            {
                Collider2D itemCollider = item.GetComponent<Collider2D>();
                float sign = transform.right.x > 0f ? 1f : -1f;
                Vector3 offset = sign * new Vector3(_collider.bounds.extents.x + itemCollider.bounds.extents.x, 0, 0);

                item.transform.parent = transform;
                item.transform.position = transform.position + offset;
                item.transform.rotation = transform.rotation;

                if (_inventory[ItemType.Equipable].Count == 1)
                {
                    EquipNextItem();
                }
            }

            NewItemAdded?.Invoke(this, new ItemEventArgs(item));

            item.Hide();
        }

        public void RemoveItem(ItemBase item)
        {
            bool removed = _inventory[item.Type].Remove(item);

            if (removed)
            {
                item.transform.parent = null;
                ItemRemoved?.Invoke(this, new ItemEventArgs(item));

                if (item == EquipedItem)
                {
                    UnEquipEquippedItem();
                    EquipNextItem();
                }

                item.OnRemovedFromEquipment();
            }
        }

            public bool ContainsItem(ItemBase item)
        {
            return _inventory.Any(p => p.Value.Contains(item));
        }

        public KeyItem GetKey(KeyType keyType)
        {
            return _inventory[ItemType.Key]
                .Select(i => i as KeyItem)
                .FirstOrDefault(k => k .KeyType == keyType);
        }

        public void EquipNextItem()
        {
            LinkedList<ItemBase> equipableItems = _inventory[ItemType.Equipable];

            if (equipableItems.Any())
            {
                if (_equippedItemNode == null)
                {
                    _equippedItemNode = equipableItems.First;
                }
                else
                {
                    LinkedListNode<ItemBase> nextNode = _equippedItemNode.Next;

                    _equippedItemNode = nextNode == null ? equipableItems.First : nextNode;
                }

                _equippedItemRenderer = EquipedItem.GetComponent<SpriteRenderer>();
            }

            NewItemEquipped?.Invoke(this, new ItemEventArgs(EquipedItem));
        }

        public void UseEquippedItem()
        {
            if (EquipedItem != null && !_equippedItemInUse)
            {
                EquipedItem.Reveal();

                _equippedItemRenderer.sortingOrder = _spriteRenderer.sortingOrder;

                EquipedItem.Use();

                _equippedItemInUse = true;
            }
        }

        public void StopUseEquippedItem()
        {
            EquipedItem?.Hide();
            _equippedItemInUse = false;
        }

        private void UnEquipEquippedItem()
        {
            _equippedItemNode = null;
            _equippedItemRenderer = null;
        }
    }
}
