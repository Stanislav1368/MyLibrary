using RentService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentService.Domain.Interfaces
{
    public interface IRentalRepository
    {
        Task<Rental> GetByIdAsync(int id);
        Task<IEnumerable<Rental>> GetAllAsync();
        Task AddAsync(Rental rental);
        Task UpdateAsync(Rental rental);
        Task DeleteAsync(int id);
        Task<bool> IsBookRentedAsync(int bookId);
      
    }
}
