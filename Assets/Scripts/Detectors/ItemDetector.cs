
using Assets.Scripts.Items;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Detectors
{
    public class ItemDetector : DetectorBase<ItemBase>
    {
        private Collider2D _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }
        public override ItemBase Detect()
        {
            RaycastHit2D[] raycastHits = Physics2D.BoxCastAll(transform.position, _collider.bounds.size, 0, transform.right, .1f);
            RaycastHit2D hit = raycastHits.FirstOrDefault(rh => SpecialGameObjectRecognition.IsItem(rh.transform.gameObject));
            ItemBase ret = hit.transform != null
                ? hit.transform.gameObject.GetComponent<ItemBase>()
                : null;

            return ret;
        }
    }
}
