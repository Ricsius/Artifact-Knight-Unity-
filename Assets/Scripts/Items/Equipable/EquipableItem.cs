
using Assets.Scripts.Detectors;
using UnityEngine;

namespace Assets.Scripts.Items.Equipable
{
    public class EquipableItem : ItemBase
    {
        [field: SerializeField]
        public float Cooldown { get; private set; }
        public float TimeTillCooldown { get; private set; }
        public bool IsOnCooldown => TimeTillCooldown > 0;
        public GameObject Owner { get; private set; }
        protected bool _isOwnedByPlayer;

        protected override void Awake()
        {
            base.Awake();

            Type = ItemType.Equipable;
            TimeTillCooldown = 0;
        }

        protected virtual void Update()
        {
            if (IsOnCooldown)
            {
                TimeTillCooldown -= Time.deltaTime;
            }
        }

        public override void OnAddedToEquipment(GameObject newOwner)
        {
            base.OnAddedToEquipment(newOwner);

            Owner = newOwner;
            _isOwnedByPlayer = SpecialGameObjectRecognition.IsPlayer(Owner);
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
