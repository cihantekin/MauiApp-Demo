using MauiApp_Demo.Models;
using System.Text.Json;

namespace MauiApp_Demo.Services
{
    public class MovieService
    {
        List<Movie> movieList;
        public async Task<List<Movie>> GetMoviesAsync()
        {
            if (movieList?.Count > 0)
            {
                return movieList;
            }

            using var stream = await FileSystem.OpenAppPackageFileAsync("movies.json");
            using StreamReader reader = new(stream);
            string json = await reader.ReadToEndAsync();

            movieList = JsonSerializer.Deserialize<List<Movie>>(json);
            movieList = movieList.Take(25).ToList();

            return movieList;
        }
    }
}
