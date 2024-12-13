using Avalonia.Controls;
using StealthGameAvalonia.ViewModels;

namespace StealthGameAvalonia.Views;

public partial class PauseMenu : UserControl
{
    public PauseMenu()
    {
        InitializeComponent();
        this.DataContext = new PauseMenuViewModel();
    }
}