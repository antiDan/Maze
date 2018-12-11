using Maze.Core.Interfaces;

namespace Maze.RobotPrograms.Maxim
{
    public class MeinErstesProgramm : IInfoLevelProgram
    {
        public void Program(ILevelInfo robot)
        {
            var leftIsWall = robot.IsLeftWall();

            if (leftIsWall)
            {
                robot.GoRight();
                robot.GoRight();
                robot.GoRight();
            }
            else
            {
                robot.GoLeft();
                robot.GoLeft();
                robot.GoLeft();
            }
        }
    }
}