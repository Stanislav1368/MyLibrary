using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookService.Domain.Entities
{
    public class Author
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Полное имя автора обязательно.")]
        [StringLength(100, ErrorMessage = "Полное имя не должно превышать 100 символов.")]
        public string FullName { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [Required(ErrorMessage = "Страна обязательна.")]
        [StringLength(50, ErrorMessage = "Название страны не должно превышать 50 символов.")]
        public string Country { get; set; }

        [StringLength(500, ErrorMessage = "Биография не должна превышать 500 символов.")]
        public string Biography { get; set; }

        [JsonIgnore]
        public List<Book> Books { get; set; } = new List<Book>();
    }
}
