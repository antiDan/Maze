using System.Collections.Generic;

namespace Maze.Core.Interfaces
{
    public interface ILevel
    {
        double Width { get; }
        double Height { get; }
        //List<IWall> Walls { get; }
    }
}