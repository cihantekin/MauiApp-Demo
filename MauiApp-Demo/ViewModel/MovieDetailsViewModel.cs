using CommunityToolkit.Mvvm.ComponentModel;
using MauiApp_Demo.Models;

namespace MauiApp_Demo.ViewModel
{
    [QueryProperty(nameof(Movie), "Movie")]
    public partial class MovieDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        Movie movie;
    }
}
