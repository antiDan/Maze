namespace Maze.Core.Interfaces
{
    public interface IProgramBase
    {

    }

    public interface IProgram : IProgramBase
    {
        void Program(IRobotControl robot);
    }

    public interface IInfoLevelProgram : IProgramBase
    {
        void Program(ILevelInfo robot);
    }
}