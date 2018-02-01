﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NLog.Web;
using System;
using System.IO;

namespace AspNetCoreNlog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var logger = NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
          
            try
            {
                logger.Debug("init main");
                BuildWebHost(args).Run();
            }
            catch (Exception e)
            {
                logger.Error(e, "Stopped program because of exception");
                throw;
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                            .UseStartup<Startup>()
                            .UseContentRoot(Directory.GetCurrentDirectory())
                            .UseNLog()
                            .UseKestrel()
                            .UseIISIntegration()
                            .Build();

    }
}
