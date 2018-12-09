using Maze.Core.Interfaces;

namespace Maze.RobotPrograms.Test
{
    public class TestProgram5 : IInfoLevelProgram
    {
        public void Program(ILevelInfo robot)
        {
            while (!robot.IsDownWall())
            {
                robot.GoDown();
            }

            while(!robot.IsRightWall())
            {
                robot.GoRight();
            }
        }
    }
}