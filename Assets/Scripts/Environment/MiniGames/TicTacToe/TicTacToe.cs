using Assets.Scripts.Environment.MiniGames.TicTacToe.AI;
using Assets.Scripts.Items;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Environment.MiniGames.TicTacToe
{
    public class TicTacToe : MonoBehaviour
    {
        //ToDo: Players settable in editor, background change on game end
        [SerializeField]
        private Transform _rows;
        [SerializeField]
        private List<Sprite> _playerSprites;
        [SerializeField]
        private ItemBase _reward;
        [SerializeField]
        private float _restartDelay;
        private int _gameSize;
        private float _timeTillRestart;
        private bool _isRestarting;
        private TicTacToeTile[,] _tiles;
        private int _mainGamePlayerID;
        private int _aIPlayerID;
        private TicTacToeGameState _gameState;
        private TicTacToeAI _ai;
        private int[] _playerIDs;
        private int _startingPlayerIndex;
        private int _currentPlayerIndex;

        protected virtual void Awake()
        {
            _gameSize = _rows.childCount;
            _mainGamePlayerID = 0;
            _aIPlayerID = 1;
            _playerIDs = new int[] { _mainGamePlayerID, _aIPlayerID };
            _gameState = new TicTacToeGameState(_gameSize, _playerIDs);
            _ai = new TicTacToeAI(_aIPlayerID, _mainGamePlayerID, _gameState);
            _tiles = new TicTacToeTile[_gameSize, _gameSize];
            _startingPlayerIndex = -1;

            for (int i = 0; i < _gameSize; ++i)
            {
                Transform row = _rows.GetChild(i);

                for (int j = 0; j < _gameSize; ++j)
                {
                    TicTacToeTile tile = row.GetChild(j).GetComponent<TicTacToeTile>();

                    tile.Position = new Position(j, i);
                    tile.Sprite = null;

                    _tiles[j, i] = tile;
                }
            }

            _reward.gameObject.SetActive(false);

            ResetGame();
        }

        protected virtual void Update()
        {
            if (_timeTillRestart > 0)
            {
                _timeTillRestart -= Time.deltaTime;
            }

            if (_isRestarting && _timeTillRestart <= 0)
            {
                ResetGame();
            }
        }

        private void ResetGame()
        {
            List<Position> markedPositions = new List<Position>();

            markedPositions.AddRange(_gameState.GetMarkedPositions(_mainGamePlayerID));
            markedPositions.AddRange(_gameState.GetMarkedPositions(_aIPlayerID));

            foreach (Position position in markedPositions)
            {
                _tiles[position.X, position.Y].Sprite = null;
            }

            foreach (Position position in _gameState.UnmarkedPositions)
            {
                _tiles[position.X, position.Y].MarkRequested -= OnTileMarkRequest;
            }

            _gameState.Reset();

            _isRestarting = false;

            foreach (Position position in _gameState.UnmarkedPositions)
            {
                _tiles[position.X, position.Y].MarkRequested += OnTileMarkRequest;
            }

            _startingPlayerIndex = (_startingPlayerIndex + 1) % _playerIDs.Length;
            _currentPlayerIndex = _startingPlayerIndex;

            if (_playerIDs[_currentPlayerIndex] == _aIPlayerID)
            {
                AIAction();
            }
        }

        private void AIAction()
        {
            Position nextMove = _ai.CalculateNextMove();

            _tiles[nextMove.X, nextMove.Y].MarkRequest(_playerIDs[_currentPlayerIndex]);
        }

        private void OnTileMarkRequest(object sender, TileMarkEventArgs args)
        {
            int playerID = args.PlayerID;

            if (_playerIDs[_currentPlayerIndex] != playerID)
            {
                return;
            }

            TicTacToeTile tile = sender as TicTacToeTile;

            _gameState.PutMark(playerID, tile.Position);

            tile.Sprite = _playerSprites[playerID];
            tile.MarkRequested -= OnTileMarkRequest;

            if (_gameState.CheckWinCondition(_mainGamePlayerID))
            {
                _reward.gameObject.SetActive(true);

                Destroy(gameObject);

                return;
            } 
            else if (_gameState.CheckWinCondition(_aIPlayerID) || !_gameState.UnmarkedPositions.Any())
            {
                _timeTillRestart = _restartDelay;
                _isRestarting = true;

                return;
            }

            _currentPlayerIndex = (_currentPlayerIndex + 1) % _playerIDs.Length;

            if (_playerIDs[_currentPlayerIndex] == _aIPlayerID && _gameState.UnmarkedPositions.Any())
            {
                AIAction();
            }
        }
    }
}
