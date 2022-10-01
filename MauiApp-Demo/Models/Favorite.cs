using SQLite;

namespace MauiApp_Demo.Models
{
    [Table("favorite")]
    public class Favorite
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public string Poster { get; set; }
        public string MovieNotes { get; set; }
        public int Score { get; set; }
        public bool IsDeleted { get; set; }
    }
}
