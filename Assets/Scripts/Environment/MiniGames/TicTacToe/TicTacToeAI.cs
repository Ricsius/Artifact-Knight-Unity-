using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Environment.MiniGames.TicTacToe
{
    //ToDo: Finish this!
    public class TicTacToeAI
    {
        private TicTacToeGameState _gameState;

        public TicTacToeAI(TicTacToeGameState gameState) 
        {
            _gameState = gameState;
        }
        public Vector2 CalculateNextMove()
        {
            System.Random random = new System.Random();
            Vector2[] unmarkedPositions= _gameState.UnmarkedPositions;
            int randomIndex = random.Next(unmarkedPositions.Length);

            return unmarkedPositions[randomIndex];
        }
    }
}
