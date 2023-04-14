
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assets.Scripts.Environment.MiniGames.TicTacToe.AI
{
    public class TicTacToeAI
    {
        private int _myID;
        private int _opponentID;
        private TicTacToeGameState _gameState;

        public TicTacToeAI(int myID, int mainGamePlayerID, TicTacToeGameState gameState)
        {
            _myID = myID;
            _opponentID = mainGamePlayerID;
            _gameState = gameState;
        }
        public Position CalculateNextMove()
        {
            //ToDo: Balance this!
            Position opponentWinningPatternCompleteingPosition = SelectRandomPosition(FindWinningPatternCompleteingPositions(_opponentID, _gameState));

            if (opponentWinningPatternCompleteingPosition != null)
            {
                //return opponentWinningPatternCompleteingPosition;
                return SelectRandomPosition(_gameState.UnmarkedPositions);
            }

            Position myWinningPatternCompleteingPosition = SelectRandomPosition(FindWinningPatternCompleteingPositions(_myID, _gameState));

            if (myWinningPatternCompleteingPosition != null)
            {
                return myWinningPatternCompleteingPosition;
            }

            ConcurrentBag<SimulationEvaluation> simulationEvaluations = new ConcurrentBag<SimulationEvaluation>();
            IEnumerable<IEnumerable<Position>> myPossibleWinningPatterns = PossibleWinningPatterns(_myID, _gameState);
            List<Position> myPossibleEmptyWinningPatternPositions = new List<Position>();

            foreach (IEnumerable<Position> pattern in myPossibleWinningPatterns)
            {
                IEnumerable<Position> unmarkedPositions = pattern.Where(p => _gameState.UnmarkedPositions.Contains(p));

                myPossibleEmptyWinningPatternPositions.AddRange(unmarkedPositions);
            }

            Parallel.ForEach(_gameState.UnmarkedPositions, unmarkedPosition =>
            {
                TicTacToeGameState simulation = _gameState.Copy();
                simulation.PutMark(_myID, unmarkedPosition);

                IEnumerable<IEnumerable<Position>> winningPatterns = simulation.WinningPatterns;
                IEnumerable<Position> myMarks = simulation.GetMarkedPositions(_myID);
                int impossibleWinningPatternsForOpponent = simulation.WinningPatterns.Count() - PossibleWinningPatterns(_opponentID, simulation).Count();

                SimulationEvaluation evaluation = new SimulationEvaluation(unmarkedPosition, impossibleWinningPatternsForOpponent);

                simulationEvaluations.Add(evaluation);
            });

            int impossibleWinningPatternCountForOpponentMax = simulationEvaluations.Max(e => e.ImpossibleWinningPatternCountForOpponent);
            IEnumerable<Position> opponentInibitingMoves = simulationEvaluations
                .Where(e => e.ImpossibleWinningPatternCountForOpponent == impossibleWinningPatternCountForOpponentMax)
                .Select(e => e.Position);

            IEnumerable<Position> winAdvancingMoves = opponentInibitingMoves.Where(p => myPossibleEmptyWinningPatternPositions.Contains(p));

            if (winAdvancingMoves.Any())
            {
                return SelectRandomPosition(winAdvancingMoves);
            }

            if (opponentInibitingMoves.Any())
            {
                return SelectRandomPosition(opponentInibitingMoves);
            }

            return SelectRandomPosition(_gameState.UnmarkedPositions);
        }

        private Position SelectRandomPosition(IEnumerable<Position> positions)
        {
            if (positions == null || !positions.Any())
            {
                return null;
            }

            Position[] arr = positions.ToArray();
            System.Random random = new System.Random();
            int randomIndex = random.Next(arr.Length);

            return arr[randomIndex];
        }

        private IEnumerable<Position> FindWinningPatternCompleteingPositions(int playerID, TicTacToeGameState gameState)
        {
            IEnumerable<IEnumerable<Position>> winningPatterns = gameState.WinningPatterns;
            IEnumerable<Position> unmarked = gameState.UnmarkedPositions;
            IEnumerable<Position> playerMarks = gameState.GetMarkedPositions(playerID);

            IEnumerable<IEnumerable<Position>> patterns = winningPatterns.Where(p => 
            p.Count(m => playerMarks.Contains(m)) == gameState.Size -1 
            && p.Count(m => unmarked.Contains(m)) == 1);

            if (!patterns.Any())
            {
                return null;
            }

            List<Position> ret = new List<Position>();

            foreach (IEnumerable<Position> pattern in patterns)
            {
                Position p = pattern.First(m => unmarked.Contains(m));

                ret.Add(p);
            }

            return ret;
        }

        private IEnumerable<IEnumerable<Position>> PossibleWinningPatterns(int playerID, TicTacToeGameState gameState)
        {
            return gameState.WinningPatterns.Where(p => p.All(pos => gameState.GetMarkedPositions(playerID).Contains(pos) || gameState.UnmarkedPositions.Contains(pos)));
        }
    }
}
