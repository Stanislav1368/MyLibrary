using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace BookService.Domain.Entities
{
    public class Genre
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название жанра обязательно.")]
        [StringLength(50, ErrorMessage = "Название жанра не должно превышать 50 символов.")]
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
