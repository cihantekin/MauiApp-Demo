using System.Text.Json.Serialization;

namespace MauiApp_Demo.Models
{

    public class MovieJsonModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("year")]
        public string Year { get; set; }

        [JsonPropertyName("runtime")]
        public string Runtime { get; set; }

        [JsonPropertyName("genres")]
        public List<string> Genres { get; set; }

        [JsonPropertyName("director")]
        public string Director { get; set; }

        [JsonPropertyName("actors")]
        public string Actors { get; set; }

        [JsonPropertyName("plot")]
        public string Plot { get; set; }

        [JsonPropertyName("posterUrl")]
        public string Poster { get; set; }
    }
}
