
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Environment.MiniGames.TicTacToe.AI
{
    public class TicTacToeAI : MonoBehaviour
    {
        public int ID { private get; set; }
        public int OpponentID { private get; set; }
        public TicTacToe Game 
        {
            set
            {
                _game = value;
                _gameState = _game.GameState;
                _game.TurnElapsed += OnTurnElapsed;
            }
        }
        private TicTacToe _game;
        private TicTacToeGameState _gameState;
        [SerializeField]
        private float _actionDelay;

        private void OnTurnElapsed(object sender, TurnElapsedEventArgs args)
        {
            if (ID == args.NextPlayerID)
            {
                StartCoroutine(ActionWithDelay());
            }
        }

        private IEnumerator ActionWithDelay()
        {
            yield return new WaitForSeconds(_actionDelay);

            _game.MarkTile(CalculateNextMove(), gameObject);
        }

        private Position CalculateNextMove()
        {
            //ToDo: Balance this!
            Position opponentWinningPatternCompleteingPosition = SelectRandomPosition(FindWinningPatternCompleteingPositions(OpponentID, _gameState));

            if (opponentWinningPatternCompleteingPosition != null)
            {
                //return opponentWinningPatternCompleteingPosition;
                return SelectRandomPosition(_gameState.UnmarkedPositions);
            }

            Position myWinningPatternCompleteingPosition = SelectRandomPosition(FindWinningPatternCompleteingPositions(ID, _gameState));

            if (myWinningPatternCompleteingPosition != null)
            {
                return myWinningPatternCompleteingPosition;
            }

            ConcurrentBag<SimulationEvaluation> simulationEvaluations = new ConcurrentBag<SimulationEvaluation>();
            IEnumerable<IEnumerable<Position>> myPossibleWinningPatterns = PossibleWinningPatterns(ID, _gameState);
            List<Position> myPossibleEmptyWinningPatternPositions = new List<Position>();

            foreach (IEnumerable<Position> pattern in myPossibleWinningPatterns)
            {
                IEnumerable<Position> unmarkedPositions = pattern.Where(p => _gameState.UnmarkedPositions.Contains(p));

                myPossibleEmptyWinningPatternPositions.AddRange(unmarkedPositions);
            }

            Parallel.ForEach(_gameState.UnmarkedPositions, unmarkedPosition =>
            {
                TicTacToeGameState simulation = _gameState.Copy();
                simulation.PutMark(ID, unmarkedPosition);

                IEnumerable<IEnumerable<Position>> winningPatterns = simulation.WinningPatterns;
                IEnumerable<Position> myMarks = simulation.GetMarkedPositions(ID);
                int impossibleWinningPatternsForOpponent = simulation.WinningPatterns.Count() - PossibleWinningPatterns(OpponentID, simulation).Count();

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
