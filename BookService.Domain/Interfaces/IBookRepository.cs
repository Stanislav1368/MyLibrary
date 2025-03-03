using BookService.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookService.Domain.Interfaces
{
    public interface IBookRepository
    {
        Task<Book> GetByIdAsync(int id);
        Task<IEnumerable<Book>> GetAllAsync();
        Task AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(int id);
        Task<IEnumerable<Book>> SearchBooksAsync(string? title, List<string>? genres, List<string>? authors, int? startYear, int? endYear);
    }
}