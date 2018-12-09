using Maze.Core.Interfaces;

namespace Maze.RobotPrograms.Test
{
    public class TestProgram4 : IProgram
    {
        public void Program(IRobotControl robot)
        {
            robot.GoDown();
            robot.GoDown();

            for (int i = 0; i < 5; i++)
            {
                robot.GoRight();
            }

            robot.GoUp();
            robot.GoUp();

            robot.GoRight();
        }
    }
}