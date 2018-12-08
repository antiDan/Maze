using Maze.Core.Objects;
using Maze.View.Game;

namespace Maze.View.CanvasObjects
{
    public class Line : CanvasObject
    {
        public Line(Wall wall)
        {
            this.X1 = SizeConverter.Convert(wall.Point1.X);
            this.Y1 = SizeConverter.Convert(wall.Point1.Y);
            this.X2 = SizeConverter.Convert(wall.Point2.X);
            this.Y2 = SizeConverter.Convert(wall.Point2.Y);
        }

        public override string Type => nameof(Line);

        public decimal X1 { get; }
        public decimal Y1 { get; }
        public decimal X2 { get; }
        public decimal Y2 { get; }
    }
}