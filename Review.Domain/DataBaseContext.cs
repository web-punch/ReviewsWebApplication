using Microsoft.EntityFrameworkCore;
using Reviews.Domain.Models;

namespace Reviews.Domain
{
    public class DataBaseContext : DbContext
    {

        public DbSet<Review> Reviews { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
