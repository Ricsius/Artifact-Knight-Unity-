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
            set
            {
                UnsubscribeFromEvents();

                _healthSysyem = value;

                if (isActiveAndEnabled)
                {
                    SubscribeToEvents();
                    ResetIndicator();
                }
            }
        }
        [SerializeField]
        private HealthSystem _healthSysyem;

        private void Start()
        {
            HealthSystem = _healthSysyem;
        }

        protected override void SubscribeToEvents()
        {
            if (_healthSysyem != null)
            {
                _healthSysyem.Healed += UpdateHealthIndicator;
                _healthSysyem.TookDamage += UpdateHealthIndicator;
            }
        }

        protected override void UnsubscribeFromEvents()
        {
            if (_healthSysyem != null)
            {
                _healthSysyem.Healed -= UpdateHealthIndicator;
                _healthSysyem.TookDamage -= UpdateHealthIndicator;
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
            RemoveHealthIcons(transform.childCount);
            AddHealthIcons(_healthSysyem.CurrentHealthPoints);
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
