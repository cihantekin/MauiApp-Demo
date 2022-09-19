namespace MauiApp_Demo;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(MovieDetailsPage), typeof(MovieDetailsPage));
		Routing.RegisterRoute(nameof(FavoritesPage), typeof(FavoritesPage));
		Routing.RegisterRoute(nameof(WatchListPage), typeof(WatchListPage));
	}
}
