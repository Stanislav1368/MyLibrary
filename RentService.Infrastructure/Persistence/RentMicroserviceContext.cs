using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using BookService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookService.Infrastructure.Persistence
{
    public class RentMicroserviceContext : DbContext
    {
        public RentMicroserviceContext(DbContextOptions<RentMicroserviceContext> options) : base(options) { }

        public DbSet<Librarian> Librarians { get; set; }
        public DbSet<Renter> Renters { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Status> Statuses { get; set; } 
    }
}

