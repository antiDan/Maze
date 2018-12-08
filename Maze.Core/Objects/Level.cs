using System;
using System.Collections.Generic;
using System.IO;
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
        }

        public void GoUp()
        {
            this.Robot.Y--;
            this.RaiseRobotChanged();
        }

        public void GoRight()
        {
            this.Robot.X++;
            this.RaiseRobotChanged();
        }

        public void GoDown()
        {
            this.Robot.Y++;
            this.RaiseRobotChanged();
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

        #endregion Save/Load File
    }
}