using Maze.Core.Interfaces;

namespace Maze.RobotPrograms.Test
{
    public class TestProgram1 : IProgram
    {
        public void Program(IRobotControl robot)
        {
            robot.GoRight();
            robot.GoRight();
            robot.GoRight();
        }
    }
}