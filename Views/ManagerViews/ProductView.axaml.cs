using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AutoGears.Views.ManagerViews;

public partial class ProductView : UserControl
{
    public ProductView()
    {
        InitializeComponent();
    }

    private void NumericUpDown_LostFocus(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
    }
}