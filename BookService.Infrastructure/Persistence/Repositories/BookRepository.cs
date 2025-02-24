using BookService.Domain.Entities;
using BookService.Domain.Interfaces;
using BookService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BookService.Infrastructure.Persistence.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookMicroserviceContext _context;

        public BookRepository(BookMicroserviceContext context)
        {
            _context = context;
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _context.Books
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .ToListAsync();
        }

        public async Task AddAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var book = await GetByIdAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Book>> GetByFilterAsync(string? genre, string? author, string? title)
        {
            var query = _context.Books
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .AsQueryable();

            if (!string.IsNullOrEmpty(title))
                query = query.Where(b => b.Title.Contains(title));

            if (!string.IsNullOrEmpty(author))
                query = query.Where(b => b.Authors.Any(a => a.FullName.Contains(author)));

            if (!string.IsNullOrEmpty(genre))
                query = query.Where(b => b.Genres.Any(g => g.Name.Contains(genre)));

            return await query.ToListAsync();
        }
    }
}