using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp_Demo.Models;
using MauiApp_Demo.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MauiApp_Demo.ViewModel
{
    public partial class MoviesViewModel : BaseViewModel
    {
        public ObservableCollection<Movie> Movies { get; } = new();

        private readonly MovieService _movieService;

        [ObservableProperty]
        bool isRefreshing;

        public MoviesViewModel(MovieService movieService)
        {
            Title = "Movies";
            this._movieService = movieService;
        }

        [RelayCommand]
        async Task GetMoviesAsync()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                IsBusy = true;
                var movies = await _movieService.GetMoviesAsync();

                if (Movies.Any())
                {
                    Movies.Clear();
                }

                foreach (var movie in movies)
                    Movies.Add(movie);
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
            if (IsBusy)
            {
                return;
            }

            try
            {
                IsBusy = true;
                var movies = await _movieService.GetMoviesAsync(text);

                if (Movies.Any())
                {
                    Movies.Clear();
                }

                foreach (var movie in movies)
                    Movies.Add(movie);
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
    }
}
