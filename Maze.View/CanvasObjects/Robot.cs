using Maze.Core.Objects;
using System.Windows;
using SizeConverter = Maze.View.Game.SizeConverter;

namespace Maze.View.CanvasObjects
{
    public class Robot : CanvasObject
    {
        private Level level;

        public Robot(Level level)
        {
            this.level = level;
        }

        public override string Type => nameof(Robot);

        public Thickness Margin => new Thickness(
            SizeConverter.LevelToView(this.level.Robot.X + 0.1),
            SizeConverter.LevelToView(this.level.Robot.Y + 0.1),
            0,
            0);

        public double Size => SizeConverter.Factor * 0.8;

        public void Refresh()
        {
            this.RaisePropertyChanged(nameof(this.Margin));
        }
    }
}