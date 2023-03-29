using Assets.Scripts.Environment;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Detectors
{
    public class ProfessorDetector : DetectorBase<Professor>
    {
        private Collider2D _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }
        public override Professor Detect()
        {
            RaycastHit2D[] raycastHits = Physics2D.BoxCastAll(transform.position, _collider.bounds.size, 0, transform.right, .1f);
            RaycastHit2D hit = raycastHits.FirstOrDefault(rh => SpecialGameObjectRecognition.IsProfessor(rh.transform.gameObject));
            Professor ret = hit.transform != null 
                ? hit.transform.gameObject.GetComponent<Professor>() 
                : null;

            return ret;
        }
    }
}
