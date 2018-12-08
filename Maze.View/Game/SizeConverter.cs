namespace Maze.View.Game
{
    public static class SizeConverter
    {
        private const decimal Factor = 100;

        public static decimal Convert(decimal value)
        {
            return value * SizeConverter.Factor;
        }
    }
}