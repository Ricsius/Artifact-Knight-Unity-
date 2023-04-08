using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Environment.MiniGames.TicTacToe
{
    public class TicTacToeGameState
    {
        public Vector2[] UnmarkedPositions => _unmarkedPositions.ToArray();
        private int _size;
        IEnumerable<int> _playerIDs;
        private HashSet<Vector2> _unmarkedPositions;
        private List<HashSet<Vector2>> _winningPositionGroups;
        private Dictionary<int, HashSet<Vector2>> _markedPositions;

        public TicTacToeGameState(int size, IEnumerable<int> playerIDs) 
        {
            _size = size;
            _playerIDs = playerIDs;

            _unmarkedPositions = new HashSet<Vector2>();
            _markedPositions = new Dictionary<int, HashSet<Vector2>>();
            _winningPositionGroups = new List<HashSet<Vector2>>();

            for (int i = 0; i < _size; ++i)
            {
                for (int j = 0; j < _size; ++j)
                {
                    _unmarkedPositions.Add(new Vector2(j, i));
                }
            }

            for (int i = 0; i < _size; ++i)
            {
                IEnumerable<Vector2> row = _unmarkedPositions.Where(p => p.x == i).ToArray();
                IEnumerable<Vector2> column = _unmarkedPositions.Where(p => p.y == i).ToArray();

                _winningPositionGroups.Add(new HashSet<Vector2>(row));
                _winningPositionGroups.Add(new HashSet<Vector2>(column));
            }

            IEnumerable<Vector2> diagonal1 = _unmarkedPositions.Where(p => p.x == p.y).ToArray();
            IEnumerable<Vector2> diagonal2 = _unmarkedPositions.Where(p => p.x + p.y == _size - 1).ToArray();

            _winningPositionGroups.Add(new HashSet<Vector2>(diagonal1));
            _winningPositionGroups.Add(new HashSet<Vector2>(diagonal2));

            foreach (int playerID in _playerIDs)
            {
                _markedPositions[playerID] = new HashSet<Vector2>();
            }
        }

        public void PutMark(int playerID, Vector2 position)
        {
            _unmarkedPositions.Remove(position);
            _markedPositions[playerID].Add(position);
        }

        public bool CheckWinCondition(int playerID)
        {
            return _winningPositionGroups.Any(g => g.All(p => _markedPositions[playerID].Contains(p)));
        }
    }
}
