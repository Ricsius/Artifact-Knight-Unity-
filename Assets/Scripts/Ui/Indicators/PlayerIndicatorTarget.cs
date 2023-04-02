using Assets.Scripts.Systems.Equipment;
using Assets.Scripts.Systems.Health;
using Assets.Scripts.Systems.Score;
using UnityEngine;

namespace Assets.Scripts.Ui.Indicators
{
    public class PlayerIndicatorTarget : MonoBehaviour
    {
        private HealthIndicator _playerHealthIndicator;
        private EquippedItemIndicator _playerEquippedItemIndicator;
        private ScoreIndicator _playerScoreIndicator;
        private HealthSystem _healthSystem;
        private EquipmentSystem _equipmentSystem;
        private ScoreSystem _scoreSystem;

        protected virtual void Awake()
        {
            _playerHealthIndicator = GameObject.Find("PlayerHealthIndicator").GetComponent<HealthIndicator>();
            _playerEquippedItemIndicator = GameObject.Find("PlayerEquippedItemIndicator").GetComponent<EquippedItemIndicator>();
            _playerScoreIndicator = GameObject.Find("PlayerScoreIndicator").GetComponent<ScoreIndicator>();
            _healthSystem = GetComponent<HealthSystem>();
            _equipmentSystem = GetComponent<EquipmentSystem>();
            _scoreSystem = GetComponent<ScoreSystem>();
        }

        protected virtual void OnEnable()
        {
            _playerHealthIndicator.HealthSystem = _healthSystem;
            _playerEquippedItemIndicator.EquipmentSystem = _equipmentSystem;
            _playerScoreIndicator.ScoreSystem= _scoreSystem;
        }

        protected virtual void OnDestroy()
        {
            if (_playerHealthIndicator.HealthSystem == _healthSystem)
            {
                _playerHealthIndicator.HealthSystem = null;
            }

            if (_playerEquippedItemIndicator.EquipmentSystem == _equipmentSystem)
            {
                _playerEquippedItemIndicator.EquipmentSystem = null;
            }

            if (_playerScoreIndicator.ScoreSystem == _scoreSystem)
            {
                _playerScoreIndicator.ScoreSystem = null;
            }
        }
    }
}
