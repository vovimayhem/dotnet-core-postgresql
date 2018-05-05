using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MvcMovie.Models;

namespace MvcMovie
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            ////////////////////////////////////////////////////////////////////////
            // WRITE ENVIRONMENT VARIABLES TO THE LOG
            ////////////////////////////////////////////////////////////////////////
            // Console.WriteLine("[Startup] List of environment variables:");
            // var enumerator = Environment.GetEnvironmentVariables().GetEnumerator();
            // while (enumerator.MoveNext())
            // {
            //     Console.WriteLine($"{enumerator.Key,5}:{enumerator.Value,100}");
            // }
            ////////////////////////////////////////////////////////////////////////


            String databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

            // CHECK FOR LOCAL 'LOCAL_CONNECTION_STRING' ENVIRONMENT VARIABLE
            // (in case we're running on localhost)
            String connectionString = ParseDatabaseUrl(databaseUrl);

            // WRITE CONNECTION STRING TO THE LOG
            Console.WriteLine("********************************************************************************");
            Console.WriteLine("[Startup] Connection String: " + connectionString);
            Console.WriteLine("********************************************************************************");

            // NOW THAT WE HAVE OUR CONNECTION STRING
            // WE CAN ESTABLISH OUR DB CONTEXT
            services.AddDbContext<MvcMovieContext>
            (
		          opts => opts.UseNpgsql(connectionString)
	          );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public String ParseDatabaseUrl(String databaseUrl)
        {
          Uri url = new Uri(databaseUrl);
          String[] userAndPass = url.UserInfo.Split(':');
          String username = userAndPass[0];
          String password = userAndPass[1];
          String dbName = url.AbsolutePath.Substring(1);

          String connectionString = "Username=" + username + ";"
              + "Password=" + password + ";"
              + "Host=" + url.Host + ";"
              + "Port=" + url.Port + ";"
              + "Database=" + dbName + ";Pooling=true;";

          String useSSL = Environment.GetEnvironmentVariable("DATABASE_USE_SSL");

          if (!string.IsNullOrEmpty(useSSL) && useSSL == "true")
          {
            connectionString += "SSL Mode=Require;Trust Server Certificate=true";
          }

          return connectionString;
        }
    }
}
