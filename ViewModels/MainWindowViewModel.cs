using System;
using AutoGears.Services;
using System.Reactive;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform;
using Avalonia.Styling;
using ReactiveUI;
using AutoGears.ViewModels.SessionAuthorization;
using System.Threading.Tasks;

namespace AutoGears.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Fields
        ViewModelBase _currentViewModel;
        static MainWindowViewModel _navigate;
        private ThemeVariant _currentTheme;
        private WindowIcon _currentIcon;
        #endregion

        #region Properties
        public ViewModelBase CurrentViewModel { get => _currentViewModel; set => this.RaiseAndSetIfChanged(ref _currentViewModel, value); }
        public static MainWindowViewModel Navigate { get => _navigate; set => _navigate = value; }
        public ThemeVariant CurrentTheme { get => _currentTheme; set => this.RaiseAndSetIfChanged(ref _currentTheme, value); }
        public WindowIcon CurrentIcon { get => _currentIcon; set => this.RaiseAndSetIfChanged(ref _currentIcon, value); }        
        #endregion

        #region Constructors
        public MainWindowViewModel()
        {
            Navigate = this;
            _ = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            _currentTheme = Application.Current.ActualThemeVariant;
            _currentIcon = CurrentTheme == ThemeVariant.Dark ?
                new WindowIcon(AssetLoader.Open(new Uri($"avares://AutoGears/Assets/avalonia-logo-dark.ico"))) :
                new WindowIcon(AssetLoader.Open(new Uri($"avares://AutoGears/Assets/avalonia-logo-light.ico")));

            await AuthorizedUserSession.UserInstance.SessionIsSaved();
            CurrentViewModel = AuthorizedUserSession.UserInstance.UserLoadedState ? new MainViewModel() : new AuthViewModel();            
        }
        #endregion

        #region Methods
        public void ToAuth() => CurrentViewModel = new AuthViewModel();
        public void ToMain() => CurrentViewModel = new MainViewModel();

        public void ToggleTheme()
        {
            CurrentTheme = CurrentTheme == ThemeVariant.Dark ? ThemeVariant.Light : ThemeVariant.Dark;
            Application.Current.RequestedThemeVariant = CurrentTheme;

            CurrentIcon = CurrentTheme == ThemeVariant.Dark ?
                new WindowIcon(AssetLoader.Open(new Uri($"avares://AutoGears/Assets/avalonia-logo-dark.ico"))) :
                new WindowIcon(AssetLoader.Open(new Uri($"avares://AutoGears/Assets/avalonia-logo-light.ico")));
        }
        #endregion
    }
}
