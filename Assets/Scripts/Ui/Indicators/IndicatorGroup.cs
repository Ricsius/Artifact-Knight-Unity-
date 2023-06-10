
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.UI.Indicators
{
    public class IndicatorGroup : MonoBehaviour
    {
        private IEnumerable<IndicatorBase> _indicators;
        private LinkedList<GameObject> _targets;
        private GameObject _currentTarget;

        protected virtual void Awake()
        {
            _indicators = GetComponentsInChildren<IndicatorBase>();
            _targets = new LinkedList<GameObject>();

            foreach (IndicatorBase indicator in _indicators) 
            {
                indicator.gameObject.SetActive(false);
            }
        }

        public void JoinTargets(GameObject gameObject)
        {
            _targets.AddLast(gameObject);

            if (_targets.Count == 1)
            {
                _currentTarget = gameObject;

                IndicateGameObject(_currentTarget);
            }
        }

        public void LeaveTargets(GameObject gameObject)
        {
            _targets.Remove(gameObject);

            GameObject first = _targets.FirstOrDefault();

            if (first != _currentTarget)
            {
                _currentTarget = first;

                IndicateGameObject(_currentTarget);
            }
        }

        private void IndicateGameObject(GameObject gameObject)
        {
            foreach (IndicatorBase indicator in _indicators)
            {
                Component component = gameObject?.GetComponent(indicator.ComponentToIndicateType);

                indicator.ComponentToIndicate = component;
                indicator.gameObject.SetActive(component != null);
            }
        }
    }
}
