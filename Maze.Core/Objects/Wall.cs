using Maze.Core.Interfaces;

namespace Maze.Core.Objects
{
    public class Wall : IWall
    {
        public Point Point1 { get; set; }
        public Point Point2 { get; set; }
    }
}