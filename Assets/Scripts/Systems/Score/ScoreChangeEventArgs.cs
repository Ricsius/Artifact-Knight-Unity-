using System;

namespace Assets.Scripts.Systems.Score
{
    public class ScoreChangeEventArgs : EventArgs
    {
        public int NewScore { get; }
        public ScoreChangeEventArgs(int newScore) 
        {
            NewScore = newScore;
        }
    }
}
