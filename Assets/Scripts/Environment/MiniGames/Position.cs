
namespace Assets.Scripts.Environment.MiniGames
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Position other = obj as Position;

            if (other == null)
            {
                return false;
            }

            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            return X ^ Y;
        }

        public override string ToString()
        {
            return $"X: {X} Y: {Y}";
        }
    }
}
