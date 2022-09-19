﻿using MauiApp_Demo.Services;
using MauiApp_Demo.ViewModel;
using Microsoft.Extensions.DependencyInjection;

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

		builder.Services.AddSingleton<MovieService>();
		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<MoviesViewModel>();

        builder.Services.AddTransient<MovieDetailsPage>();
        builder.Services.AddTransient<MovieDetailsViewModel>();

		builder.Services.AddTransient<FavoritesPage>();


        return builder.Build();
	}
}
