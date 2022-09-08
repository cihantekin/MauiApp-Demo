using MauiApp_Demo.ViewModel;

namespace MauiApp_Demo;

public partial class MainPage : ContentPage
{
    public MainPage(MoviesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
