using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp_Demo.Models;
using MauiApp_Demo.Services;
using MvvmHelpers;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MauiApp_Demo.ViewModel
{
    public partial class MoviesViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Movie> Movies { get; } = new();
        private readonly MovieService _movieService;
        private readonly WatchListService _watchListService;

        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        bool isLoading;

        public MoviesViewModel(MovieService movieService, WatchListService watchListService)
        {
            Title = "Movies";
            this._movieService = movieService;
            _watchListService = watchListService;
            _ = GetMoviesAsync();
        }

        [RelayCommand]
        async Task GetMoviesAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                Movies.ReplaceRange(await _movieService.GetMoviesAsync());
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get movies: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        async Task GetSearchedMoviesAsync(string text)
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                Movies.ReplaceRange(await _movieService.GetMoviesAsync(text));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get movies: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        async Task LoadMoreDataAsync()
        {
            if (IsLoading) return;

            try
            {
                if (Movies?.Count > 0)
                {
                    IsLoading = true;
                    Movies.AddRange(await _movieService.GetMoviesAsync("", Movies.Count));
                    IsLoading = false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to load more movies: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
        }

        [RelayCommand]
        async Task GoToDetails(Movie movie)
        {
            if (movie == null)
                return;

            var genres = movie.Genres.Split(",").ToList();
            var isInWatchList = await _watchListService.IsMovieInWathcList(movie.Id);

            await Shell.Current.GoToAsync(nameof(MovieDetailsPage), true, new Dictionary<string, object>
            {
                {"Movie", movie},
                {"Genres", genres},
                {"IsInWatchList", isInWatchList}
            });
        }
    }
}
