using System;
using System.Reactive;
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
            ToPurchasesCommand = ReactiveCommand.Create(ToPurchases);
            ToOrdersCommand = ReactiveCommand.Create(ToOrderss);
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
        }

        private void ToProduct() => CurrentSplitViewModel = new ProductViewModel();
        private void ToPurchases() => CurrentSplitViewModel = new PurchasesViewModel();
        private void ToOrderss() => CurrentSplitViewModel = new OrdersViewModel();
        #endregion
    }
}
