using Microsoft.EntityFrameworkCore;
using RentService.Domain.Entities;
using RentService.Domain.Interfaces;

namespace RentService.Infrastructure.Persistence.Repositories
{
    public class LibrarianRepository : ILibrarianRepository
    {
        private readonly RentMicroserviceContext _context;

        public LibrarianRepository(RentMicroserviceContext context)
        {
            _context = context;
        }

        public async Task<Librarian> GetByIdAsync(int id)
        {
            return await _context.Librarians
                .Include(l => l.Rentals)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<IEnumerable<Librarian>> GetAllAsync()
        {
            return await _context.Librarians.Include(l => l.Rentals).ToListAsync();
        }

        public async Task AddAsync(Librarian librarian)
        {
            await _context.Librarians.AddAsync(librarian);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Librarian librarian)
        {
            _context.Librarians.Update(librarian);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var librarian = await GetByIdAsync(id);
            if (librarian != null)
            {
                _context.Librarians.Remove(librarian);
                await _context.SaveChangesAsync();
            }
        }
    }
}
