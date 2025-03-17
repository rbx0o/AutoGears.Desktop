using System;
using System.Reactive;
using System.Threading.Tasks;
using AutoGears.Models.Entities;
using AutoGears.Models.Queries;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Styling;
using ReactiveUI;
using static AutoGears.ViewModels.MainWindowViewModel;

namespace AutoGears.ViewModels.ManagerViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Fields
        private string _role = string.Empty, _fullName = string.Empty;
        private Bitmap _icon, _signOutIcon, _themeIcon;
        private ViewModelBase _currentSplitViewModel;
        private bool _button1IsChecked = true, _button2IsChecked = false, _button3IsChecked = false;
        private Person _currentUser;
        #endregion

        #region Properties
        public string Role { get => _role; set => this.RaiseAndSetIfChanged(ref _role, value); }
        public string FullName { get => _fullName; set => this.RaiseAndSetIfChanged(ref _fullName, value); }
        public Bitmap Icon { get => _icon; set => this.RaiseAndSetIfChanged(ref _icon, value); }
        public Bitmap SignOutIcon { get => _signOutIcon; set => this.RaiseAndSetIfChanged(ref _signOutIcon, value); }
        public Bitmap ThemeIcon { get => _themeIcon; set => this.RaiseAndSetIfChanged(ref _themeIcon, value); }
        public ViewModelBase CurrentSplitViewModel { get => _currentSplitViewModel; set => this.RaiseAndSetIfChanged(ref _currentSplitViewModel, value); }
        public bool Button1IsChecked { get => _button1IsChecked; set => this.RaiseAndSetIfChanged(ref _button1IsChecked, value); }
        public bool Button2IsChecked { get => _button2IsChecked; set => this.RaiseAndSetIfChanged(ref _button2IsChecked, value); }
        public bool Button3IsChecked { get => _button3IsChecked; set => this.RaiseAndSetIfChanged(ref _button3IsChecked, value); }
        public Person CurrentUser { get => _currentUser; set => this.RaiseAndSetIfChanged(ref _currentUser, value); }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            _ = InitializeAsync();

            _icon = Application.Current?.ActualThemeVariant == ThemeVariant.Dark ?
                new Bitmap(AssetLoader.Open(new Uri($"avares://AutoGears/Assets/avalonia-logo-dark.ico"))) :
                new Bitmap(AssetLoader.Open(new Uri($"avares://AutoGears/Assets/avalonia-logo-light.ico")));

            _signOutIcon = Application.Current?.ActualThemeVariant == ThemeVariant.Dark ?
                new Bitmap(AssetLoader.Open(new Uri($"avares://AutoGears/Assets/ExitIcon/sign-out-dark.ico"))) :
                new Bitmap(AssetLoader.Open(new Uri($"avares://AutoGears/Assets/ExitIcon/sign-out-light.ico")));

            _themeIcon = Application.Current?.ActualThemeVariant == ThemeVariant.Dark ?
                new Bitmap(AssetLoader.Open(new Uri($"avares://AutoGears/Assets/ChangeThemeIcon/theme-icon-dark.ico"))) :
                new Bitmap(AssetLoader.Open(new Uri($"avares://AutoGears/Assets/ChangeThemeIcon/theme-icon-light.ico")));

            _currentSplitViewModel = new ProductViewModel();

            ToggleThemeCommand = ReactiveCommand.Create(ToggleThemeMain);
            ExitCommand = ReactiveCommand.CreateFromTask(MainWindow.Exit);

            ToProductCommand = ReactiveCommand.Create(ToProduct);
            ToPurchasesCommand = ReactiveCommand.Create(ToPurchases);
            ToOrdersCommand = ReactiveCommand.Create(ToOrderss);
        }

        private async Task InitializeAsync()
        {
            CurrentUser = await Get.CurrentUserPerson();

            Role = await Get.CurrentUserRole();

            FullName = $"{CurrentUser.LastName} {CurrentUser.FirstName}";
        }
        #endregion

        #region ReactiveCommands
        public ReactiveCommand<Unit, Unit> ToggleThemeCommand { get; }
        public ReactiveCommand<Unit, Unit> ExitCommand { get; }
        public ReactiveCommand<Unit, Unit> ToProductCommand { get; }
        public ReactiveCommand<Unit, Unit> ToPurchasesCommand { get; }
        public ReactiveCommand<Unit, Unit> ToOrdersCommand { get; }
        #endregion

        #region Methods
        private void ToggleThemeMain()
        {
            MainWindow.ToggleTheme();

            Icon = Application.Current.ActualThemeVariant == ThemeVariant.Dark ?
                new Bitmap(AssetLoader.Open(new Uri($"avares://AutoGears/Assets/avalonia-logo-dark.ico"))) :
                new Bitmap(AssetLoader.Open(new Uri($"avares://AutoGears/Assets/avalonia-logo-light.ico")));

            SignOutIcon = Application.Current?.ActualThemeVariant == ThemeVariant.Dark ?
                new Bitmap(AssetLoader.Open(new Uri($"avares://AutoGears/Assets/ExitIcon/sign-out-dark.ico"))) :
                new Bitmap(AssetLoader.Open(new Uri($"avares://AutoGears/Assets/ExitIcon/sign-out-light.ico")));

            ThemeIcon = Application.Current?.ActualThemeVariant == ThemeVariant.Dark ?
                new Bitmap(AssetLoader.Open(new Uri($"avares://AutoGears/Assets/ChangeThemeIcon/theme-icon-dark.ico"))) :
                new Bitmap(AssetLoader.Open(new Uri($"avares://AutoGears/Assets/ChangeThemeIcon/theme-icon-light.ico")));
        }

        private void ToProduct()
        {
            CurrentSplitViewModel = new ProductViewModel();
            Button1IsChecked = true;
            Button2IsChecked = false;
            Button3IsChecked = false;
        }
        private void ToPurchases()
        {
            CurrentSplitViewModel = new PurchasesViewModel();
            Button1IsChecked = false;
            Button2IsChecked = true;
            Button3IsChecked = false;
        }
        private void ToOrderss()
        {
            CurrentSplitViewModel = new OrdersViewModel();
            Button1IsChecked = false;
            Button2IsChecked = false;
            Button3IsChecked = true;
        }
        #endregion
    }
}
