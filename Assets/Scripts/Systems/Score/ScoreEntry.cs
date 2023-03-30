
namespace Assets.Scripts.Systems.Score
{
    public class ScoreEntry
    {
        public string Name { get; private set; }
        public int Score { get; private set; }

        public ScoreEntry(string name, int score)
        {
            Name = Clean(name);
            Score = score;
        }

        private string Clean(string name)
        {
            return name.Replace("(Clone)", string.Empty);
        }

        public override string ToString() 
        {
            return $"{Name} {Score}";
        }
    }
}
