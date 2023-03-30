
using Assets.Scripts.Systems.Score;
using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Ui.Indicators
{
    public class ScoreIndicator : IndicatorBase
    {
        public ScoreSystem ScoreSystem
        {
            get
            {
                return _scoreSystem;
            }
            set
            {
                UnsubscribeFromEvents();

                _scoreSystem = value;

                SubscribeToEvents();
                ResetIndicator();
            }
        }
        [field: SerializeField]
        private TextMeshProUGUI _scoreText;
        private ScoreSystem _scoreSystem;

        protected override void SubscribeToEvents()
        {
            if (_scoreSystem != null)
            {
                _scoreSystem.ScoreChanged += OnScoreChange;
            }
        }

        protected override void UnsubscribeFromEvents()
        {
            if (_scoreSystem != null)
            {
                _scoreSystem.ScoreChanged -= OnScoreChange;
            }
        }

        protected override void ResetIndicator()
        {
            if (_scoreSystem != null)
            {
                _scoreText.text = _scoreSystem.ScoreSum.ToString();
            }
        }

        private void OnScoreChange(object sender, EventArgs args)
        {
            ScoreChangeEventArgs scoreArgs = args as ScoreChangeEventArgs;

            _scoreText.text = scoreArgs.NewScore.ToString();
        }
    }
}
