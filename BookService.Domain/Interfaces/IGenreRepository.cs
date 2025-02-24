using BookService.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookService.Domain.Interfaces
{
    public interface IGenreRepository
    {
        Task<Genre> GetByIdAsync(int id);
        Task<IEnumerable<Genre>> GetByIdsAsync(List<int> ids);
        Task<IEnumerable<Genre>> GetAllAsync();
        Task AddAsync(Genre genre);
        Task UpdateAsync(Genre genre);
        Task DeleteAsync(int id);
    }
}