using Maze.Core.Objects;
using Maze.View.Game;
using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Maze.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //this.CreateLevel();

            this.Content = this.CreateGameView();
        }

        private UserControl CreateGameView()
        {
            var viewModel = new GameViewModel();
            var view = new GameView();
            view.DataContext = viewModel;
            return view;
        }

        private void CreateLevel()
        {
            var level = new Level
            {
                Width = 3,
                Height = 1,
                Walls = new List<Wall>(),
                Exit = new Wall
                (
                    new Point(3, 0),
                    new Point(3, 1)
                ),
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