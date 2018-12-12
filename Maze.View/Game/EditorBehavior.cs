using Maze.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Maze.View.Game
{
    public class EditorBehavior
    {
        public static DependencyProperty ToggleWallCommandProperty = DependencyProperty.RegisterAttached("ToggleWall",
               typeof(ICommand),
               typeof(EditorBehavior),
               new FrameworkPropertyMetadata(null, new PropertyChangedCallback(EditorBehavior.ToggleWallChanged)));

        public static void SetToggleWall(DependencyObject target, ICommand value)
        {
            target.SetValue(EditorBehavior.ToggleWallCommandProperty, value);
        }

        public static ICommand GetToggleWall(DependencyObject target)
        {
            return (ICommand)target.GetValue(ToggleWallCommandProperty);
        }

        private static void ToggleWallChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            var canvas = target as Canvas;
            if (canvas != null)
            {
                if ((e.NewValue != null) && (e.OldValue == null))
                {
                    canvas.MouseLeftButtonDown += OnMouseLeftButtonDown;
                }
                else if ((e.NewValue == null) && (e.OldValue != null))
                {
                    canvas.MouseLeftButtonDown -= OnMouseLeftButtonDown;
                }
            }
        }

        private static void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var canvas = (Canvas)sender;
            var mousePoint = e.GetPosition(canvas);
            var wall = EditorBehavior.MousePointToWall(mousePoint);
            var command = (ICommand)canvas.GetValue(EditorBehavior.ToggleWallCommandProperty);
            command.Execute(wall);
        }

        private static Wall MousePointToWall(Point mousePoint)
        {
            var x = SizeConverter.ViewToLevel(mousePoint.X);
            var y = SizeConverter.ViewToLevel(mousePoint.Y);

            var levelPoint = new Point(x, y);

            var smallX = Math.Floor(x);
            var smallY = Math.Floor(y);

            var points = new List<Point>
            {
                new Point(smallX, smallY),
                new Point(smallX + 1, smallY),
                new Point(smallX + 1, smallY + 1),
                new Point(smallX, smallY + 1)
            };

            // Wir nehmen 2 LevelPoints, die am wenigsten vom MousePosition entfernt sind
            var pointDistances = points.Select(point => new
            {
                Point = point,
                Distance = (levelPoint - point).Length
            })
            .OrderBy(p => p.Distance)
            .ToList();

            return new Wall(pointDistances[0].Point, pointDistances[1].Point);
        }
    }
}