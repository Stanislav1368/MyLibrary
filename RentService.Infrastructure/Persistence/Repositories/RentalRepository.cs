    using Microsoft.EntityFrameworkCore;
    using RentService.Domain.Entities;
    using RentService.Domain.Interfaces;

    namespace RentService.Infrastructure.Persistence.Repositories
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
                return _context.Rentals.Any(r => r.BookId == bookId && r.Status.Id != 2 && r.ActualReturnDate == null);
            }   
        }

    }
