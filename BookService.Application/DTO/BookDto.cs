using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookService.Application.DTO
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? PublicationYear { get; set; }
        public string Description { get; set; }
        public List<AuthorDto> Authors { get; set; } = new List<AuthorDto>();
        public List<GenreDto> Genres { get; set; } = new List<GenreDto>();
        public string Condition { get; set; }
        public bool IsAccess { get; internal set; }
    }
}
