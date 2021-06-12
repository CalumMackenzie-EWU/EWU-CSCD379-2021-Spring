using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SecretSanta.Business;
//using Microsoft.Extensions.Configuration;
//using System.Configuration;

namespace SecretSanta.Api
{

    public class Startup
    {
        /*cal: We removed since we couldnt resolve an error.
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration config)
        {
            Configuration = config;
        }*/

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddControllers();
            services.AddSwaggerDocument();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            
            /*//cal: We couldnt get this working. Kept erroring out to MSB3073 which 
            //is to do with relative pathing. But the Connection string made sense, were in the Api project. Go out one, and into the SecretSanta.Data folder. Still didnt work.
            services.AddDbContext<DbContext>(options =>
            {
                options.UseSqlite($"Data Source={Configuration.GetConnectionString("DbConnection")}");
                options.UseLoggerFactory(LoggerFactory.Create(builder =>
                {
                    builder.AddSerilog(Log.Logger.ForContext<DbContext>().ForContext("Catgegory", "Database"));
                }));

            });*/
            //
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseRouting();

            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
