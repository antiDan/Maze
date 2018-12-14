namespace Maze.View.Game
{
    public static class SizeConverter
    {
        public static double Factor { get; set; }

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