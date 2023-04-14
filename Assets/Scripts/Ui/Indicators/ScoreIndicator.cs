
using Assets.Scripts.Systems.Score;
using TMPro;

namespace Assets.Scripts.Ui.Indicators
{
    public class ScoreIndicator : IndicatorBase<ScoreSystem>
    {
        private TextMeshProUGUI _scoreText;

        protected virtual void Awake()
        {
            _scoreText = GetComponentInChildren<TextMeshProUGUI>();
        }

        protected override void SubscribeToEvents()
        {
            if (_componentToIndicateTyped != null)
            {
                _componentToIndicateTyped.ScoreChanged += OnScoreChange;
            }
        }

        protected override void UnsubscribeFromEvents()
        {
            if (_componentToIndicateTyped != null)
            {
                _componentToIndicateTyped.ScoreChanged -= OnScoreChange;
            }
        }

        protected override void ResetIndicator()
        {
            if (_componentToIndicateTyped != null)
            {
                _scoreText.text = _componentToIndicateTyped.ScoreSum.ToString();
            }
        }

        private void OnScoreChange(object sender, ScoreChangeEventArgs args)
        {
            _scoreText.text = args.NewScore.ToString();
        }
    }
}
