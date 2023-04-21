using System;

namespace Assets.Scripts.Environment.MiniGames
{
    public class TurnElapsedEventArgs : EventArgs
    {
        public int NextPlayerID { get; private set; }
        public TurnElapsedEventArgs(int nextPlayerID) 
        {
            NextPlayerID = nextPlayerID;
        }
    }
}
