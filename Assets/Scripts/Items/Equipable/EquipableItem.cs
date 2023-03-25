
using UnityEngine;

namespace Assets.Scripts.Items.Equipable
{
    public class EquipableItem : ItemBase
    {
        [field: SerializeField]
        public float Cooldown { get; private set; }
        public float TimeTillCooldown { get; private set; }
        public bool IsOnCooldown => TimeTillCooldown > 0;

        protected override void Awake()
        {
            base.Awake();

            Type = ItemType.Equipable;
            TimeTillCooldown = Cooldown;
        }

        protected virtual void Update()
        {
            if (IsOnCooldown)
            {
                TimeTillCooldown -= Time.deltaTime;
            }
        }

        public void Use()
        {
            if (!IsOnCooldown)
            {
                Effect();
                ResetCooldown();
            }
        }

        protected virtual void Effect()
        {
        }

        protected void ResetCooldown()
        {
            TimeTillCooldown = Cooldown;
        }
    }
}
