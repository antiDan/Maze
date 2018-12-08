namespace Maze.View.Game
{
    public static class SizeConverter
    {
        private const int Factor = 100;

        public static int Convert(int value)
        {
            return value * SizeConverter.Factor;
        }
    }
}