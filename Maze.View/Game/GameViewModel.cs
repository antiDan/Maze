using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Maze.Core.Objects;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Reflection;
using Maze.Core.Interfaces;

namespace Maze.View.Game
{
    public class GameViewModel : BindableBase
    {
        private string selectedLevel;
        private string selectedProgram;
        private LevelView levelView;

        public GameViewModel()
        {
            this.StartCommand = new DelegateCommand(this.Start, this.CanStart);
            this.RefreshCommand = new DelegateCommand(this.Refresh);

            this.Programs = new ObservableCollection<string>();
            this.Levels = new ObservableCollection<string>();

            this.Refresh();
        }

        public ObservableCollection<string> Programs { get; }
        public ObservableCollection<string> Levels { get; }

        public string SelectedLevel
        {
            get
            {
                return this.selectedLevel;
            }
            set
            {
                this.selectedLevel = value;
                this.RaisePropertyChanged();
                this.StartCommand.RaiseCanExecuteChanged();

                this.CreateLevel();
            }
        }

        public string SelectedProgram
        {
            get
            {
                return this.selectedProgram;
            }
            set
            {
                this.selectedProgram = value;
                this.RaisePropertyChanged();
                this.StartCommand.RaiseCanExecuteChanged();
            }
        }

        public LevelView LevelView
        {
            get
            {
                return this.levelView;
            }
            set
            {
                this.levelView = value;
                this.RaisePropertyChanged();
            }
        }

        public DelegateCommand StartCommand { get; }
        public DelegateCommand RefreshCommand { get; }

        private bool CanStart()
        {
            return this.SelectedLevel != null && this.SelectedProgram != null;
        }

        private void Start()
        {
            var program = this.LoadProgram(this.SelectedProgram);
            var level = this.CreateLevel();
            level.Run(program);
        }

        private Level CreateLevel()
        {
            if (string.IsNullOrEmpty(this.SelectedLevel))
            {
                this.LevelView = null;
                return null;
            }

            var level = Level.FromFile(this.SelectedLevel);

            var viewModel = new LevelViewModel(level);
            var view = new LevelView();
            view.DataContext = viewModel;

            this.LevelView = view;

            return level;
        }

        private IProgram LoadProgram(string name)
        {
            var dllPath = this.GetProgramDllPath();
            var assembly = Assembly.LoadFile(dllPath);

            var interfaceType = typeof(IProgram);
            var programType = assembly.GetTypes().Single(type => interfaceType.IsAssignableFrom(type) && type.Name == name);

            return (IProgram)Activator.CreateInstance(programType);
        }

        private void Refresh()
        {
            this.RefreshLevels();
            this.RefreshPrograms();
        }

        private void RefreshLevels()
        {
            var directory = Level.GetLevelsPath();
            var files = Directory.GetFiles(directory);

            this.Levels.Clear();
            this.Levels.AddRange(files);
            this.RaisePropertyChanged(nameof(this.Levels));
        }

        private void RefreshPrograms()
        {
            var dllPath = this.GetProgramDllPath();
            var assembly = Assembly.LoadFile(dllPath);

            var interfaceType = typeof(IProgram);
            var programTypes = assembly.GetTypes().Where(type => interfaceType.IsAssignableFrom(type)).ToList();

            this.Programs.Clear();
            this.Programs.AddRange(programTypes.Select(t => t.Name));
            this.RaisePropertyChanged(nameof(this.Programs));
        }

        private string GetProgramDllPath()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var directoryInfo = Directory.GetParent(path);

            while (directoryInfo.Name != "Maze")
            {
                directoryInfo = directoryInfo.Parent;
            }

            return Path.Combine(directoryInfo.FullName, @"Maze.RobotPrograms\bin\Debug\Maze.RobotPrograms.dll");
        }
    }
}