using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AutoGears.Models.Queries;
using AutoGears.Services;
using AutoGears.ViewModels.SessionAuthorization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform;
using Avalonia.Styling;
using ReactiveUI;

namespace AutoGears.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Fields
        ViewModelBase _currentViewModel;
        static MainWindowViewModel _mainWindow;
        private ThemeVariant _currentTheme;
        private WindowIcon _currentIcon;
        private string _role;
        #endregion

        #region Properties
        public ViewModelBase CurrentViewModel { get => _currentViewModel; set => this.RaiseAndSetIfChanged(ref _currentViewModel, value); }
        public static MainWindowViewModel MainWindow { get => _mainWindow; set => _mainWindow = value; }
        public ThemeVariant CurrentTheme { get => _currentTheme; set => this.RaiseAndSetIfChanged(ref _currentTheme, value); }
        public WindowIcon CurrentIcon { get => _currentIcon; set => this.RaiseAndSetIfChanged(ref _currentIcon, value); }
        public string Role { get => _role; set => this.RaiseAndSetIfChanged(ref _role, value); }
        #endregion

        #region Constructors
        public MainWindowViewModel()
        {
            MainWindow = this;
            _ = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            _currentTheme = Application.Current.ActualThemeVariant;
            _currentIcon = CurrentTheme == ThemeVariant.Dark ?
                new WindowIcon(AssetLoader.Open(new Uri($"avares://AutoGears/Assets/avalonia-logo-dark.ico"))) :
                new WindowIcon(AssetLoader.Open(new Uri($"avares://AutoGears/Assets/avalonia-logo-light.ico")));

            await AuthorizedUserSession.UserInstance.SessionIsSaved();

            if (AuthorizedUserSession.UserInstance.UserLoadedState)
            {
                Role = await Get.CurrentUserRole();
                Debug.WriteLine($"Role: {Role}");
                CurrentViewModel = Role == "Администратор" ? new AdminViewModels.MainViewModel() : new ManagerViewModels.MainViewModel();
                return;
            }
            else
            {
                CurrentViewModel = new AuthViewModel();
                return;
            }
        }
        #endregion

        #region Methods
        public void NavigateToAuth() => CurrentViewModel = new AuthViewModel();
        public void NavigateToAdminMain() => CurrentViewModel = new AdminViewModels.MainViewModel();
        public void NavigateToManagerMain() => CurrentViewModel = new ManagerViewModels.MainViewModel();

        public void ToggleTheme()
        {
            CurrentTheme = CurrentTheme == ThemeVariant.Dark ? ThemeVariant.Light : ThemeVariant.Dark;
            Application.Current.RequestedThemeVariant = CurrentTheme;

            CurrentIcon = CurrentTheme == ThemeVariant.Dark ?
                new WindowIcon(AssetLoader.Open(new Uri($"avares://AutoGears/Assets/avalonia-logo-dark.ico"))) :
                new WindowIcon(AssetLoader.Open(new Uri($"avares://AutoGears/Assets/avalonia-logo-light.ico")));
        }

        public async Task Exit()
        {
            try
            {
                await SupabaseService.Instance.Supabase?.Auth.SignOut();
                AuthorizedUserSession.UserInstance.DeleteData();
                MainWindow.NavigateToAuth();
                Debug.WriteLine($"Exit Success");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exit Error: {ex.Message}");
            }
        }
        #endregion
    }
}
