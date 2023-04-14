
using UnityEngine;

namespace Assets.Scripts.Ui.Indicators.Targets
{
    public class IndicatorGroupTargetBase : MonoBehaviour
    {
        protected string _indicatorGroupObjectName;
        private IndicatorGroup _indicatorGroup;

        protected virtual void Awake()
        {
            _indicatorGroup = GameObject.Find(_indicatorGroupObjectName).GetComponent<IndicatorGroup>();
        }

        protected virtual void OnEnable()
        {
            _indicatorGroup.JoinTargets(gameObject);
        }

        protected virtual void OnDisable()
        {
            _indicatorGroup.LeaveTargets(gameObject);
        }
    }
}
