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

        public async Task AddWatchList(int movieId)
        {
            await InitAsync();
            var watchListAdd = new WatchList
            {
                MovieId = movieId,
                IsDeleted = false,
            };

            await con.InsertAsync(watchListAdd);
        }

        public async Task<List<WatchList>> GetWatchList()
        {
            await InitAsync();
            return await con.Table<WatchList>().ToListAsync();
        }
    }
}
