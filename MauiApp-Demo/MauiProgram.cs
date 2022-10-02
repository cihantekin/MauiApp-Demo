using MauiApp_Demo.Services;
using MauiApp_Demo.ViewModel;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MauiApp_Demo;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Movie.db");
        builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<MovieService>(s, dbPath));
		builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<WatchListService>(s, dbPath));
		builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<FavoriteService>(s, dbPath));

		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<MoviesViewModel>();

        builder.Services.AddTransient<MovieDetailsPage>();
        builder.Services.AddTransient<MovieDetailsViewModel>();

		builder.Services.AddTransient<FavoritesPage>();
		builder.Services.AddTransient<FavoritesViewModel>();

		builder.Services.TryAddTransient<WatchListPage>();
		builder.Services.AddTransient<WatchListViewModel>();


        return builder.Build();
	}
}
