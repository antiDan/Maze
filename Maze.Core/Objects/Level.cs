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
    public class Level : ILevel, ILevelInfo
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
            this.CheckWin();
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
            this.CheckWin();
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
            this.CheckWin();
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
            this.CheckWin();
            this.Wait();
        }

        private void Fail()
        {
            throw new Exception("Der Roboter kann nicht durch die Wand :(");
        }

        private void CheckWin()
        {
            if (this.IsWin())
            {
                throw new Exception("Gewonnen! :)");
            }
        }

        private bool IsWin()
        {
            return
                this.Robot.X < 0 ||
                this.Robot.Y < 0 ||
                this.Robot.X >= this.Width ||
                this.Robot.Y >= this.Height;
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

        public void Run(IProgramBase program)
        {
            Task.Factory.StartNew(() =>
            {
                this.Wait();
                try
                {
                    if (program is IProgram)
                    {
                        (program as IProgram).Program(this);
                    }
                    else if(program is IInfoLevelProgram)
                    {
                        (program as IInfoLevelProgram).Program(this);
                    }
                    throw new Exception("Das Program ist beendet, der Roboter ist noch im Labyrinth :(");
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