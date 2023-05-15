using Assets.Scripts.Environment.MiniGames;
using Assets.Scripts.Environment.MiniGames.TicTacToe;
using UnityEngine;

namespace Assets.Scripts.TestScenario.Requirement
{
    public class TicTacToeTurnElapsedRequirement : TestRequirementBase
    {
        [SerializeField]
        private int _requiredElapsedTurns;
        private int _elapsedTurns;
        protected virtual void Awake()
        {
            _elapsedTurns = 0;

            _subject.GetComponent<TicTacToe>().TurnElapsed += OnTurnElapsed;
        }

        public override bool Check()
        {
            return _elapsedTurns >= _requiredElapsedTurns;
        }

        private void OnTurnElapsed(object sender, TurnElapsedEventArgs args)
        {
            ++_elapsedTurns;
        }
    }
}
