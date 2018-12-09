namespace Maze.Core.Interfaces
{
    public interface ILevelInfo : IRobotControl
    {
        bool IsLeftWall();
        bool IsUpWall();
        bool IsRightWall();
        bool IsDownWall();
    }
}