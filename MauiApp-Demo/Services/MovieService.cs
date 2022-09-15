using MauiApp_Demo.Models;
using System.Linq.Expressions;
using System.Text.Json;

namespace MauiApp_Demo.Services
{
    public class MovieService
    {
        private List<Movie> movieList;
        public async Task<List<Movie>> GetMoviesAsync(string filter = "")
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("movies.json");
            using StreamReader reader = new(stream);
            string json = await reader.ReadToEndAsync();

            var movies = JsonSerializer.Deserialize<List<Movie>>(json).AsQueryable();

            if (!string.IsNullOrEmpty(filter))
            {
                movies = movies.Where(FilterMovies(filter));
            }

            movieList = movies.ToList();
            movieList = movies.Take(25).ToList();

            return movieList;
        }

        private static Expression<Func<Movie, bool>> FilterMovies(string filter) => x => x.Title.Contains(filter) || x.Plot.Contains(filter);
    }
}
