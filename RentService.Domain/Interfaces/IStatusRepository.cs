using RentService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentService.Domain.Interfaces
{
    public interface IStatusRepository
    {
        Task<Status> GetByIdAsync(int id);
        Task<IEnumerable<Status>> GetAllAsync();
    }
}
