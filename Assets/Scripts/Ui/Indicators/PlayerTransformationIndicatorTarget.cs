using Assets.Scripts.Systems.Health;
using UnityEngine;

namespace Assets.Scripts.Ui.Indicators
{
    public class PlayerTransformationIndicatorTarget : PlayerIndicatorTarget
    {
        private TransformationTimeIndicator _playerTransformationTimeIndicator;
        private TransformationHealthSystem _transformationHealthSystem;

        protected override void Awake()
        {
            base.Awake();

            _playerTransformationTimeIndicator = GameObject.Find("PlayerTrasformationTimerIndicator").GetComponent<TransformationTimeIndicator>();
            _transformationHealthSystem = GetComponent<TransformationHealthSystem>();

        }

        protected override void Start()
        {
            base.Start();

            _playerTransformationTimeIndicator.Show();
            _playerTransformationTimeIndicator.TransformationHealthSystem = _transformationHealthSystem;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (_playerTransformationTimeIndicator.TransformationHealthSystem == _transformationHealthSystem)
            {
                _playerTransformationTimeIndicator.Hide();
                _playerTransformationTimeIndicator.TransformationHealthSystem = null;
            }
        }
    }
}
