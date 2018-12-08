using Maze.Core.Interfaces;

namespace Maze.Core.Objects
{
    public class Wall : IWall
    {
        public Point Point1 { get; set; }
        public Point Point2 { get; set; }

        public override bool Equals(object obj)
        {
            var wall = obj as Wall;
            return wall != null &&
                   this.Point1.Equals(wall.Point1) &&
                   this.Point2.Equals(wall.Point2);
        }

        public override int GetHashCode()
        {
            var hashCode = 363529913;
            hashCode = hashCode * -1521134295 + Point1.GetHashCode();
            hashCode = hashCode * -1521134295 + Point2.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return $"{this.Point1.ToString()} => {this.Point2.ToString()}";
        }
    }
}