using BookService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookService.Domain.Interfaces
{
    public interface ILibrarianRepository
    {
        Task<Librarian> GetByIdAsync(int id);
        Task<IEnumerable<Librarian>> GetAllAsync();
        Task AddAsync(Librarian librarian);
        Task UpdateAsync(Librarian librarian);
        Task DeleteAsync(int id);
    }
}
