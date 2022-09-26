using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp_Demo.Models;
using MauiApp_Demo.Services;

namespace MauiApp_Demo.ViewModel
{
    [QueryProperty(nameof(Movie), "Movie")]
    [QueryProperty(nameof(Genres), "Genres")]
    [QueryProperty(nameof(IsInWatchList), "IsInWatchList")]
    public partial class MovieDetailsViewModel : BaseViewModel
    {
        private readonly WatchListService _watchListService;

        public MovieDetailsViewModel(WatchListService watchListService)
        {
            _watchListService = watchListService;
        }

        [ObservableProperty]
        Movie movie;

        [ObservableProperty]
        List<string> genres;

        [ObservableProperty]
        bool isInWatchList;

        [RelayCommand]
        private async Task AddWatchListAsync(Movie movie)
        {
            await _watchListService.AddWatchList(movie);
        }
    }
}
