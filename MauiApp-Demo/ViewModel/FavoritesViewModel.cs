using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp_Demo.Models;
using MauiApp_Demo.Services;
using MvvmHelpers;
using System.Diagnostics;

namespace MauiApp_Demo.ViewModel
{
    public partial class FavoritesViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Favorite> FavoriteList { get; } = new();
        private readonly FavoriteService _favoriteService;

        [ObservableProperty]
        bool isRefreshing;

        public FavoritesViewModel(FavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
            _ = GetFavoriteListAsync();
        }

        [RelayCommand]
        async Task GetFavoriteListAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                FavoriteList.ReplaceRange(await _favoriteService.GetFavorites());
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get favorite movies: {ex.Message}");
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
