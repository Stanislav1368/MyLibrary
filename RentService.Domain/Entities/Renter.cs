using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RentService.Domain.Entities
{
    public class Renter
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FullName { get; set; }

        [Required, MaxLength(200)]
        public string Address { get; set; }

        [Required, MaxLength(20)]
        public string Phone { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [JsonIgnore]
        public List<Rental> Rentals { get; set; } = new List<Rental>();
    }
}
