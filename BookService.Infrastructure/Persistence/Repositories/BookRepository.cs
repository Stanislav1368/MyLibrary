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

        public async Task<IEnumerable<Book>> SearchBooksAsync(string? title, List<string>? genres, List<string>? authors, int? startYear, int? endYear)
        {
            var books = _context.Books
                .Include(b => b.Authors)
                .Include(b => b.Genres)
                .AsQueryable();

            if (!string.IsNullOrEmpty(title))
                books = books.Where(b => b.Title.Contains(title));

            if (authors != null && authors.Count != 0)
                books = books.Where(b => b.Authors.Any(a => authors.Contains(a.FullName)));

            if (genres != null && genres.Count != 0)
                books = books.Where(b => b.Genres.Any(g => genres.Contains(g.Name)));

            if (startYear.HasValue)
                books = books.Where(b => b.PublicationYear >= startYear.Value);

            if (endYear.HasValue)
                books = books.Where(b => b.PublicationYear <= endYear.Value);

            return await books.ToListAsync();
        }


    }
}