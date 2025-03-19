using AutoGears.ViewModels.ManagerViewModels;
using Avalonia.Controls;

namespace AutoGears.Views.ManagerViews;

public partial class PurchasesView : UserControl
{
    public PurchasesView()
    {
        InitializeComponent();
        DataContext = new PurchasesViewModel();
    }
}