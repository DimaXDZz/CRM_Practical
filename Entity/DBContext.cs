using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Classes
{
    public class DBContext:DbContext
    {
        public DbSet<User> User { get; set; } = null!;
        public DbSet<Abonent> Abonent { get; set; } = null!;
        public DbSet<Package> Package { get; set; } = null!;
        public DbSet<Agreement> Agreement { get; set; } = null!;
        public DbSet<History> History { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-H74P6EH\SQLEXPRESS;Database=CRM_Beltelecom;Trusted_Connection=True;TrustServerCertificate=True;");
        }

    }
}
