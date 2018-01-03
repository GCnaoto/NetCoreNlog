using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using NLog.Web;
using System;
using System.IO;

namespace AspNetCoreNlog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            IWebHost BuildWebHost() =>
                        WebHost.CreateDefaultBuilder(args)
                            .UseContentRoot(Directory.GetCurrentDirectory())
                            .UseNLog()
                            .UseStartup<Startup>()
                            .UseKestrel()
                            .UseIISIntegration()
                            .Build();
         

            var logger = NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();

            try
            {
                logger.Debug("init main");
                BuildWebHost().Run();
            }
            catch (Exception e)
            {
                //NLog: catch setup errors
                logger.Error(e, "Stopped program because of exception");
                throw;
            }
        }
       
    }
}
