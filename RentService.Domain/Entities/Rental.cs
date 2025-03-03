using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookService.Domain.Entities
{
    public class Rental
    {
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        public int RenterId { get; set; }
        public Renter Renter { get; set; }

        [Required]
        public int LibrarianId { get; set; }
        public Librarian Librarian { get; set; }

        [Required]
        public int StatusId { get; set; }
        public Status Status { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public DateTime? ActualReturnDate { get; set; }

        [MaxLength(500)]
        public string Review { get; set; }
    }
}
