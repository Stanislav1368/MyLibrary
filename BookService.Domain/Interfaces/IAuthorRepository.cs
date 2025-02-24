using BookService.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookService.Domain.Interfaces
{
    public interface IAuthorRepository
    {
        Task<Author> GetByIdAsync(int id);
        Task<IEnumerable<Author>> GetByIdsAsync(List<int> ids);
        Task<IEnumerable<Author>> GetAllAsync();
        Task AddAsync(Author author);
        Task UpdateAsync(Author author);
        Task DeleteAsync(int id);
    }
}