using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp_Demo.Models;
using MauiApp_Demo.Services;

namespace MauiApp_Demo.ViewModel
{
    [QueryProperty(nameof(Movie), "Movie")]
    public partial class MovieDetailsViewModel : BaseViewModel
    {
        private readonly WatchListService _watchListService;

        public MovieDetailsViewModel(WatchListService watchListService)
        {
            _watchListService = watchListService;
        }

        [ObservableProperty]
        Movie movie;

        [RelayCommand]
        private async Task AddWatchListAsync(int movieId)
        {
            await _watchListService.AddWatchList(movieId);
        }
    }
}
