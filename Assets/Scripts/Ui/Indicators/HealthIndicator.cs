using Assets.Scripts.Systems.Health;
using System;
using UnityEngine;

namespace Assets.Scripts.Ui.Indicators
{
    public class HealthIndicator : IndicatorBase
    {
        [field: SerializeField]
        public GameObject HealthIcon { get; private set; }
        public HealthSystem HealthSystem
        {
            get
            {
                return _healthSystem;
            }
            set
            {
                UnsubscribeFromEvents();

                _healthSystem = value;

                SubscribeToEvents();
                ResetIndicator();
            }
        }

        private HealthSystem _healthSystem;

        protected override void SubscribeToEvents()
        {
            if (_healthSystem != null)
            {
                _healthSystem.Healed += UpdateHealthIndicator;
                _healthSystem.TookDamage += UpdateHealthIndicator;
            }
        }

        protected override void UnsubscribeFromEvents()
        {
            if (_healthSystem != null)
            {
                _healthSystem.Healed -= UpdateHealthIndicator;
                _healthSystem.TookDamage -= UpdateHealthIndicator;
            }
        }

        private void AddHealthIcons(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Instantiate(HealthIcon).transform.SetParent(transform);
            }
        }

        private void RemoveHealthIcons(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        protected override void ResetIndicator()
        {
            if (_healthSystem != null)
            {
                RemoveHealthIcons(transform.childCount);
                AddHealthIcons(_healthSystem.CurrentHealthPoints);
            }
        }

        private void UpdateHealthIndicator(object sender, EventArgs args)
        {
            HealthChangeEventArgs healthChange = args as HealthChangeEventArgs;

            if (healthChange.IsDamage)
            {
                RemoveHealthIcons(healthChange.HealthChange);
            }
            else
            {
                AddHealthIcons(healthChange.HealthChange);
            }
        }
    }
}
