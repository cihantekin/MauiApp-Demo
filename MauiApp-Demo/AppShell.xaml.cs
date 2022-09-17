namespace MauiApp_Demo;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(MovieDetailsPage), typeof(MovieDetailsPage));
	}
}
