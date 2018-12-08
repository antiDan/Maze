namespace Maze.Core.Objects
{
    public class Point
    {
        public decimal X { get; set; }
        public decimal Y { get; set; }

        public Point(decimal x, decimal y)
        {
            this.X = x;
            this.Y = y;
        }

        public override bool Equals(object obj)
        {
            var point = obj as Point;
            return point != null &&
                   X == point.X &&
                   Y == point.Y;
        }

        public override int GetHashCode()
        {
            var hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return $"{this.X}/{this.Y}";
        }
    }
}