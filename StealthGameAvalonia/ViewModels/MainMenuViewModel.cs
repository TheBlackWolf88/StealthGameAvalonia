using StealthGameAvalonia.Views;

namespace StealthGameAvalonia.ViewModels
{
    public class MainMenuViewModel : ViewModelBase
    {
        public DelegateCommand NaviCommand { get; private set; }

        public MainMenuViewModel()
        {
            NaviCommand = new DelegateCommand((object? obj) =>
            {
                MainWindowViewModel.Instance.CurrentView = new LevelSelector();
            });
        }
    }
}
