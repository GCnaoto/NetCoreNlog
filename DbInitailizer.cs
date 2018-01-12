using AspNetCoreNlog.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace AspNetCoreNlog
{
    public static class DbInitailizer
    {
        public static void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {

                var logDbContext = serviceScope.ServiceProvider.GetRequiredService<LogDbContext>();
                logDbContext.Database.Migrate();

            }
        }
    }
}
