using MauiApp_Demo.Services;
using MauiApp_Demo.ViewModel;
using Microsoft.Extensions.DependencyInjection;
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

        builder.Services.AddSingleton<MovieService>();
        builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<MoviesViewModel>();

        builder.Services.AddTransient<MovieDetailsPage>();
        builder.Services.AddTransient<MovieDetailsViewModel>();

		builder.Services.AddTransient<FavoritesPage>();

		builder.Services.TryAddTransient<WatchListPage>();
		builder.Services.AddTransient<WatchListViewModel>();
        builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<WatchListService>(s, dbPath));


        return builder.Build();
	}
}
