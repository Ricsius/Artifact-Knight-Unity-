using Assets.Scripts.Systems.Health;
using UnityEngine;

namespace Assets.Scripts.Timers
{
    public class DeathTimer : MonoBehaviour
    {
        [field: SerializeField]
        public float DeathTime { get; set; }
        public float TimeTillDeath { get; private set; }
        private HealthSystem _healthSystem;
        private bool _isStarted;

        protected virtual void Awake()
        {
            _healthSystem = GetComponent<HealthSystem>();
        }

        protected virtual void Update()
        {
            if (_isStarted)
            {
                if (TimeTillDeath > 0)
                {
                    TimeTillDeath -= Time.deltaTime;
                }
                else
                {
                    _healthSystem.TakeDamage(int.MaxValue);
                }
            }
        }

        public void StartTimer()
        {
            TimeTillDeath = DeathTime;
            _isStarted = true;
        }
    }
}
