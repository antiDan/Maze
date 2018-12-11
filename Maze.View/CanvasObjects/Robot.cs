﻿using Maze.Core.Objects;
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
            SizeConverter.Convert(this.level.Robot.X + 0.1),
            SizeConverter.Convert(this.level.Robot.Y + 0.1),
            0,
            0);

        public void Refresh()
        {
            this.RaisePropertyChanged(nameof(this.Margin));
        }
    }
}