using RentService.Domain.Entities;

namespace RentService.Domain.Interfaces
{
    public interface ILibrarianRepository
    {
        Task<Librarian> GetByIdAsync(int id);

        Task<Librarian> GetByEmailAsync(string email);
        Task<IEnumerable<Librarian>> GetAllAsync();
        Task AddAsync(Librarian librarian);
        Task UpdateAsync(Librarian librarian);
        Task DeleteAsync(int id);
    }
}
