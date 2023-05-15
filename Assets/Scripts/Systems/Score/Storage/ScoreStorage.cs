using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Assets.Scripts.Systems.Score.Storage
{
    public static class ScoreStorage
    {
        private const string _sourcePath = "./scores.json";

        public static void AddScore(PlayerScore scoreEntry)
        {
            List<PlayerScore> scores = GetScores().ToList();

            scores.Add(scoreEntry);

            using (StreamWriter writer = new StreamWriter(_sourcePath))
            {
                string json = JsonSerializer.Serialize(scores);

                writer.WriteLine(json);
            }
        }

        public static IEnumerable<PlayerScore> GetScores()
        {
            try
            {
                using (StreamReader reader = new StreamReader(_sourcePath))
                {
                    string json = reader.ReadToEnd();

                    return JsonSerializer.Deserialize<List<PlayerScore>>(json);
                }
            }
            catch 
            {
                return Enumerable.Empty<PlayerScore>();
            }
        }
    }
}
