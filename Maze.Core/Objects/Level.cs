using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Maze.Core.Interfaces;
using Newtonsoft.Json;

namespace Maze.Core.Objects
{
    public class Level : ILevel, IRobotControl
    {
        public Level()
        {

        }

        #region ILevel

        public decimal Width { get; set; }

        public decimal Height { get; set; }

        public List<Wall> Walls { get; set; }

        public Wall Exit { get; set; }

        public Point Robot { get; set; }

        #endregion ILevel

        #region IRobotControl

        private bool IsWall(Point point1, Point point2)
        {
            return this.Walls.Contains(new Wall(point1, point2));
        }

        public bool IsLeftWall()
        {
            return this.IsWall(
                new Point(this.Robot.X, this.Robot.Y),
                new Point(this.Robot.X, this.Robot.Y + 1));
        }

        public bool IsUpWall()
        {
            return this.IsWall(
                new Point(this.Robot.X, this.Robot.Y),
                new Point(this.Robot.X + 1, this.Robot.Y));
        }

        public bool IsRightWall()
        {
            return this.IsWall(
                new Point(this.Robot.X + 1, this.Robot.Y),
                new Point(this.Robot.X + 1, this.Robot.Y + 1));
        }

        public bool IsDownWall()
        {
            return this.IsWall(
                new Point(this.Robot.X, this.Robot.Y + 1),
                new Point(this.Robot.X + 1, this.Robot.Y + 1));
        }

        public void GoLeft()
        {
            this.Robot.X--;
            this.RaiseRobotChanged();
            if (this.IsRightWall())
            {
                this.Fail();
            }
            this.Wait();
        }

        public void GoUp()
        {
            this.Robot.Y--;
            this.RaiseRobotChanged();
            if (this.IsDownWall())
            {
                this.Fail();
            }
            this.Wait();
        }

        public void GoRight()
        {
            this.Robot.X++;
            this.RaiseRobotChanged();
            if (this.IsLeftWall())
            {
                this.Fail();
            }
            this.Wait();
        }

        public void GoDown()
        {
            this.Robot.Y++;
            this.RaiseRobotChanged();
            if (this.IsUpWall())
            {
                this.Fail();
            }
            this.Wait();
        }

        public void Fail()
        {
            throw new Exception("FEHLER !!!");
        }

        public event Action RobotChanged;

        private void RaiseRobotChanged()
        {
            this.RobotChanged?.Invoke();
        }

        #endregion IRobotControl

        #region Save/Load File

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static Level FromJson(string json)
        {
            return JsonConvert.DeserializeObject<Level>(json);
        }

        public static Level FromFile(string path)
        {
            var json = File.ReadAllText(path);
            return Level.FromJson(json);
        }

        public static string GetLevelsPath()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var directoryInfo = Directory.GetParent(path);

            while (directoryInfo.Name != "Maze")
            {
                directoryInfo = directoryInfo.Parent;
            }

            return Path.Combine(directoryInfo.FullName, @"Maze.Core\Levels");
        }

        #endregion Save/Load File

        public void Run(IProgram program)
        {
            Task.Factory.StartNew(() =>
            {
                this.Wait();
                try
                {
                    program.Program(this);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);                                       
                }
            });
        }

        private void Wait()
        {
            Thread.Sleep(500);
        }
    }
}