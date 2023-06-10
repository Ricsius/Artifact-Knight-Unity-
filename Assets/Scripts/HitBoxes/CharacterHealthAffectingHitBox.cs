using Assets.Scripts.Systems.Health;
using UnityEngine;

namespace Assets.Scripts.HitBoxes
{
    public class CharacterHealthAffectingHitBox : MonoBehaviour
    {
        [field: SerializeField]
        public int HealthEffect { get; set; }

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            HealthSystem healthSystem = collision.gameObject.GetComponent<HealthSystem>();

            HealthAffecting(healthSystem);
        }

        protected virtual void OnTriggerEnter2D(Collider2D collider)
        {
            HealthSystem healthSystem = collider.gameObject.GetComponent<HealthSystem>();

            HealthAffecting(healthSystem);
        }

        protected virtual void HealthAffecting(HealthSystem healthSystem)
        {
            if (healthSystem != null)
            {
                if (HealthEffect < 0)
                {
                    healthSystem.TakeDamage(-HealthEffect);
                }
                else
                {
                    healthSystem.Heal(HealthEffect);
                }
            }
        }
    }
}
