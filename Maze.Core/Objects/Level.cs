using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
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

        public void GoLeft()
        {
            this.Robot.X--;
            this.RaiseRobotChanged();
            this.Wait();
        }

        public void GoUp()
        {
            this.Robot.Y--;
            this.RaiseRobotChanged();
            this.Wait();
        }

        public void GoRight()
        {
            this.Robot.X++;
            this.RaiseRobotChanged();
            this.Wait();
        }

        public void GoDown()
        {
            this.Robot.Y++;
            this.RaiseRobotChanged();
            this.Wait();
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
                program.Program(this);
            });
        }

        private void Wait()
        {
            Thread.Sleep(500);
        }
    }
}