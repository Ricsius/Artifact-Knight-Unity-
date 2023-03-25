
using UnityEngine;

namespace Assets.Scripts.Ui.Indicators
{
    public abstract class IndicatorBase : MonoBehaviour
    {
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
