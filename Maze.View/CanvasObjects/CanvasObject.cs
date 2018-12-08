using Prism.Mvvm;

namespace Maze.View.CanvasObjects
{
    public abstract class CanvasObject : BindableBase
    {
        public abstract string Type { get; }
    }
}