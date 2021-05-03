using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SecretSanta.Web.Api;

namespace SecretSanta.Web
{
    public class Startup
    {
        //public static System.Net.Http.HttpClient ApiClient = new();//cal: This is a default. The below one tacks on more information.
        public static System.Net.Http.HttpClient ApiClient = new()
        {
            BaseAddress = new Uri("https://localhost:5101")
        };

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //System.Net.Http.HttpClient client = new();
            //cal: dont dispose of the client or you may run out of tcp sockets.
            //services.AddScoped<UsersClient>()//cal: this only calls the default constructor. Have to use the one below.
            services.AddScoped<UsersClient>(x=> new UsersClient(ApiClient));
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
