using Assets.Scripts.Environment;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Detectors
{
    public class ChestDetector : DetectorBase<Chest>
    {
        private Collider2D _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }
        public override Chest Detect()
        {
            RaycastHit2D[] raycastHits = Physics2D.BoxCastAll(transform.position, _collider.bounds.size, 0, transform.right, .1f);
            RaycastHit2D hit = raycastHits.FirstOrDefault(rh => SpecialGameObjectRecognition.IsChest(rh.transform.gameObject));
            Chest ret = hit.transform?.gameObject.GetComponent<Chest>();

            return ret;
        }
    }
}
