using AutoGears.ViewModels.ManagerViewModels;
using Avalonia.Controls;

namespace AutoGears.Views.ManagerViews;

public partial class OrdersView : UserControl
{
    public OrdersView()
    {
        InitializeComponent();
        DataContext = new OrdersViewModel();
    }
}