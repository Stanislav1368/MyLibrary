using BookService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookService.Domain.Interfaces
{
    public interface IRenterRepository
    {
        Task<Renter> GetByIdAsync(int id);
        Task<IEnumerable<Renter>> GetAllAsync();
        Task AddAsync(Renter renter);
        Task UpdateAsync(Renter renter);
        Task DeleteAsync(int id);
    }
}
