using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp_Demo.Models;
using MauiApp_Demo.Services;
using MvvmHelpers;
using System.Diagnostics;

namespace MauiApp_Demo.ViewModel
{
    public partial class WatchListViewModel : BaseViewModel
    {
        private readonly WatchListService _watchListService;
        public ObservableRangeCollection<WatchList> WatchList { get; } = new ();

        public WatchListViewModel(WatchListService watchListService)
        {
            _watchListService = watchListService;
            _ = GetWatchListAsync();
        }

        [RelayCommand]
        async Task GetWatchListAsync()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                WatchList.AddRange(await _watchListService.GetWatchList());
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get watch list: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
