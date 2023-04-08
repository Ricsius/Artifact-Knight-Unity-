using Assets.Scripts.Systems.Health;
using UnityEngine.UI;

namespace Assets.Scripts.Ui.Indicators
{
    public class TransformationTimeIndicator : IndicatorBase
    {
        public TransformationHealthSystem TransformationHealthSystem 
        {
            get 
            {
                return _transformationHealthSystem;
            }
            set 
            {
                _transformationHealthSystem = value;

                ResetIndicator();
            } 
        }
        private TransformationHealthSystem _transformationHealthSystem;
        private Slider _slider;

        protected virtual void Awake() 
        {
            _slider= GetComponent<Slider>();
        }

        protected virtual void Start()
        {
            Hide();
        }

        protected virtual void Update()
        {
            if (_transformationHealthSystem != null)
            {
                _slider.value = _transformationHealthSystem.TimeTillTurningBack / _transformationHealthSystem.TransformationDuration;
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

        public void Show()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        public void Hide()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
