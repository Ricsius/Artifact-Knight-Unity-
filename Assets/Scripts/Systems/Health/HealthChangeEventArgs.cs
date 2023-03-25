using System;

namespace Assets.Scripts.Systems.Health
{
    public class HealthChangeEventArgs : EventArgs
    {
        public int HealthChange { get; }
        public bool IsDamage { get; }
        public HealthChangeEventArgs(int healthChange, bool isDamage)
        {
            HealthChange = healthChange;
            IsDamage = isDamage;
        }
    }
}
