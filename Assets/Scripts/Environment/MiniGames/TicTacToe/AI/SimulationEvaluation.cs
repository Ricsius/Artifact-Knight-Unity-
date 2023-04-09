
namespace Assets.Scripts.Environment.MiniGames.TicTacToe.AI
{
    public class SimulationEvaluation
    {
        public Position Position { get; }
        public int ImpossibleWinningPatternCountForOpponent { get; }

        public SimulationEvaluation(Position position, int impossibleWinningPatternsForOpponent)
        {
            Position = position;
            ImpossibleWinningPatternCountForOpponent = impossibleWinningPatternsForOpponent;
        }
    }
}

