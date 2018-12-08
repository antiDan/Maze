using Maze.Core.Objects;
using Prism.Commands;
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
            this.CreateItems();
        }

        public ObservableCollection<Line> Items { get; set; }

        private void CreateItems()
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

            this.Items = new ObservableCollection<Line>(walls.Select(w => new Line(w)));
        }
    }
}