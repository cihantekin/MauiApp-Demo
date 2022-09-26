using MauiApp_Demo.Models;
using SQLite;

namespace MauiApp_Demo.Services
{
    public class WatchListService
    {
        readonly string _dbPath;
        private SQLiteAsyncConnection con;

        public WatchListService(string dbPath)
        {
            _dbPath = dbPath;
        }

        private async Task InitAsync()
        {
            if (con != null)
                return;

            con = new SQLiteAsyncConnection(_dbPath);
            await con.CreateTableAsync<WatchList>();
        }

        public async Task AddWatchList(Movie movie)
        {
            await InitAsync();
            if (await con.Table<WatchList>().FirstOrDefaultAsync(x => x.MovieId == movie.Id && !x.IsDeleted) is not null)
            {
                return;
            }

            var watchListAdd = new WatchList
            {
                MovieId = movie.Id,
                MovieName = movie.Title,
                Poster = movie.Poster,
                IsDeleted = false,
            };

            await con.InsertAsync(watchListAdd);
        }

        public async Task<List<WatchList>> GetWatchList()
        {
            await InitAsync();
            return await con.Table<WatchList>().ToListAsync();
        }

        public async Task<bool> IsMovieInWathcList(int movieId)
        {
            await InitAsync();
            return await con.Table<WatchList>().FirstOrDefaultAsync(x => x.MovieId == movieId) != null; 
        }
    }
}
