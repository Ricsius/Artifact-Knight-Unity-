
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Detectors
{
    public class LadderDetector : DetectorBase<IEnumerable<GameObject>>
    {
        private Collider2D _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }
        public override IEnumerable<GameObject> Detect()
        {
            RaycastHit2D[] raycastHits = Physics2D.BoxCastAll(transform.position, _collider.bounds.size, 0, Vector2.zero);

            return raycastHits
                .Where(rh => SpecialGameObjectRecognition.IsLadder(rh.transform.gameObject))
                .Select(rh => rh.transform.gameObject)
                .ToArray();
        }
    }
}
