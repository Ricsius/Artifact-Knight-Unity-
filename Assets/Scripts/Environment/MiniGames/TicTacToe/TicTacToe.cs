
using Assets.Scripts.Items;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//ToDo: Finish this!
namespace Assets.Scripts.Environment.MiniGames.TicTacToe
{
    public class TicTacToe : MonoBehaviour
    {
        [SerializeField]
        private List<Sprite> _playerSprites;
        [SerializeField]
        private ItemBase _reward;
        private TicTacToeTile[,] _tiles;
        private int _mainGamePlayerID;
        private int _aIPlayerID;
        private TicTacToeGameState _gameState;
        private TicTacToeAI _ai;
        private int[] _playerIDs;
        private int _currePlayerIndex;

        protected virtual void Awake()
        {
            int gameSize = transform.childCount;

            _tiles = new TicTacToeTile[gameSize, gameSize];
            _mainGamePlayerID = 0; 
            _aIPlayerID = 1;
            _playerIDs = new int[] { _mainGamePlayerID, _aIPlayerID };
            _currePlayerIndex = 0;
            _gameState = new TicTacToeGameState(gameSize, _playerIDs);
            _ai = new TicTacToeAI(_gameState);

            for (int i = 0; i < gameSize; ++i)
            {
                Transform row = transform.GetChild(i);

                for (int j = 0; j < gameSize; ++j)
                {
                    TicTacToeTile tile = row.GetChild(j).GetComponent<TicTacToeTile>();

                    tile.Position = new Vector2(j, i);
                    tile.Sprite = null;
                    tile.MarkRequested += OnTileMarkRequest;

                    _tiles[j, i] = tile;
                }
            }

            _reward.gameObject.SetActive(false);
        }

        private void OnTileMarkRequest(object sender, TileMarkEventArgs args)
        {
            int playerID = args.PlayerID;

            if (_playerIDs[_currePlayerIndex] != playerID)
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
            } 
            else if (_gameState.CheckWinCondition(_aIPlayerID) || !_gameState.UnmarkedPositions.Any())
            {
                Awake();
                return;
            }

            _currePlayerIndex = (_currePlayerIndex + 1) % _playerIDs.Length;

            if (_currePlayerIndex == _aIPlayerID && _gameState.UnmarkedPositions.Any())
            {
                Vector2 nextMove = _ai.CalculateNextMove();

                _tiles[(int)nextMove.x, (int)nextMove.y].MarkRequest(_aIPlayerID);
            }
        }
    }
}
