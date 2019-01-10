using Maze.View.Game;
using System.Windows;
using System.Windows.Controls;

namespace Maze.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Content = this.CreateGameView();
        }

        private UserControl CreateGameView()
        {
            var viewModel = new GameViewModel();
            var view = new GameView();
            view.DataContext = viewModel;
            return view;
        }
    }
}