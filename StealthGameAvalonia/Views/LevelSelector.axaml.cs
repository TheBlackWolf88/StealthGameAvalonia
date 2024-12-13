using Avalonia.Controls;
using StealthGameAvalonia.ViewModels;

namespace StealthGameAvalonia.Views;

public partial class LevelSelector : UserControl
{
    public LevelSelector()
    {
        InitializeComponent();
        this.DataContext = new LevelSelectorViewModel();
    }
}