using Assets.Scripts.Timers;
using UnityEngine.UI;

namespace Assets.Scripts.Ui.Indicators
{
    public class TransformationTimeIndicator : IndicatorBase
    {
        public DeathTimer DeathTimer 
        { 
            set 
            { 
                _deathTimer = value;

                ResetIndicator();
            } 
        }
        private DeathTimer _deathTimer;
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
            if (_deathTimer != null)
            {
                _slider.value = _deathTimer.TimeTillDeath / _deathTimer.DeathTime;
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
