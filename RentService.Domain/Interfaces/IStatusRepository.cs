using RentService.Domain.Entities;

namespace RentService.Domain.Interfaces
{
    public interface IStatusRepository
    {
        Task<Status> GetByIdAsync(int id);
        Task<IEnumerable<Status>> GetAllAsync();
    }
}
