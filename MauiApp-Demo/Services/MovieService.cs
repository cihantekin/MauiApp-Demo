using MauiApp_Demo.Models;
using System.Text.Json;

namespace MauiApp_Demo.Services
{
    public class MovieService
    {
        List<Movie> movieList;
        public async Task<List<Movie>> GetMoviesAsync()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("movies.json");
            using StreamReader reader = new(stream);
            string json = await reader.ReadToEndAsync();

            Random r = new Random();
            var skip = r.Next(0, 50);

            movieList = JsonSerializer.Deserialize<List<Movie>>(json);
            movieList = movieList.Skip(skip).Take(25).ToList();

            return movieList;
        }
    }
}
