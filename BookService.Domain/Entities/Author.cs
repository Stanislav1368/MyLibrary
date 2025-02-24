using System.Text.Json.Serialization;

namespace BookService.Domain.Entities
{
    public class Author
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public DateTime? BirthDate { get; set; }

        public string Country { get; set; }

        public string Biography { get; set; }
        [JsonIgnore]
        public List<Book> Books { get; set; } = new List<Book>();
    }
}
