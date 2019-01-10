namespace Maze.Core.Interfaces
{
    public interface ILevelInfo : IRobotControl
    {
        bool IsLeftWall();
        bool IsAboveWall();
        bool IsRightWall();
        bool IsBelowWall();
    }
}