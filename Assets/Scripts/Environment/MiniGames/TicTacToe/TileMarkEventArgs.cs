using System;

namespace Assets.Scripts.Environment.MiniGames.TicTacToe
{
    public class TileMarkEventArgs : EventArgs
    {
        public int PlayerID { get; private set; }

        public TileMarkEventArgs(int playerID)
        {
            PlayerID = playerID;
        }
    }
}
