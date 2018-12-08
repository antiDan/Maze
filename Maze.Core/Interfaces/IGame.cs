namespace Maze.Core.Interfaces
{
    interface IGame
    {
        ILevel Level { get; }
        IProgram Program { get; }
        void Start();
    }
}