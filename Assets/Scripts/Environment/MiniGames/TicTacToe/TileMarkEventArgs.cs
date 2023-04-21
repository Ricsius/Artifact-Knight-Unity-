using System;
using UnityEngine;

namespace Assets.Scripts.Environment.MiniGames.TicTacToe
{
    public class TileMarkEventArgs : EventArgs
    {
        public GameObject Player { get; private set; }

        public TileMarkEventArgs(GameObject player)
        {
            Player = player;
        }
    }
}
