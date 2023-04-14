
using Assets.Scripts.Effects;
using System;
using UnityEngine;

namespace Assets.Scripts.Systems.Health
{
    public class HealthSystem : MonoBehaviour
    {
        [field: SerializeField]
        public int MaxHealthPoints { get; set; }
        public int CurrentHealthPoints { get; private set; }

        [field: SerializeField]
        public bool DisableGameObjectOnDeath { get; set; }

        [field: SerializeField]
        public Effect DeathEffect { get; set; }
        public event EventHandler<HealthChangeEventArgs> Healed;
        public event EventHandler<HealthChangeEventArgs> TookDamage;
        public event EventHandler Death;


        private void Awake()
        {
            CurrentHealthPoints = MaxHealthPoints;
        }

        public void Heal(int amount)
        {
            int diff = 0;

            CurrentHealthPoints += amount;

            if (CurrentHealthPoints > MaxHealthPoints)
            {
                diff = CurrentHealthPoints - MaxHealthPoints;

                amount -= diff;
                CurrentHealthPoints = MaxHealthPoints;
            }

            Healed?.Invoke(this, new HealthChangeEventArgs(amount, false));
        }

        public void TakeDamage(int amount)
        {
            CurrentHealthPoints -= amount;

            if (CurrentHealthPoints < 0)
            {
                amount += CurrentHealthPoints;
                CurrentHealthPoints = 0;
            }

            TookDamage?.Invoke(this, new HealthChangeEventArgs(amount, true));

            if (CurrentHealthPoints == 0)
            {
                Die();
            }
        }
        protected virtual void Die()
        {
            if (DeathEffect != null)
            {
                Instantiate(DeathEffect.gameObject, transform.position, transform.rotation);
            }

            if (DisableGameObjectOnDeath)
            {
                gameObject.SetActive(false);
                Death?.Invoke(this, new EventArgs());
            }
            else
            {
                Death?.Invoke(this, new EventArgs());
                Destroy(gameObject);
            }
        }
    }
}
