using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SecretSanta.Business;

namespace SecretSanta.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddTransient //cal: this calls the constructor each time it needs a new one.
            //services.AddScoped //cal: calls for each http request. Two users could get the same instance of the object.
            services.AddSingleton<IUserRepository, UserManager>();//cal: this will call your constructor once.
            services.AddControllers();//cal: added during assignemnt 4.
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //cal: this was added and the one below was removed for Assignment 4.
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            /*
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello from API!");
                });
            });
            */
        }
    }
}
