using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BookService.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название книги обязательно.")]
        [StringLength(200, ErrorMessage = "Название книги не должно превышать 200 символов.")]
        public string Title { get; set; }

        public ICollection<Genre> Genres { get; set; } = new List<Genre>();

        public int? PublicationYear { get; set; }

        [Required(ErrorMessage = "Описание книги обязательно.")]
        [StringLength(1000, ErrorMessage = "Описание не должно превышать 1000 символов.")]
        public string Description { get; set; }

        [StringLength(100, ErrorMessage = "Состояние книги не должно превышать 100 символов.")]
        public string? Condition { get; set; }

        public ICollection<Author> Authors { get; set; } = new List<Author>();

        public bool IsAccess { get; set; } = true;
    }
}
