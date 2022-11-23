using be_prototipo.Models;
using Microsoft.EntityFrameworkCore;

namespace be_prototipo
{
    public class ApplicationDbContext :DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Pets> Pets { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options){ }   
    }
}
