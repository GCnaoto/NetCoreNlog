using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreNlog.Model
{
    public class LogDbContext : DbContext
    {
        private ILoggerFactory _loggerfact;

        public LogDbContext(DbContextOptions<LogDbContext> options, ILoggerFactory logger)
        {
            //_logger = logger.CreateLogger("TodoApi.Controllers.TodoController");
            //Database.EnsureCreated();
        }

        public DbSet<Logs> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);

        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {

            _loggerfact.AddDebug().CreateLogger<LogDbContext>().LogError("çXêVÇµÇƒÇ‹Ç∑");
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

        }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=logdb2;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            _loggerfact = new LoggerFactory().AddConsole().AddDebug();

            optionsBuilder.UseLoggerFactory(_loggerfact);
            //optionsBuilder.UseSqlite(@"Data Source='hello.db'");
        }
    }
}