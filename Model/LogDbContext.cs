using Microsoft.EntityFrameworkCore;

namespace AspNetCoreNlog.Model
{
    public class LogDbContext : DbContext
    {

        public LogDbContext(DbContextOptions<LogDbContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Log> Log { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=logdb;");
            //optionsBuilder.UseSqlite(@"Data Source='hello.db'");
        }
    }
}