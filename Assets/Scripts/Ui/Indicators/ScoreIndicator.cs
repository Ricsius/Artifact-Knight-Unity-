using Assets.Scripts.Systems.Score;
using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Ui.Indicators
{
    public class ScoreIndicator : IndicatorBase
    {
        [field: SerializeField]
        public ScoreSystem _scoreSystem { get; private set; }
        private TextMeshProUGUI _scoreText;
        
        private void Awake()
        {
            _scoreText = transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
            
        }

        protected override void SubscribeToEvents()
        {
            _scoreSystem.ScoreChanged += OnScoreChange;
        }

        protected override void UnsubscribeFromEvents()
        {
            _scoreSystem.ScoreChanged -= OnScoreChange;
        }

        protected override void ResetIndicator()
        {
            _scoreText.text = _scoreSystem.Score.ToString();
        }

        private void OnScoreChange(object sender, EventArgs args)
        {
            ScoreChangeEventArgs scoreArgs = args as ScoreChangeEventArgs;

            _scoreText.text = scoreArgs.NewScore.ToString();
        }
    }
}
