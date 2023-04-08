using Assets.Scripts.Detectors;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Projectiles
{
    public class HomingProjectile : Projectile
    {
        public Predicate<GameObject> TargetSelector { get; set; }
        private Rigidbody2D _rigidbody;
        private Transform _target;
        [SerializeField]
        private float _homingRadius;
        [SerializeField]
        private float _homingSpeed;

        protected override void Awake()
        {
            base.Awake();

            _rigidbody= GetComponent<Rigidbody2D>();
            TargetSelector = o => SpecialGameObjectRecognition.IsPlayer(o);
        }

        protected virtual void FixedUpdate() 
        {
            if (_target == null && TargetSelector != null)
            {
                _target = Physics2D.CircleCastAll(transform.position, _homingRadius, Vector2.zero)
                    .Where(h => TargetSelector(h.transform.gameObject))
                    .FirstOrDefault()
                    .transform;
            }

            if (_target != null)
            {
                Vector2 direction = (_target.position - transform.position).normalized;
                float rotation = Vector3.Cross(direction, transform.right).z;

                _rigidbody.angularVelocity = -rotation * _homingSpeed * 100;
                _rigidbody.velocity= _homingSpeed * transform.right;
                

            }
        }
    }
}
