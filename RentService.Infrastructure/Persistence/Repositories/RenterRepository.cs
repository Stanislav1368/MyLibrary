using Microsoft.EntityFrameworkCore;
using RentService.Domain.Entities;
using RentService.Domain.Interfaces;

namespace RentService.Infrastructure.Persistence.Repositories
{
    public class RenterRepository : IRenterRepository
    {
        private readonly RentMicroserviceContext _context;

        public RenterRepository(RentMicroserviceContext context)
        {
            _context = context;
        }

        public async Task<Renter> GetByIdAsync(int id)
        {
            var renter = await _context.Renters
                   .Include(r => r.Rentals)
                   .FirstOrDefaultAsync(r => r.Id == id);

            if (renter == null)
            {
                throw new Exception($"Арендатор с таким Id не найден");
            }

            return renter;
        }

        public async Task<IEnumerable<Renter>> GetAllAsync()
        {
            return await _context.Renters.Include(r => r.Rentals).ToListAsync();
        }

        public async Task AddAsync(Renter renter)
        {
            await _context.Renters.AddAsync(renter);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Renter renter)
        {
            _context.Renters.Update(renter);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var renter = await GetByIdAsync(id);
            if (renter != null)
            {
                _context.Renters.Remove(renter);
                await _context.SaveChangesAsync();
            }
        }
    }
}
