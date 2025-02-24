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
    public class BookMicroserviceContext : DbContext
    {
        public BookMicroserviceContext(DbContextOptions<BookMicroserviceContext> options) : base(options) { }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }

    }
}
