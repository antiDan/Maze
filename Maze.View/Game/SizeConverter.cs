namespace Maze.View.Game
{
    public static class SizeConverter
    {
        private const double Factor = 100;

        public static double LevelToView(double value)
        {
            return value * SizeConverter.Factor;
        }

        public static double ViewToLevel(double value)
        {
            return value / SizeConverter.Factor;
        }
    }
}