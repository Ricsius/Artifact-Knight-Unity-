
using System;
using UnityEngine;

namespace Assets.Scripts.UI.Indicators
{
    public abstract class IndicatorBase : MonoBehaviour
    {
        public abstract Component ComponentToIndicate { get; set; }
        public abstract Type ComponentToIndicateType { get; }
    }
    public abstract class IndicatorBase<T> : IndicatorBase where T : Component
    {
        public override Type ComponentToIndicateType => typeof(T);
        public override Component ComponentToIndicate
        {
            get
            {
                return _componentToIndicateTyped;
            }
            set
            {
                UnsubscribeFromEvents();

                _componentToIndicateTyped = value as T;

                SubscribeToEvents();
                ResetIndicator();
            }
        }
        protected T _componentToIndicateTyped;
        protected virtual void OnEnable()
        {
            SubscribeToEvents();

            ResetIndicator();
        }

        protected virtual void OnDisable()
        {
            UnsubscribeFromEvents();
        }
        protected abstract void ResetIndicator();
        protected abstract void SubscribeToEvents();
        protected abstract void UnsubscribeFromEvents();
    }
}
