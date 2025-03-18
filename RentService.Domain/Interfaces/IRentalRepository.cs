using RentService.Domain.Entities;

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
