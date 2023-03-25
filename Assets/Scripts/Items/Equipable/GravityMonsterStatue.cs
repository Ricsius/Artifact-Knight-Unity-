
using UnityEngine;

namespace Assets.Scripts.Items.Equipable
{
    public class GravityMonsterStatue : EquipableItem
    {
        private Transform _ownerTransform;
        private Rigidbody2D _ownerRigidbody;

        public override void OnAddedToEquipment(GameObject newOwner)
        {
            _ownerTransform= newOwner.transform;
            _ownerRigidbody = newOwner.GetComponent<Rigidbody2D>();
        }

        protected override void Effect()
        {
            _ownerTransform.Rotate(_ownerTransform.right * 180);
            _ownerRigidbody.gravityScale *= -1;
        }
    }
}
