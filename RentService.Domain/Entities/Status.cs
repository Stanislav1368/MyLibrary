using System.ComponentModel.DataAnnotations;

namespace RentService.Domain.Entities
{
    public class Status
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }
    }
}
