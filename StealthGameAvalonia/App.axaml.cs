using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using StealthGameAvalonia.ViewModels;
using StealthGameAvalonia.Views;

namespace StealthGameAvalonia;

public partial class App : Application
{

    public static new App Current => (App)Application.Current!;
    public string LevelPath { get; set; } = "";
    public StealthGameViewModel? StealthGameViewModel { get; set; }

    public GameState GameState { get; set; } = GameState.Normal;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = MainWindowViewModel.Instance
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = MainWindowViewModel.Instance
            };
            if (Application.Current?.TryGetFeature<IActivatableLifetime>() is { } activatableLifetime) {
                activatableLifetime.Deactivated += (s, e) =>
                {
                    if (e.Kind == ActivationKind.Background && StealthGameViewModel is not null) {
                        StealthGameViewModel.PauseCommand!.Execute(null);
                    }
                };
            }
        }

        base.OnFrameworkInitializationCompleted();
    }

}