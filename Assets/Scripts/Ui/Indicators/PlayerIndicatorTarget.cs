using Assets.Scripts.Systems.Equipment;
using Assets.Scripts.Systems.Health;
using UnityEngine;

namespace Assets.Scripts.Ui.Indicators
{
    public class PlayerIndicatorTarget : MonoBehaviour
    {
        private HealthIndicator _playerHealthIndicator;
        private EquippedItemIndicator _playerEquippedItemIndicator;
        private HealthSystem _healthSystem;
        private EquipmentSystem _equipmentSystem;

        protected virtual void Awake()
        {
            _playerHealthIndicator = GameObject.Find("PlayerHealthIndicator").GetComponent<HealthIndicator>();
            _playerEquippedItemIndicator = GameObject.Find("PlayerEquippedItemIndicator").GetComponent<EquippedItemIndicator>();
            _healthSystem = GetComponent<HealthSystem>();
            _equipmentSystem = GetComponent<EquipmentSystem>();
        }

        protected virtual void OnEnable()
        {
            _playerHealthIndicator.HealthSystem = _healthSystem;
            _playerEquippedItemIndicator.EquipmentSystem = _equipmentSystem;
        }

        protected virtual void OnDisable()
        {
            _playerHealthIndicator.HealthSystem = null;
            _playerEquippedItemIndicator.EquipmentSystem = null;
        }
    }
}
