using System.Collections.Generic;

namespace Maze.Core.Interfaces
{
    public interface ILevel
    {
        decimal Width { get; }
        decimal Height { get; }
        //List<IWall> Walls { get; }
    }
}