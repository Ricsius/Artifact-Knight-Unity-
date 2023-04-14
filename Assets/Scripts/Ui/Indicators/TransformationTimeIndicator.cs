using Assets.Scripts.Systems.Health;
using UnityEngine.UI;

namespace Assets.Scripts.Ui.Indicators
{
    public class TransformationTimeIndicator : IndicatorBase<TransformationHealthSystem>
    {
        private Slider _slider;

        protected virtual void Awake() 
        {
            _slider= GetComponent<Slider>();
        }

        protected virtual void Start()
        {
        }

        protected virtual void Update()
        {
            if (_componentToIndicateTyped != null)
            {
                _slider.value = _componentToIndicateTyped.TimeTillTurningBack / _componentToIndicateTyped.TransformationDuration;
            }
        }

        protected override void ResetIndicator()
        {
            _slider.value = 1;
        }

        protected override void SubscribeToEvents()
        {
            
        }

        protected override void UnsubscribeFromEvents()
        {
            
        }
    }
}
