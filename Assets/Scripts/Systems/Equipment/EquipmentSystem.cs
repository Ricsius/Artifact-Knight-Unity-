
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
        public GameObject EquipedItem
        {
            get
            {
                return _equipedItemIndex >= 0
                    ? _inventory[ItemType.Equipable][_equipedItemIndex]
                    : null;
            }
        }
        public IEnumerable<KeyItem> Keys
        {
            get 
            {
                return _inventory[ItemType.Key]
                    .Select(i => i.GetComponent<KeyItem>())
                    .ToArray();
            }
        }
        public EventHandler NewItemEquipped;
        public EventHandler NewItemAdded;
        private Collider2D _collider;
        private int _equipedItemIndex = -1;
        private EquipableItem _equipedItemScript;
        private SpriteRenderer _equipedItemRenderer;
        private Collider2D _equipedItemCollider;
        private Dictionary<ItemType, List<GameObject>> _inventory;

        protected virtual void Awake()
        {
            _collider = GetComponent<Collider2D>();
            _inventory = new Dictionary<ItemType, List<GameObject>>();

            foreach (ItemType itemType in Enum.GetValues(typeof(ItemType)))
            {
                _inventory.Add(itemType, new List<GameObject>());
            }
        }

        protected virtual void OnDisable() 
        {
            StopUseEquipedItem();
        }

        public void AddItem(GameObject item)
        {
            ItemBase itemScript = item.GetComponent<ItemBase>();

            _inventory[itemScript.Type].Add(item);

            itemScript.OnAddedToEquipment(gameObject);

            if (itemScript.Type == ItemType.Equipable)
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
        }

        private void UnEquipEquipedItem()
        {
            _equipedItemScript = null;
            _equipedItemRenderer = null;
            _equipedItemCollider = null;
        }

        public void EquipNextItem()
        {
            if (_inventory[ItemType.Equipable].Any())
            {
                UnEquipEquipedItem();

                _equipedItemIndex = (_equipedItemIndex + 1) % _inventory[ItemType.Equipable].Count;

                _equipedItemScript = EquipedItem.GetComponent<EquipableItem>();
                _equipedItemRenderer = EquipedItem.GetComponent<SpriteRenderer>();
                _equipedItemCollider = EquipedItem.GetComponent<Collider2D>();

                NewItemEquipped?.Invoke(this, new ItemEventArgs(EquipedItem));
            }
        }

        public void UseEquipedItem()
        {
            if (EquipedItem != null)
            {
                _equipedItemRenderer.enabled = true;
                _equipedItemCollider.enabled = true;
                _equipedItemRenderer.sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder;
                _equipedItemScript.Use();
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
    }
}
