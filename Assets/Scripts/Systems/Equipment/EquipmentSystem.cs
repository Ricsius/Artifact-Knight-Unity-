
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
        public EventHandler<ItemEventArgs> NewItemEquipped;
        public EventHandler<ItemEventArgs> NewItemAdded;
        public EventHandler<ItemEventArgs> ItemRemoved;
        private Collider2D _collider;
        private LinkedListNode<ItemBase> _equippedItemNode;
        private SpriteRenderer _equipedItemRenderer;
        private Collider2D _equipedItemCollider;
        private Dictionary<ItemType, LinkedList<ItemBase>> _inventory;
        [SerializeField]
        private List<ItemBase> _preparedItems;

        protected virtual void Awake()
        {
            _collider = GetComponent<Collider2D>();
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

                AddItem(cloneItem, false);
            }
        }
        
        protected virtual void OnDisable() 
        {
            StopUseEquipedItem();
        }

        public void AddItem(ItemBase item)
        {
            AddItem(item, true);
        }

        public void RemoveItem(ItemBase item)
        {
            if (item == EquipedItem)
            {
                EquipNextItem();
            }

            bool removed = _inventory[item.Type].Remove(item);

            if (removed)
            {
                ItemRemoved?.Invoke(this, new ItemEventArgs(item));
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

            UnEquipEquipedItem();

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

                _equipedItemRenderer = EquipedItem.GetComponent<SpriteRenderer>();
                _equipedItemCollider = EquipedItem.GetComponent<Collider2D>();
            }

            NewItemEquipped?.Invoke(this, new ItemEventArgs(EquipedItem));
        }

        public void UseEquipedItem()
        {
            if (EquipedItem != null)
            {
                _equipedItemRenderer.enabled = true;
                _equipedItemCollider.enabled = true;
                _equipedItemRenderer.sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder;
                EquipedItem.Use();
            }
        }

        public void StopUseEquipedItem()
        {
            if (EquipedItem != null)
            {
                _equipedItemRenderer.enabled = false;
                _equipedItemCollider.enabled = false;
            }
        }

        private void AddItem(ItemBase item, bool invokeEvent)
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

            if (invokeEvent)
            {
                NewItemAdded?.Invoke(this, new ItemEventArgs(item));
            }

            item.GetComponent<SpriteRenderer>().enabled = false;
            item.GetComponent<Collider2D>().enabled = false;
        }

        private void UnEquipEquipedItem()
        {
            _equipedItemRenderer = null;
            _equipedItemCollider = null;
        }
    }
}
