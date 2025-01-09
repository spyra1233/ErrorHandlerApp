using Microsoft.EntityFrameworkCore;
using ErrorHandlerApp.Models;

namespace ErrorHandlerApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Bug> Bugs { get; set; } = null!;
        public DbSet<Report> Reports { get; set; } = null!;
    }
}
