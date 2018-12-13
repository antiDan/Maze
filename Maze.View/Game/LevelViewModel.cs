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
            this.IsEditorActive = isEditorActive;
            this.level = level;
            this.level.RobotChanged += OnRobotChanged;

            this.ToggleWallCommand = new DelegateCommand<Wall>(this.ToggleWall, this.CanExecuteToggleWall);
            this.CreateLevelCommand = new DelegateCommand(this.CreateNewLevel);
            this.SaveLevelCommand = new DelegateCommand(this.SaveLevel);
            this.DeleteLevelCommand = new DelegateCommand(this.DeleteLevel);

            this.CanvasObjects = new ObservableCollection<CanvasObject>();
            this.ShowLevel();
        }

        public bool IsEditorActive
        {
            get
            {
                return this.isEditorActive;
            }
            set
            {
                this.isEditorActive = value;
                this.RaisePropertyChanged();
            }
        }

        public DelegateCommand<Wall> ToggleWallCommand { get; }
        public DelegateCommand CreateLevelCommand { get; }
        public DelegateCommand SaveLevelCommand { get; }
        public DelegateCommand DeleteLevelCommand { get; }
        public ObservableCollection<CanvasObject> CanvasObjects { get; }

        public double LevelWidth { get; set; }
        public double LevelHeight { get; set; }

        private void CreateNewLevel()
        {
            
        }

        private void SaveLevel()
        {
            this.level.SaveToFile();
        }

        private void DeleteLevel()
        {
            
        }

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
            this.robot = new Robot(this.level);
            this.CanvasObjects.Add(this.robot);
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