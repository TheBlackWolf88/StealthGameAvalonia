using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System.Diagnostics;

namespace StealthGameAvalonia.Views;

public partial class Game : UserControl
{
    public Game()
    {
        InitializeComponent();
        this.DataContext = App.Current.StealthGameViewModel;

       
    }

}