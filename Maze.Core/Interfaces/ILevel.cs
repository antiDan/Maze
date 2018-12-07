using System.Collections.Generic;

namespace Maze.Core.Interfaces
{
    public interface ILevel
    {
        int Width { get; }
        int Height { get; }
        //List<IWall> Walls { get; }
    }
}