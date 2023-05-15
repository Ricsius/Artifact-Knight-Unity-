
using System;

namespace Assets.Scripts.Systems.Score.Storage
{
    public class PlayerScore
    {
        public string LevelName { get; set; }
        public string PlayerName { get; set; }
        public int Score { get; set; }
        public DateTime Date { get; set; }
    }
}
