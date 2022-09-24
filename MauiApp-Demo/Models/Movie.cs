using SQLite;
using TableAttribute = SQLite.TableAttribute;

namespace MauiApp_Demo.Models
{

    [Table("movies")]
    public class Movie
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string Runtime { get; set; }
        public string Genres { get; set; }
        public string Director { get; set; }
        public string Actors { get; set; }
        public string Plot { get; set; }
        public string Poster { get; set; }
    }
}
