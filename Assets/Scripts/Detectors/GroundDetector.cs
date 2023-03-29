
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Detectors
{
    public class GroundDetector : DetectorBase<IEnumerable<GameObject>>
    {
        private Collider2D _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }

        public override IEnumerable<GameObject> Detect()
        {
            Vector2 down = -1 * transform.up;
            RaycastHit2D[] raycastHits = Physics2D.BoxCastAll(_collider.bounds.center, _collider.bounds.size, 0f, down, .02f);

            return raycastHits
                .Where(rh => rh.transform.gameObject != gameObject && !rh.collider.isTrigger)
                .Select(rh => rh.transform.gameObject)
                .ToArray();
        }
    }
}
