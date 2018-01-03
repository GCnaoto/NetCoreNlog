﻿using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using NLog.Targets;
using Microsoft.AspNetCore.Http;
using NLog.Web;
using AspNetCoreNlog.Model;
using Swashbuckle.AspNetCore.Swagger;

namespace AspNetCoreNlog
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			// Add framework services.
			services.AddMvc();
            services.AddEntityFrameworkSqlServer().AddDbContext<LogDbContext>();

            // SwaggerGenサービスの登録
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("orders", new Info { Title = "Order APIs", Version = "v1" });
            });
            services.AddScoped<LogFilter>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddNLog();

            // Swaggerミドルウェアの登録
            app.UseSwagger();
            // SwaggerUIミドルウェアの登録
            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint("/swagger/orders/swagger.json", "Order APIs sandbox.");
            });

            app.UseMvc();
		}
		
		
    }
}
