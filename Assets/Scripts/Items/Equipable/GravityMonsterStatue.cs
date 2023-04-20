
using UnityEngine;

namespace Assets.Scripts.Items.Equipable
{
    public class GravityMonsterStatue : EquipableItem
    {
        private Transform _ownerTransform;
        private Rigidbody2D _ownerRigidbody;

        public override void OnAddedToEquipment(GameObject newOwner)
        {
            base.OnAddedToEquipment(newOwner);

            _ownerTransform = newOwner.transform;
            _ownerRigidbody = newOwner.GetComponent<Rigidbody2D>();
        }

        public override void OnRemovedFromEquipment()
        {
            base.OnRemovedFromEquipment();

            _ownerTransform = null;
            _ownerRigidbody = null;
        }

        protected override void Effect()
        {
            _ownerTransform.Rotate(_ownerTransform.right * 180);
            _ownerRigidbody.gravityScale *= -1;
        }
    }
}
