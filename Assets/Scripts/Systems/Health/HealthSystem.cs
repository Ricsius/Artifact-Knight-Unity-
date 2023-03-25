
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
        public GameObject DeathEffect { get; set; }
        public event EventHandler Healed;
        public event EventHandler TookDamage;
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

            if (CurrentHealthPoints == 0)
            {
                Die();
            }

            TookDamage?.Invoke(this, new HealthChangeEventArgs(amount, true));
        }
        protected virtual void Die()
        {
            if (DisableGameObjectOnDeath)
            {
                gameObject.SetActive(false);
            }
            else
            {
                Destroy(gameObject);
            }

            if (DeathEffect != null)
            {
                Instantiate(DeathEffect, transform.position, transform.rotation);
            }

            Death?.Invoke(this, new EventArgs());
        }
    }
}
