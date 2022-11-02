using MauiApp_Demo.Models;
using SQLite;

namespace MauiApp_Demo.Services
{
    public class FavoriteService
    {
        readonly string _dbPath;
        private SQLiteAsyncConnection con;

        public FavoriteService(string dbPath)
        {
            _dbPath = dbPath;
        }
        private async Task InitAsync()
        {
            if (con != null)
                return;

            con = new SQLiteAsyncConnection(_dbPath);
            await con.CreateTableAsync<Favorite>();
        }

        public async Task<int> AddFavorites(Favorite favorite)
        {
            await InitAsync();
            if (await con.Table<Favorite>().FirstOrDefaultAsync(x => x.MovieId == favorite.MovieId && !x.IsDeleted) is not null)
            {
                return 0;
            }

            return await con.InsertAsync(favorite);
        }

        public async Task<List<Favorite>> GetFavorites()
        {
            await InitAsync();
            return await con.Table<Favorite>().OrderByDescending(x => x.Score).ToListAsync();
        }
    }
}
