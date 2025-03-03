    using Microsoft.EntityFrameworkCore;
    using BookService.Domain.Entities;
    using BookService.Domain.Interfaces;

    namespace BookService.Infrastructure.Persistence.Repositories
    {
        public class RentalRepository : IRentalRepository
        {
            private readonly RentMicroserviceContext _context;

            public RentalRepository(RentMicroserviceContext context)
            {
                _context = context;
            }

            public async Task<Rental> GetByIdAsync(int id)
            {
                return await _context.Rentals
                    .Include(r => r.Renter)
                    .Include(r => r.Librarian)
                    .Include(r => r.Status)
                    .FirstOrDefaultAsync(r => r.Id == id);
            }

            public async Task<IEnumerable<Rental>> GetAllAsync()
            {
                return await _context.Rentals
                    .Include(r => r.Renter)
                    .Include(r => r.Librarian)
                    .Include(r => r.Status)
                    .ToListAsync();
            }

            public async Task AddAsync(Rental rental)
            {
                await _context.Rentals.AddAsync(rental);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateAsync(Rental rental)
            {
                _context.Rentals.Update(rental);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteAsync(int id)
            {
                var rental = await GetByIdAsync(id);
                if (rental != null)
                {
                    _context.Rentals.Remove(rental);
                    await _context.SaveChangesAsync();
                }
            }
            public async Task<bool> IsBookRentedAsync(int bookId)
            {
                return await _context.Rentals.AnyAsync(r => r.BookId == bookId && r.Status.Name != "Возврат" && r.ActualReturnDate == null);

            }   
        }

    }
