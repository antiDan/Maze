namespace Maze.Core.Interfaces
{
    interface IGame
    {
        ILevel Level { get; }
        IProgramBase Program { get; }
        void Start();
    }
}