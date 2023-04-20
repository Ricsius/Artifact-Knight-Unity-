using System.Linq;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace Assets.Scripts.Environment.MiniGames.TicTacToe
{
    public class TicTacToeGameState
    {
        public IEnumerable<Position> UnmarkedPositions => _unmarkedPositions.ToArray();
        public IEnumerable<IEnumerable<Position>> WinningPatterns => _winningPatterns.Select(g => g.ToArray()).ToArray();
        public int Size { get; }
        IEnumerable<int> _playerIDs;
        private HashSet<Position> _unmarkedPositions;
        private HashSet<HashSet<Position>> _winningPatterns;
        private Dictionary<int, HashSet<Position>> _markedPositions;

        public TicTacToeGameState(int size, IEnumerable<int> playerIDs) 
        {
            Size = size;
            _playerIDs = playerIDs;

            _unmarkedPositions = new HashSet<Position>();
            _markedPositions = new Dictionary<int, HashSet<Position>>();
            _winningPatterns = new HashSet<HashSet<Position>>();

            for (int i = 0; i < Size; ++i)
            {
                for (int j = 0; j < Size; ++j)
                {
                    _unmarkedPositions.Add(new Position(j, i));
                }
            }

            for (int i = 0; i < Size; ++i)
            {
                IEnumerable<Position> row = _unmarkedPositions.Where(p => p.X == i).ToArray();
                IEnumerable<Position> column = _unmarkedPositions.Where(p => p.Y == i).ToArray();

                _winningPatterns.Add(new HashSet<Position>(row));
                _winningPatterns.Add(new HashSet<Position>(column));
            }

            IEnumerable<Position> diagonal1 = _unmarkedPositions.Where(p => p.X == p.Y).ToArray();
            IEnumerable<Position> diagonal2 = _unmarkedPositions.Where(p => p.X + p.Y == Size - 1).ToArray();

            _winningPatterns.Add(new HashSet<Position>(diagonal1));
            _winningPatterns.Add(new HashSet<Position>(diagonal2));

            foreach (int playerID in _playerIDs)
            {
                _markedPositions[playerID] = new HashSet<Position>();
            }
        }

        public TicTacToeGameState Copy()
        {
            TicTacToeGameState ret = new TicTacToeGameState(Size, _playerIDs);

            ret._unmarkedPositions = new HashSet<Position>(_unmarkedPositions);

            foreach (int id in _playerIDs)
            {
                ret._markedPositions[id] = new HashSet<Position>(_markedPositions[id]);
            }

            return ret;
        }

        public void Reset()
        {
            foreach (int playerID in _playerIDs)
            {
                HashSet<Position> markedPositions = _markedPositions[playerID];

                _unmarkedPositions.AddRange(markedPositions);
                markedPositions.Clear();
            }
        }

        public void PutMark(int playerID, Position position)
        {
            _unmarkedPositions.Remove(position);
            _markedPositions[playerID].Add(position);
        }

        public Position[] GetMarkedPositions(int playerID)
        {
            return _markedPositions[playerID].ToArray();
        }

        public bool CheckWinCondition(int playerID)
        {
            return _winningPatterns.Any(g => g.All(p => _markedPositions[playerID].Contains(p)));
        }
    }
}
