using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookService.Domain.Entities
{

    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public List<Genre> Genres { get; set; } = new List<Genre>();

        public int? PublicationYear { get; set; }

        public string Description { get; set; }

        public bool IsAccess { get; set; }

        public string Condition { get; set; }

        public List<Author> Authors { get; set; } = new List<Author>();
        //public List<Rental> Rentals { get; set; } = new List<Rental>();
    }
}
