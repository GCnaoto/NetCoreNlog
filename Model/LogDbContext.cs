using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AspNetCoreNlog.Model
{
    public class LogDbContext : DbContext
    {

        public LogDbContext(DbContextOptions<LogDbContext> options)
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
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=logdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            var loggerFactory = new LoggerFactory().AddConsole().AddDebug();
            optionsBuilder.UseLoggerFactory(loggerFactory);
            //optionsBuilder.UseSqlite(@"Data Source='hello.db'");
        }
    }
}