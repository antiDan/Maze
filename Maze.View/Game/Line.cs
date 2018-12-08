using Maze.Core.Objects;

namespace Maze.View.Game
{
    public class Line
    {
        public Line(Wall wall)
        {
            this.X1 = SizeConverter.Convert(wall.Point1.X);
            this.Y1 = SizeConverter.Convert(wall.Point1.Y);
            this.X2 = SizeConverter.Convert(wall.Point2.X);
            this.Y2 = SizeConverter.Convert(wall.Point2.Y);
        }

        public int X1 { get; }
        public int Y1 { get; }
        public int X2 { get; }
        public int Y2 { get; }
    }
}