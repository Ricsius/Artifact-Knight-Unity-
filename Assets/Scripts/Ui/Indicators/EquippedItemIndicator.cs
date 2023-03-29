using Assets.Scripts.Items.Equipable;
using Assets.Scripts.Systems.Equipment;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Ui.Indicators
{
    public class EquippedItemIndicator : IndicatorBase
    {
        public EquipmentSystem EquipmentSystem
        {
            set
            {
                UnsubscribeFromEvents();

                _equipmentSystem = value;

                SubscribeToEvents();
                ResetIndicator();
            }
        }
        private EquipmentSystem _equipmentSystem;
        private Image _itemImage;
        private EquipableItem _item;
        private TextMeshProUGUI _cooldownText;

        private void Awake()
        {
            _itemImage = transform.Find("ItemImage").GetComponent<Image>();
            _cooldownText = transform.Find("CooldownText").GetComponent<TextMeshProUGUI>();

            _cooldownText.text = string.Empty;
        }

        protected override void SubscribeToEvents()
        {
            if (_equipmentSystem != null)
            {
                _equipmentSystem.NewItemEquipped += OnNewItemEquipped;
            }
        }

        protected override void UnsubscribeFromEvents()
        {
            if (_equipmentSystem != null)
            {
                _equipmentSystem.NewItemEquipped -= OnNewItemEquipped;
            }
        }

        protected virtual void Update()
        {
            if (_item != null)
            {
                _cooldownText.text = _item.IsOnCooldown
                ? string.Format("{0:0.0}", _item.TimeTillCooldown)
                : string.Empty;
            }
        }

        protected override void ResetIndicator()
        {
            if (_equipmentSystem != null)
            {
                if (_equipmentSystem.EquipedItem != null)
                {
                    _itemImage.sprite = _equipmentSystem.EquipedItem.GetComponent<SpriteRenderer>().sprite;
                    _itemImage.enabled = true;
                }
                else
                {
                    _itemImage.enabled = false;
                    _itemImage.sprite = null;
                    _cooldownText.text = string.Empty;
                }
            }
        }

        private void OnNewItemEquipped(object sender, EventArgs args)
        {
            ItemEventArgs itemEquipArgs = args as ItemEventArgs;

            _item = itemEquipArgs.Item as EquipableItem;
            _itemImage.sprite = itemEquipArgs.Item.GetComponent<SpriteRenderer>().sprite;
            _itemImage.enabled = true;
        }
    }
}
