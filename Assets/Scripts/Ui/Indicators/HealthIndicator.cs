using Assets.Scripts.Systems.Health;
using UnityEngine;

namespace Assets.Scripts.UI.Indicators
{
    public class HealthIndicator : IndicatorBase<HealthSystem>
    {
        [SerializeField]
        private GameObject _healthIcon;

        protected override void SubscribeToEvents()
        {
            if (_componentToIndicateTyped != null)
            {
                _componentToIndicateTyped.Healed += UpdateHealthIndicator;
                _componentToIndicateTyped.TookDamage += UpdateHealthIndicator;
            }
        }

        protected override void UnsubscribeFromEvents()
        {
            if (_componentToIndicateTyped != null)
            {
                _componentToIndicateTyped.Healed -= UpdateHealthIndicator;
                _componentToIndicateTyped.TookDamage -= UpdateHealthIndicator;
            }
        }

        private void AddHealthIcons(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Instantiate(_healthIcon).transform.SetParent(transform);
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
            if (_componentToIndicateTyped != null)
            {
                RemoveHealthIcons(transform.childCount);
                AddHealthIcons(_componentToIndicateTyped.CurrentHealthPoints);
            }
        }

        private void UpdateHealthIndicator(object sender, HealthChangeEventArgs args)
        {
            if (args.IsDamage)
            {
                RemoveHealthIcons(args.HealthChange);
            }
            else
            {
                AddHealthIcons(args.HealthChange);
            }
        }
    }
}
