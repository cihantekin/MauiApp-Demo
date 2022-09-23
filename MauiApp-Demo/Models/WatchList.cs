using SQLite;
using TableAttribute = SQLite.TableAttribute;

namespace MauiApp_Demo.Models
{
    [Table("watchlist")]
    public class WatchList
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public int MovieId { get; set; }
    }
}
