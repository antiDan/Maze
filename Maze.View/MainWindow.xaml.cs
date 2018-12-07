using Maze.Core.Objects;
using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Maze.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //this.CreateLevel();
        }

        private void CreateLevel()
        {
            var level = new Core.Objects.Level
            {
                Width = 3,
                Height = 1,
                Walls = new List<Wall>(),
                Exit = new Wall
                {
                    Point1 = new Core.Objects.Point
                    {
                        X = 1,
                        Y = 1
                    },
                    Point2 = new Core.Objects.Point
                    {
                        X = 1,
                        Y = 1
                    }
                }
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