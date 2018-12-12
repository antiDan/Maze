using Maze.Core.Objects;
using Maze.View.CanvasObjects;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;

namespace Maze.View.Game
{
    public class LevelViewModel : BindableBase
    {
        private Level level;
        private Robot robot;
        private bool isEditorActive;

        public LevelViewModel(Level level, bool isEditorActive)
        {
            this.isEditorActive = isEditorActive;
            this.level = level;
            this.level.RobotChanged += OnRobotChanged;
            this.ToggleWallCommand = new DelegateCommand<Wall>(this.ToggleWall, this.CanExecuteToggleWall);
            this.CanvasObjects = new ObservableCollection<CanvasObject>();
            this.ShowLevel();
        }

        public DelegateCommand<Wall> ToggleWallCommand { get; }
        public ObservableCollection<CanvasObject> CanvasObjects { get; }
        public double LevelWidth { get; set; }
        public double LevelHeight { get; set; }

        private void OnRobotChanged()
        {
            this.robot.Refresh();
        }

        private void ShowLevel()
        {
            // Size
            this.LevelWidth = SizeConverter.LevelToView(this.level.Width);
            this.LevelHeight = SizeConverter.LevelToView(this.level.Height);

            // Clear
            this.CanvasObjects.Clear();

            // Walls
            this.CanvasObjects.AddRange(this.level.Walls.Select(w => new Line(w)));

            // Robot
            this.CanvasObjects.Add(new Robot(this.level));
        }
               
        private bool CanExecuteToggleWall(Wall wall)
        {
            return this.isEditorActive;
        }

        private void ToggleWall(Wall wall)
        {
            if (this.level.Walls.Contains(wall))
            {
                this.level.Walls.Remove(wall);
            }
            else
            {
                this.level.Walls.Add(wall);
            }
            this.ShowLevel();
        }
    }
}