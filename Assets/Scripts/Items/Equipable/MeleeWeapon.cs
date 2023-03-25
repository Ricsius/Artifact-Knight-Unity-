using Assets.Scripts.HitBoxes;
using UnityEngine;

namespace Assets.Scripts.Items.Equipable
{
    public class MeleeWeapon : EquipableItem
    {
        private CharacterHealthAffectingHitBox _hitBox;
        private int _healthEfect;

        protected override void Awake()
        {
            base.Awake();

            _hitBox = GetComponent<CharacterHealthAffectingHitBox>();
            _healthEfect = _hitBox.HealthEffect;
            _hitBox.HealthEffect = 0;
        }

        public override void OnAddedToEquipment(GameObject newOwner)
        {
            _hitBox.HealthEffect = _healthEfect;
        }
    }
}
