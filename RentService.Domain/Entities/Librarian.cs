using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RentService.Domain.Entities
{
    public class Librarian
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Login { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required, MaxLength(100)]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MaxLength(20)]
        public string Phone { get; set; }

        [JsonIgnore]
        public List<Rental> Rentals { get; set; } = new List<Rental>();
    }
}
