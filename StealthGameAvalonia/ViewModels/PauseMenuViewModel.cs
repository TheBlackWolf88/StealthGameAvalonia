using StealthGameAvalonia.Views;
using System;

namespace StealthGameAvalonia.ViewModels
{
    public class PauseMenuViewModel : ViewModelBase
    {
        public string PMText { get; } = App.Current.GameState switch
        {
            GameState.Normal => "Pause Menu",
            GameState.Win => "Congraaats!! You won!",
            GameState.Lose => "Oh nooo! You lost!",
            _ => throw new Exception("mi?")

        };

        public bool IsNormalState { get; } = App.Current.GameState == GameState.Normal;

        public DelegateCommand BackCommand { get; private set; }
        public DelegateCommand NewGameCommand { get; private set; }

        public PauseMenuViewModel()
        {
            BackCommand = new DelegateCommand((object? obj) =>
            {
                MainWindowViewModel.Instance.CurrentView = new Game();
            });
            NewGameCommand = new DelegateCommand((object? obj) =>
            {
                MainWindowViewModel.Instance.CurrentView = new LevelSelector();
                App.Current.StealthGameViewModel = null;
            });
        }
    }
}
