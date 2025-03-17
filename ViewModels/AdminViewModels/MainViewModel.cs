using System;
using System.Reactive;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Styling;
using ReactiveUI;
using static AutoGears.ViewModels.MainWindowViewModel;

namespace AutoGears.ViewModels.AdminViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Fields
        private string _role = string.Empty;
        private Bitmap _icon;
        private ViewModelBase _currentSplitViewModel;
        #endregion

        #region Properties
        public string Role { get => _role; set => this.RaiseAndSetIfChanged(ref _role, value); }
        public Bitmap Icon { get => _icon; set => this.RaiseAndSetIfChanged(ref _icon, value); }
        public ViewModelBase CurrentSplitViewModel { get => _currentSplitViewModel; set => this.RaiseAndSetIfChanged(ref _currentSplitViewModel, value); }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            _icon = Application.Current?.ActualThemeVariant == ThemeVariant.Dark ?
                new Bitmap(AssetLoader.Open(new Uri($"avares://AutoGears/Assets/avalonia-logo-dark.ico"))) :
                new Bitmap(AssetLoader.Open(new Uri($"avares://AutoGears/Assets/avalonia-logo-light.ico")));

            _currentSplitViewModel = new ProductViewModel();

            ToggleThemeCommand = ReactiveCommand.Create(ToggleThemeMain);
            ExitCommand = ReactiveCommand.CreateFromTask(MainWindow.Exit);

            ToProductCommand = ReactiveCommand.Create(ToProduct);
            ToSuppliersCommand = ReactiveCommand.Create(ToSuppliers);
            ToUsersCommand = ReactiveCommand.Create(ToUsers);
        }
        #endregion

        #region ReactiveCommands
        public ReactiveCommand<Unit, Unit> ToggleThemeCommand { get; }
        public ReactiveCommand<Unit, Unit> ExitCommand { get; }
        public ReactiveCommand<Unit, Unit> ToProductCommand { get; }
        public ReactiveCommand<Unit, Unit> ToSuppliersCommand { get; }
        public ReactiveCommand<Unit, Unit> ToUsersCommand { get; }
        #endregion

        #region Methods
        private void ToggleThemeMain()
        {
            MainWindow.ToggleTheme();

            Icon = Application.Current.ActualThemeVariant == ThemeVariant.Dark ?
                new Bitmap(AssetLoader.Open(new Uri($"avares://AutoGears/Assets/avalonia-logo-dark.ico"))) :
                new Bitmap(AssetLoader.Open(new Uri($"avares://AutoGears/Assets/avalonia-logo-light.ico")));
        }        

        private void ToProduct() => CurrentSplitViewModel = new ProductViewModel();
        private void ToSuppliers() => CurrentSplitViewModel = new SuppliersViewModel();
        private void ToUsers() => CurrentSplitViewModel = new UsersViewModel();
        #endregion
    }
}
