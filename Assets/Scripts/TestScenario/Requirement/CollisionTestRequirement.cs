
using UnityEngine;

namespace Assets.Scripts.TestScenario.Requirement
{
    public class CollisionTestRequirement : TestRequirementBase
    {
        private bool _subjectCollided;
        public override bool Check()
        {
            return _subjectCollided;
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.gameObject == _subject)
            {
                _subjectCollided = true;
            }
        }

        protected virtual void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject == _subject)
            {
                _subjectCollided = true;
            }
        }
    }
}
