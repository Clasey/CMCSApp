using Microsoft.EntityFrameworkCore;
using CMCSApp.Models;

namespace CMCSApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Claim> Claims => Set<Claim>();
    }
}
