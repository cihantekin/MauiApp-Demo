using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp_Demo.Models;
using MauiApp_Demo.Services;

namespace MauiApp_Demo.ViewModel;

[QueryProperty(nameof(Movie), "Movie")]
[QueryProperty(nameof(Genres), "Genres")]
[QueryProperty(nameof(IsInWatchList), "IsInWatchList")]
public partial class MovieDetailsViewModel : BaseViewModel
{
    private readonly WatchListService _watchListService;
    private readonly FavoriteService _favoriteService;
    public MovieDetailsViewModel(WatchListService watchListService, FavoriteService favoriteService)
    {
        _watchListService = watchListService;
        _favoriteService = favoriteService;
    }

    [ObservableProperty]
    Movie movie;

    [ObservableProperty]
    List<string> genres;

    [ObservableProperty]
    bool isInWatchList;

    [RelayCommand]
    private async Task HandleWatchListOperationsAsync(Movie movie)
    {
        int result;

        if (IsInWatchList)
        {
            result = await _watchListService.RemoveFromWatchList(movie);
        }
        else
        {
            result = await _watchListService.AddWatchList(movie);
        }

        if (result > 0)
        {
            IsInWatchList = !IsInWatchList;
        }
    }

    internal async Task HandleFavoriteOperations(string movieNotes, string movieScore)
    {
        Favorite favorite = new()
        {
            MovieId = Movie.Id,
            MovieName = Movie.Title,
            Poster = Movie.Poster,
            MovieNotes = movieNotes,
            Score = Convert.ToInt32(movieScore)
        };

        await _favoriteService.AddFavorites(favorite);
        await Shell.Current.GoToAsync(nameof(FavoritesPage), true, new Dictionary<string, object>
            {
                {"Movie", movie},
                {"Genres", genres},
                {"IsInWatchList", isInWatchList}
            });

    }
}
