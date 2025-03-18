using RentService.Domain.Entities;

namespace RentService.Domain.Interfaces
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
