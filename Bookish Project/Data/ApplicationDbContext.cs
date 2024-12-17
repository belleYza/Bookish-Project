using Bookish_Project.Model;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Collections.Generic;

namespace Bookish_Project.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProgress> UserProgress { get; set; }
    }
}