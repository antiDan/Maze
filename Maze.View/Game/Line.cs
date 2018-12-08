using Maze.Core.Objects;

namespace Maze.View.Game
{
    public class Line
    {
        private const int Factor = 100;

        public Line(Wall wall)
        {
            this.X1 = this.Convert(wall.Point1.X);
            this.Y1 = this.Convert(wall.Point1.Y);
            this.X2 = this.Convert(wall.Point2.X);
            this.Y2 = this.Convert(wall.Point2.Y);
        }

        public int X1 { get; }
        public int Y1 { get; }
        public int X2 { get; }
        public int Y2 { get; }

        private int Convert(int value)
        {
            return (value + 1) * Line.Factor;
        }
    }
}