
using Assets.Scripts.Items.Equipable;
using Assets.Scripts.Systems.Equipment;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Ui.Indicators
{
    public class EquippedItemIndicator : IndicatorBase<EquipmentSystem>
    {
        [SerializeField]
        private Image _itemImage;
        private EquipableItem _item;
        private TextMeshProUGUI _cooldownText;

        protected virtual void Awake()
        {
            _cooldownText = GetComponentInChildren<TextMeshProUGUI>();

            _cooldownText.text = string.Empty;
        }

        protected override void SubscribeToEvents()
        {
            if (_componentToIndicateTyped != null)
            {
                _componentToIndicateTyped.NewItemEquipped += OnNewItemEquipped;
            }
        }

        protected override void UnsubscribeFromEvents()
        {
            if (_componentToIndicateTyped != null)
            {
                _componentToIndicateTyped.NewItemEquipped -= OnNewItemEquipped;
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
            IndicateItem(_componentToIndicateTyped?.EquipedItem);

            _cooldownText.text = string.Empty;
        }

        private void OnNewItemEquipped(object sender, ItemEventArgs args)
        {
            IndicateItem(args.Item as EquipableItem);
        }

        private void IndicateItem(EquipableItem item)
        {
            _item = item;
            _itemImage.sprite = _item?.GetComponent<SpriteRenderer>().sprite;
            _itemImage.enabled = item != null;
        }
    }
}
