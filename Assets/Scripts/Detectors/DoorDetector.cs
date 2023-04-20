using Assets.Scripts.Environment;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Detectors
{
    public class DoorDetector : DetectorBase<Door>
    {
        private Collider2D _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }
        public override Door Detect()
        {
            RaycastHit2D[] raycastHits = Physics2D.BoxCastAll(transform.position, _collider.bounds.size, 0, transform.right, .1f);
            RaycastHit2D hit = raycastHits.FirstOrDefault(rh => SpecialGameObjectRecognition.IsDoor(rh.transform.gameObject));
            Door ret = hit.transform?.gameObject.GetComponent<Door>();

            return ret;
        }
    }
}
