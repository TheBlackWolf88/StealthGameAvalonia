using Avalonia.Controls;
using ReactiveUI;
using StealthGameAvalonia.Views;

namespace StealthGameAvalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private static MainWindowViewModel? _instance;
        private bool _isItGameYet = false;
        public bool IsItGameYet
        {
            get => _isItGameYet;
            set => this.RaiseAndSetIfChanged(ref _isItGameYet, value);
        }
        private UserControl _currentView;
        public UserControl CurrentView
        {
            get => _currentView;
            set
            {
                this.RaiseAndSetIfChanged(ref _currentView, value);
                IsItGameYet = CurrentView is Game;
            }
        }

        public static MainWindowViewModel Instance
        {
            get
            {
                _instance ??= new MainWindowViewModel();
                return _instance;
            }
        }

        private MainWindowViewModel()
        {
            _currentView = new MainMenu();
        }
    }
}
