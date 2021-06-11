using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SecretSanta.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.IO;
using System;
using Serilog;


namespace SecretSanta.Api
{
    public class Program
    {
        public static ILoggerFactory LoggerFactory{get;set;}//cal: part of MS.Ex.Logging
        private static Serilog.ILogger Logger{get;set;}//cal: This ones from Serilog
        public static void Main(string[] args)
        {
            //cal:added during logging
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.File("db.log")
            .CreateLogger();

            Logger = Log.Logger.ForContext<Program>();
            try{
                Logger.Information("Startup");
            }
            catch(Exception e)
            {
                Logger.Fatal(e, "Could not write log file at startup.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
            //
            CreateHostBuilder(args).Build().Run();

            //cal: added this below when trying to seed database
            //SeedData sData = new SeedData();
            SampleData sData = new SampleData();
            sData.StartSeeding();
        
        }//end main

       
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
