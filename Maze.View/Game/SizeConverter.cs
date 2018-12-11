namespace Maze.View.Game
{
    public static class SizeConverter
    {
        private const double Factor = 100;

        public static double Convert(double value)
        {
            return value * SizeConverter.Factor;
        }
    }
}