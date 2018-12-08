using Maze.Core.Objects;
using Maze.View.Game;
using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Point = Maze.Core.Objects.Point;

namespace Maze.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //this.CreateLevel();

            var levelsPath = this.GetLevelsPath();
            var levelPath = Path.Combine(levelsPath, "Level_1.lvl");
            var level = Level.FromFile(levelPath);
            var levelView = this.CreateLevelView(level);
            this.Content = levelView;
        }

        private LevelView CreateLevelView(Level level)
        {
            var viewModel = new LevelViewModel(level);
            var view = new LevelView();
            view.DataContext = viewModel;
            return view;
        }

        private string GetLevelsPath()
        {
            var path = System.AppDomain.CurrentDomain.BaseDirectory;
            var directoryInfo = Directory.GetParent(path);

            while (directoryInfo.Name != "Maze")
            {
                directoryInfo = directoryInfo.Parent;
            }
            
            return Path.Combine(directoryInfo.FullName, @"Maze.Core\Levels");
        }

        private void CreateLevel()
        {
            var level = new Level
            {
                Width = 3,
                Height = 1,
                Walls = new List<Wall>(),
                Exit = new Wall
                {
                    Point1 = new Point(3, 0),
                    Point2 = new Point(3, 1)
                },
                Robot = new Point(0, 0)
            };

            var json = level.ToJson();

            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Maze Level|*.lvl";
            saveFileDialog.Title = "Save Level";
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, json);                
            }
        }
    }
}