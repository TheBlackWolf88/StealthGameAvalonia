using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Threading;
using MsBox.Avalonia;
using ReactiveUI;
using StealthGameAvalonia.Model;
using StealthGameAvalonia.Model.Entities;
using StealthGameAvalonia.Model.Floors;
using StealthGameAvalonia.Persistence;
using StealthGameAvalonia.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace StealthGameAvalonia.ViewModels
{
    public class StealthGameViewModel : ViewModelBase, IDisposable
    {
        private readonly Level? _level;

        public DelegateCommand? PauseCommand { get; private set; }
        public DelegateCommand? MoveCommand { get; private set; }

        public bool IsMobile { get; } = OperatingSystem.IsIOS() || OperatingSystem.IsAndroid();

        public int Dims => (int)Math.Sqrt(_level!.Board.Length);

        private (int r, int c) _player;
        public (int Row, int Col) Player
        {
            get => _player;
            set => this.RaiseAndSetIfChanged(ref _player, value);
        }
        //uniformgrid-nek ugyis mindegy, matekolas meg meg megy
        public ObservableCollection<Bitmap> Level
        {
            get; private set;
        }

        public StealthGameViewModel(string levelPath)
        {
            try
            {
                ITextFileManager tfm = FileManagerFactory.CreateTextFileManager(levelPath);
                _level = new Level(tfm);
            }
            catch (Exception ex)
            {
                MsBox.Avalonia.Base.IMsBox<MsBox.Avalonia.Enums.ButtonResult> msBox =
                    MessageBoxManager.GetMessageBoxStandard("Error", ex.Message, MsBox.Avalonia.Enums.ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
                _ = msBox.ShowAsync();
                throw new Exception("Wrong level file!");
            }
            _level.CellChanged += HandleCellChanged;
            _level.GameOver += HandleGameOver;
            Player = (_level.Player.Row, _level.Player.Col);
            PauseCommand = new DelegateCommand((object? obj) =>
            {
                _level.Pause();
                MainWindowViewModel.Instance.CurrentView = new PauseMenu();
            });

            MoveCommand = new DelegateCommand((object? obj) =>
            {
                Model.Utils.Direction dir = (Model.Utils.Direction)obj!;
                //Debug.WriteLine($"Player moved in {dir}");
                _level.MovePlayer(dir);

            });
            Level = [];
            BuildLevel();
            _level.Pause();
        }



        private void BuildLevel()
        {
            if (_level == null)
            {
                return;
            }

            foreach (Floor f in _level.Board)
            {
                Level.Add(f switch
                {
                    Wall => new Bitmap(AssetLoader.Open(new Uri(@"avares://StealthGameAvalonia/Assets/wall.png"))),
                    Exit => new Bitmap(AssetLoader.Open(new Uri(@"avares://StealthGameAvalonia/Assets/exit.png"))),
                    _ => new Bitmap(AssetLoader.Open(new Uri(@"avares://StealthGameAvalonia/Assets/floor.png")))
                });
            }
            Level[(Player.Row * Dims) + Player.Col] = new Bitmap(AssetLoader.Open(new Uri(@"avares://StealthGameAvalonia/Assets/thief.png")));
            foreach (Guard g in _level.Guards)
            {
                Level[(Dims * g.Row) + g.Col] = new Bitmap(AssetLoader.Open(new Uri(@"avares://StealthGameAvalonia/Assets/guard.png")));
            }
        }

        private void HandleGameOver(object? sender, bool e)
        {
            App.Current.GameState = e ? GameState.Win : GameState.Lose;
            Dispatcher.UIThread.Invoke(() =>
            {
                PauseCommand?.Execute(null);
            });
        }
        private void HandleCellChanged(object? sender, ((int r, int c) curr, (int r, int c) last) e)
        {
            if (sender is Player)
            {
                Level[(e.last.r * Dims) + e.last.c] = new Bitmap(AssetLoader.Open(new Uri(@"avares://StealthGameAvalonia/Assets/floor.png")));
                Level[(e.curr.r * Dims) + e.curr.c] = new Bitmap(AssetLoader.Open(new Uri(@"avares://StealthGameAvalonia/Assets/thief.png")));
            }
            else if (sender is Guard)
            {
                Level[(e.last.r * Dims) + e.last.c] = new Bitmap(AssetLoader.Open(new Uri(@"avares://StealthGameAvalonia/Assets/floor.png")));
                Level[(e.curr.r * Dims) + e.curr.c] = new Bitmap(AssetLoader.Open(new Uri(@"avares://StealthGameAvalonia/Assets/guard.png")));
            }
        }

        public void Dispose()
        {
            _level?.Dispose();
        }
    }
}
