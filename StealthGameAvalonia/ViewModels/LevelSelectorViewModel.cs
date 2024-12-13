using ReactiveUI;
using StealthGameAvalonia.Persistence;
using StealthGameAvalonia.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StealthGameAvalonia.ViewModels
{
    public class LevelSelectorViewModel : ViewModelBase
    {
        public ObservableCollection<KVP<string, string>> Files { get; private set; }

        private KVP<string, string>? _selectedLevelPath;

        public KVP<string, string> SelectedLevelPath
        {
            get => _selectedLevelPath!;
            set => this.RaiseAndSetIfChanged(ref _selectedLevelPath, value);
        }
        public bool IsDesktop { get; } = !OperatingSystem.IsIOS() && !OperatingSystem.IsAndroid();

        public DelegateCommand ReloadCommand { get; private set; }
        public DelegateCommand NaviCommand { get; private set; }

        public LevelSelectorViewModel()
        {
            App.Current.GameState = GameState.Normal;
            Files = new(FileLocator.GetFiles());
            ReloadCommand = new DelegateCommand((object? obj) =>
            {
                //this is quite ugly
                //there has to be a better way than just putting this in a function
                List<KVP<string, string>> actualFiles = FileLocator.GetFiles();
                foreach (KVP<string, string> f in actualFiles)
                {
                    if (!Files.Contains(f))
                    {
                        Files.Add(f);
                    }
                }
                List<KVP<string, string>> files = [.. Files];
                foreach (KVP<string, string> f in files)
                {
                    if (!actualFiles.Contains(f))
                    {
                        _ = Files.Remove(f);
                    }
                }
            });

            NaviCommand = new DelegateCommand((object? obj) =>
            {

                string filePath = (string)obj!;
                try
                {
                    App.Current.StealthGameViewModel = new(filePath);
                    MainWindowViewModel.Instance.CurrentView = new Game();
                }
                catch
                {
                    MainWindowViewModel.Instance.CurrentView = new LevelSelector();

                }

            });
        }
    }
}
