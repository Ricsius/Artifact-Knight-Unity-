using Assets.Scripts.Environment.MiniGames.TicTacToe.AI;
using Assets.Scripts.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Environment.MiniGames.TicTacToe
{
    public class TicTacToe : MonoBehaviour
    {
        public TicTacToeGameState GameState { get; private set; }
        public EventHandler<TurnElapsedEventArgs> TurnElapsed;
        [SerializeField]
        private Transform _rows;
        [SerializeField]
        private GameObject _player1Object;
        [SerializeField]
        private GameObject _player2Object;
        [SerializeField]
        private Sprite _player1Sprite;
        [SerializeField]
        private Sprite _player2Sprite;
        private Dictionary<GameObject, int> _playerIDs;
        private Dictionary<int, Sprite> _playerSprites;
        [SerializeField]
        private ItemBase _reward;
        [SerializeField]
        private float _restartDelay;
        private int _gameSize;
        private float _timeTillRestart;
        private bool _isRestarting;
        private TicTacToeTile[,] _tiles;
        private int _player1ID;
        private int _player2ID;
        private int _startingPlayerID;
        private int _currentPlayerID;

        protected virtual void Awake()
        {
            _gameSize = _rows.childCount;
            _player1ID = 0;
            _player2ID = 1;

            _playerIDs = new Dictionary<GameObject, int>
            {
                { _player1Object, _player1ID },
                { _player2Object, _player2ID }
            };

            _playerSprites = new Dictionary<int, Sprite>
            {
                { _player1ID, _player1Sprite },
                { _player2ID, _player2Sprite }
            };

            GameState = new TicTacToeGameState(_gameSize, _playerIDs.Values);
            _tiles = new TicTacToeTile[_gameSize, _gameSize];

            foreach (GameObject player in _playerIDs.Keys)
            {
                TicTacToeAI ai = player.GetComponent<TicTacToeAI>();

                if (ai != null)
                {
                    int id = _playerIDs[player];

                    ai.ID = id;
                    ai.OpponentID = id == _player1ID ? _player2ID : _player1ID;
                    ai.Game = this;
                }
            }

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

            _startingPlayerID = -1;

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

            markedPositions.AddRange(GameState.GetMarkedPositions(_player1ID));
            markedPositions.AddRange(GameState.GetMarkedPositions(_player2ID));

            foreach (Position position in markedPositions)
            {
                _tiles[position.X, position.Y].Sprite = null;
            }

            foreach (Position position in GameState.UnmarkedPositions)
            {
                _tiles[position.X, position.Y].MarkRequested -= OnTileMarkRequest;
            }

            GameState.Reset();

            _isRestarting = false;

            foreach (Position position in GameState.UnmarkedPositions)
            {
                _tiles[position.X, position.Y].MarkRequested += OnTileMarkRequest;
            }

            _startingPlayerID = _startingPlayerID == _player1ID ? _player2ID : _player1ID;
            _currentPlayerID = _startingPlayerID;

            TurnElapsed?.Invoke(this, new TurnElapsedEventArgs(_currentPlayerID));
        }

        public void MarkTile(Position position, GameObject player)
        {
            _tiles[position.X, position.Y].MarkRequest(player);
        }

        private void OnTileMarkRequest(object sender, TileMarkEventArgs args)
        {
            GameObject player = args.Player;

            if (_playerIDs[player] != _currentPlayerID)
            {
                return;
            }

            TicTacToeTile tile = sender as TicTacToeTile;

            GameState.PutMark(_currentPlayerID, tile.Position);

            tile.Sprite = _playerSprites[_currentPlayerID];
            tile.MarkRequested -= OnTileMarkRequest;

            if (GameState.CheckWinCondition(_player1ID))
            {
                _reward.gameObject.SetActive(true);

                Destroy(gameObject);

                return;
            } 
            else if (GameState.CheckWinCondition(_player2ID) || !GameState.UnmarkedPositions.Any())
            {
                _timeTillRestart = _restartDelay;
                _isRestarting = true;

                return;
            }

            _currentPlayerID = _currentPlayerID == _player1ID ? _player2ID : _player1ID;

            TurnElapsed?.Invoke(this, new TurnElapsedEventArgs(_currentPlayerID));
        }
    }
}
