using Assets.Scripts.Timers;
using UnityEngine;

namespace Assets.Scripts.Ui.Indicators
{
    public class PlayerTransformationIndicatorTarget : PlayerIndicatorTarget
    {
        private TransformationTimeIndicator _playerTransformationTimeIndicator;
        private DeathTimer _deathTimer;

        protected override void Awake()
        {
            base.Awake();

            _playerTransformationTimeIndicator = GameObject.Find("PlayerTrasformationTimerIndicator").GetComponent<TransformationTimeIndicator>();
            _deathTimer = GetComponent<DeathTimer>();

        }

        protected override void OnEnable()
        {
            base.OnEnable();

            _playerTransformationTimeIndicator.Show();
            _playerTransformationTimeIndicator.DeathTimer = _deathTimer;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (_playerTransformationTimeIndicator.DeathTimer == _deathTimer)
            {
                _playerTransformationTimeIndicator.Hide();
                _playerTransformationTimeIndicator.DeathTimer = null;
            }
        }
    }
}
