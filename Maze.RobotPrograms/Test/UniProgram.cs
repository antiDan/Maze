using Maze.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace Maze.RobotPrograms.Test
{
    public class UniProgram : IInfoLevelProgram
    {
        public void Program(ILevelInfo robot)
        {
            var size = 16;
            var startPoint = size / 2;

            var cells = new Cell[size, size];
            var stack = new Stack<Cell>();

            var cell = new Cell(robot, cells, 0, 0);
            stack.Push(cell);

            while (cell.NewMoves > 0 || stack.Count > 0)
            {
                if (cell.NewMoves > 0)
                {
                    cell = cell.MoveInNewCell();
                    stack.Push(cell);
                }
                else
                {                    
                    this.GoBack(stack.Pop(), stack.Peek(), robot);
                    cell = stack.Peek();
                }
            }
        }

        private void GoBack(Cell current, Cell previous, ILevelInfo robot)
        {
            if (previous.X < current.X)
            {
                robot.GoLeft();
            }
            else if (previous.Y < current.Y)
            {
                robot.GoUp();
            }
            else if (previous.X > current.X)
            {
                robot.GoRight();
            }
            else if (previous.Y > current.Y)
            {
                robot.GoDown();
            }
            else
            {
                throw new Exception("Ich kann nicht zurück");
            }
        }
    }

    public class Cell
    {
        private ILevelInfo robot;
        private Cell[,] cells;

        public Cell(ILevelInfo robot, Cell[,] cells, int x, int y)
        {
            this.robot = robot;
            this.cells = cells;
            this.X = x;
            this.Y = y;

            this.cells[this.X, this.Y] = this;
            this.CheckWalls();
        }

        public int X { get; }
        public int Y { get; }

        public bool? IsLeftWall { get; set; }
        public bool? IsAboveWall { get; set; }
        public bool? IsRightWall { get; set; }
        public bool? IsBelowWall { get; set; }

        public Cell Left => this.cells[this.X - 1, this.Y];
        public Cell Top => this.cells[this.X, this.Y - 1];
        public Cell Right => this.cells[this.X + 1, this.Y];
        public Cell Bottom => this.cells[this.X, this.Y + 1];

        public int NewMoves
        {
            get
            {
                var count = 0;

                if (this.IsLeftNewMove) count++;
                if (this.IsAboveNewMove) count++;
                if (this.IsRightNewMove) count++;
                if (this.IsBelowNewMove) count++;

                return count;
            }
        }

        public bool IsLeftNewMove => this.IsLeftWall == false && this.Left == null;
        public bool IsAboveNewMove => this.IsAboveWall == false && this.Top == null;
        public bool IsRightNewMove => this.IsRightWall == false && this.Right == null;
        public bool IsBelowNewMove => this.IsBelowWall == false && this.Bottom == null;

        public void CheckWalls()
        {
            this.IsLeftWall = robot.IsLeftWall();
            this.IsAboveWall = robot.IsAboveWall();
            this.IsRightWall = robot.IsRightWall();
            this.IsBelowWall = robot.IsBelowWall();
        }

        public Cell MoveInNewCell()
        {
            if (this.IsLeftNewMove)
            {
                this.robot.GoLeft();
                return new Cell(this.robot, this.cells, this.X - 1, this.Y);
            }

            if (this.IsAboveNewMove)
            {
                this.robot.GoUp();
                return new Cell(this.robot, this.cells, this.X, this.Y - 1);
            }

            if (this.IsRightNewMove)
            {
                this.robot.GoRight();
                return new Cell(this.robot, this.cells, this.X + 1, this.Y);
            }

            if (this.IsBelowNewMove)
            {
                this.robot.GoDown();
                return new Cell(this.robot, this.cells, this.X, this.Y + 1);
            }

            throw new Exception("Es gibt keine neue Zelle");
        }

        public override string ToString()
        {
            return $"{this.X}/{this.Y}";
        }
    }
}