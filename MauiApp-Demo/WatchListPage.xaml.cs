using MauiApp_Demo.ViewModel;

namespace MauiApp_Demo;

public partial class WatchListPage : ContentPage
{
    public WatchListPage(WatchListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
