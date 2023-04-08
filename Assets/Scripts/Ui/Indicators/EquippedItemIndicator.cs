
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
            get
            {
                return _equipmentSystem;
            }
            set
            {
                UnsubscribeFromEvents();

                _equipmentSystem = value;

                SubscribeToEvents();
                ResetIndicator();
            }
        }
        private EquipmentSystem _equipmentSystem;
        [SerializeField]
        private Image _itemImage;
        private EquipableItem _item;
        [SerializeField]
        private TextMeshProUGUI _cooldownText;

        private void Awake()
        {
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
            IndicateItem(_equipmentSystem?.EquipedItem);

            _cooldownText.text = string.Empty;
        }

        private void OnNewItemEquipped(object sender, EventArgs args)
        {
            ItemEventArgs itemEquipArgs = args as ItemEventArgs;

            IndicateItem(itemEquipArgs.Item as EquipableItem);
        }

        private void IndicateItem(EquipableItem item)
        {
            _item = item;
            _itemImage.sprite = _item?.GetComponent<SpriteRenderer>().sprite;
            _itemImage.enabled = item != null;
        }
    }
}
