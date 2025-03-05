using RentService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using RentService.Domain.Entities;
using RentService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentService.Infrastructure.Persistence.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private readonly RentMicroserviceContext _context;

        public StatusRepository(RentMicroserviceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Status>> GetAllAsync()
        {
            return await _context.Statuses.ToListAsync();
        }

        public async Task<Status> GetByIdAsync(int id)
        {
            var status = await _context.Statuses
                   .FirstOrDefaultAsync(s => s.Id == id);
            return status;
        }

        
    }
}
