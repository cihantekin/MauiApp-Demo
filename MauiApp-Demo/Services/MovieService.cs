using MauiApp_Demo.Models;
using SQLite;
using System.Linq.Expressions;
using System.Text.Json;

namespace MauiApp_Demo.Services
{
    public class MovieService
    {
        readonly string _dbPath;
        private SQLiteAsyncConnection con;

        public MovieService(string dbPath)
        {
            _dbPath = dbPath;
        }

        private async Task InitAsync()
        {
            if (con != null)
                return;

            con = new SQLiteAsyncConnection(_dbPath);
            await con.CreateTableAsync<Movie>();

            var movieList = await con.Table<Movie>().ToListAsync();

            if (!movieList.Any())
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync("movies.json");
                using StreamReader reader = new(stream);
                string json = await reader.ReadToEndAsync();

                var moviesJson = JsonSerializer.Deserialize<List<MovieJsonModel>>(json).ToList();
                List<Movie> movies = new();

                foreach (var item in moviesJson)
                {
                    string genres = string.Join(',', item.Genres);
                    movies.Add(new Movie
                    {
                        Genres = genres,
                        Actors = item.Actors,
                        Director = item.Director,
                        Id = item.Id,
                        Plot = item.Plot,
                        Poster = item.Poster,
                        Runtime = item.Runtime,
                        Title = item.Title,
                        Year = item.Year
                    });
                }

                await con.InsertAllAsync(movies);
            }
        }

        public async Task<List<Movie>> GetMoviesAsync(string filter = "", int? skip = 0)
        {
            await InitAsync();

            var movies = filter == string.Empty ?
                        await con.Table<Movie>().Skip((int)skip).Take(25).ToListAsync() :
                        await con.Table<Movie>().Where(FilterMovies(filter)).ToListAsync();

            return movies;
        }

        private static Expression<Func<Movie, bool>> FilterMovies(string filter) => x => x.Title.Contains(filter) || x.Plot.Contains(filter);
    }
}
