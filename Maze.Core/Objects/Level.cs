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
            throw new System.NotImplementedException();
        }

        public void GoUp()
        {
            throw new System.NotImplementedException();
        }

        public void GoRight()
        {
            throw new System.NotImplementedException();
        }

        public void GoDown()
        {
            throw new System.NotImplementedException();
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