using MauiApp_Demo.ViewModel;
using System.ComponentModel;

namespace MauiApp_Demo;

public partial class MovieDetailsPage : ContentPage
{
    public MovieDetailsPage(MovieDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        viewModel.PropertyChanged += ViewModelPropertyChanged;
    }

    private void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "IsInWatchList")
        {
            var viewModel = (MovieDetailsViewModel)sender;
            if (viewModel.IsInWatchList)
            {
                HandleRemoveFromWatchListButtonStyle();
            }
            else
            {
                HandleAddWatchListButtonStyle();
            }
        }
    }

    private void HandleRemoveFromWatchListButtonStyle()
    {
        WatchListOperationsButton.Text = "-";
        WatchListOperationsButton.TextColor = Colors.Black;
        WatchListOperationsButton.BackgroundColor = Colors.Yellow;
    }

    private void HandleAddWatchListButtonStyle()
    {
        WatchListOperationsButton.Text = "+";
        WatchListOperationsButton.TextColor = Colors.Yellow;
        WatchListOperationsButton.BackgroundColor = Colors.Transparent;
    }

    async void OnFavoriteButtonClicked(object sender, EventArgs args)
    {
        var model = (MovieDetailsViewModel)BindingContext;
        string movieScore = string.Empty;
        string movieNotes = await DisplayPromptAsync("Add Favorites", "Would you like to write some personel notes about movie:");

        if (movieNotes != null)
        {
            movieScore = await DisplayActionSheet("Your points for movie:", "Cancel", null, "1", "2", "3", "4", "5", "6", "7", "8", "9", "10");
        }
        
        await model.HandleFavoriteOperations(movieNotes, movieScore);
    }
}
