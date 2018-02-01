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
        private readonly ILogger _logger;
        private readonly ILoggerFactory _loggerfact;

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

        public override Task<int> SaveChangesAsync()
        {
            ChangeTracker.DetectChanges();
            var serviceProvider = this.GetInfrastructure();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            //loggerFactory.AddProvider(new LoggerProvider(logAction));
            _loggerfact.AddDebug().CreateLogger<LogDbContext>().LogError("�X�V���Ă܂�");
            foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Added))
            {
                _loggerfact.AddDebug().CreateLogger<LogDbContext>().LogError("�X�V���Ă܂�");

            }
            
            return await this.SaveChangesAsync();
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