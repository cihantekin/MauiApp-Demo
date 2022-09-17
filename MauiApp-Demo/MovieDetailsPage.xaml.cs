using MauiApp_Demo.ViewModel;

namespace MauiApp_Demo;

public partial class MovieDetailsPage : ContentPage
{
    public MovieDetailsPage(MovieDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
