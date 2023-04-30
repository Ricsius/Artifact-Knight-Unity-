
using Assets.Scripts.Controllers.ControllerStates.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Controllers.ControllerStates.BehaviourStates
{
    public abstract class ControllerBehaviourStateBase
    {
        public ControllerBehaviourStateType Type { get; }
        public ControllerBehaviourStateManager Manager 
        { 
            set 
            {
                _manager = value;

                Init();
            } 
        }
        public ControllerMovementStateManager MovementStateManager { protected get; set; }
        protected ControllerBehaviourStateManager _manager;
        protected Transform _transform;
        protected Collider2D _collider;

        public ControllerBehaviourStateBase(ControllerBehaviourStateType type)
        {
            Type = type;
        }

        public virtual void OnSelect()
        {
        }
        public virtual void OnDeselect()
        {
        }
        public virtual void Behaviour()
        {
        }

        protected IEnumerable<GameObject> DetectGameObjectsFront()
        {
            RaycastHit2D[] raycastHits = Physics2D.BoxCastAll(_transform.position, _collider.bounds.size, 0, _transform.right, _manager.SightRange);

            IEnumerable<GameObject> ret = raycastHits
                .Where(rh => rh.transform != _transform)
                .Select(rh => rh.transform.gameObject)
                .ToArray();

            return ret;
        }

        protected IEnumerable<GameObject> DetectGameObjectsBehind()
        {
            RaycastHit2D[] raycastHits = Physics2D.BoxCastAll(_transform.position, _collider.bounds.size, 0, -1 * _transform.right, _manager.SightRange);

            IEnumerable<GameObject> ret = raycastHits
                .Where(rh => rh.transform != _transform)
                .Select(rh => rh.transform.gameObject)
                .ToArray();

            return ret;
        }

        protected IEnumerable<GameObject> DetectGameObjectsInCircle()
        {
            RaycastHit2D[] raycastHits = Physics2D.CircleCastAll(_transform.position, _manager.SightRange, Vector2.zero);

            IEnumerable<GameObject> ret = raycastHits
                .Where(rh => rh.transform != _transform)
                .Select(rh => rh.transform.gameObject)
                .ToArray();

            return ret;
        }

        protected virtual void Init()
        {
            _transform = _manager.Owner.transform;
            _collider = _manager.Owner.GetComponent<Collider2D>();
        }
    }
}
