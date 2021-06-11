using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SecretSanta.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace SecretSanta.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            //cal: added this below when trying to seed database
            //SeedData sData = new SeedData();
            SampleData sData = new SampleData();
            sData.StartSeeding();
        
        }//end main

       
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
