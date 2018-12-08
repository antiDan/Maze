using Maze.Core.Interfaces;

namespace Maze.RobotPrograms.Test
{
    public class TestProgram : IProgram
    {
        public void Program(IRobot robot)
        {
            robot.GoRight();
            robot.GoRight();
            robot.GoRight();
        }
    }
}