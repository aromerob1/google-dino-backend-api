using Microsoft.EntityFrameworkCore;

namespace google_dino_backend.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        public DbSet<Score> Scores { get; set; }

    }
}
