using Maze.Core.Objects;
using Maze.View.CanvasObjects;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;

namespace Maze.View.Game
{
    public class LevelViewModel : BindableBase
    {
        private Level level;

        public LevelViewModel(Level level)
        {
            this.level = level;
            this.CanvasObjects = new ObservableCollection<CanvasObject>();

            this.CalculateLevelSize();
            this.CreateWalls();
            this.CreateRobot();
        }

        public ObservableCollection<CanvasObject> CanvasObjects { get; }
        public decimal LevelWidth { get; set; }
        public decimal LevelHeight { get; set; }
        public decimal RobotX => this.level.Robot.X;
        public decimal RobotY => this.level.Robot.Y;

        private void CalculateLevelSize()
        {
            this.LevelWidth = SizeConverter.Convert(this.level.Width);
            this.LevelHeight = SizeConverter.Convert(this.level.Height);
        }

        private void CreateWalls()
        {
            var walls = this.level.Walls;
            for (var x = 0; x < this.level.Width; x++)
            {
                walls.Add(new Wall
                {
                    Point1 = new Point(x, 0),
                    Point2 = new Point(x + 1, 0)
                });

                walls.Add(new Wall
                {
                    Point1 = new Point(x, this.level.Height),
                    Point2 = new Point(x + 1, this.level.Height)
                });
            }

            for (var y = 0; y < this.level.Height; y++)
            {
                walls.Add(new Wall
                {
                    Point1 = new Point(0, y),
                    Point2 = new Point(0, y + 1)
                });

                walls.Add(new Wall
                {
                    Point1 = new Point(this.level.Width, y),
                    Point2 = new Point(this.level.Width, y + 1)
                });
            }

            walls.Remove(this.level.Exit);

            this.CanvasObjects.AddRange(walls.Select(w => new Line(w)));
        }

        private void CreateRobot()
        {
            this.CanvasObjects.Add(new Robot(this.level));
        }
    }
}