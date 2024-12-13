using Avalonia.Controls;
using StealthGameAvalonia.ViewModels;

namespace StealthGameAvalonia.Views
{
    public partial class MainMenu : UserControl
    {
        public MainMenu()
        {
            InitializeComponent();
            this.DataContext = new MainMenuViewModel();

        }
    }
}