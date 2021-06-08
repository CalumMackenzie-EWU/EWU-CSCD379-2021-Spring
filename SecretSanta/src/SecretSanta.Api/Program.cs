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
            string workFrom = @"..\SecretSanta.Data\";
            Directory.SetCurrentDirectory(workFrom);
            using SecretSanta.Data.DbContext dbContext = new SecretSanta.Data.DbContext();
            dbContext.Database.Migrate();

            var users = new[]
            {
                new User { Id = 1, FirstName = "Edelgard", LastName = "Hresvelg", Email = "empire@west.com"},
                new User { Id = 2, FirstName = "Dmitri", LastName = "Blaiddyd" ,Email = "kingdom@north.com"},
                new User { Id = 3, FirstName = "Claude", LastName = "Riegan", Email = "alliance@east.com"}
            };
            var groups = new[]
            {
                new Group { Id = 1, Name = "Black Eagles", Users = new List<User>{users[0]}},
                new Group { Id = 2, Name = "Blue Lions", Users = new List<User>{users[1], users[2]}},
                new Group { Id = 3, Name = "Golden Deer", Users = new List<User>{users[0], users[1], users[2]}}
            };

            // var gifts= new[]
            // {
            //     new Gift { Id = 1, Title = "Exotic Spices", Url = "www.EasternAlliance.com", Priority = 3},
            //     new Gift { Id = 2, Title = "Ceremonial Sword", Url = "www.NorthernKingdom.com", Priority = 4},
            //     new Gift { Id = 3, Title = "Board Game", Url = "www.WesternEmpire.com", Priority = 2}
            // };


            // List<Group> Groups = new()
            // {
            //     new Group { Id = 1, Name = "Black Eagles"},
            //     new Group { Id = 2, Name = "Blue Lions"},
            //     new Group { Id = 3, Name = "Golden Deer"}
            // };

            // List<User> Users = new()
            // {
            //     new User { Id = 1, FirstName = "Edelgard", LastName = "Hresvelg", Email = "empire@west.com"},
            //     new User { Id = 2, FirstName = "Dmitri", LastName = "Blaiddyd" ,Email = "kingdom@north.com"},
            //     new User { Id = 3, FirstName = "Claude", LastName = "Riegan", Email = "alliance@east.com"}
            // };

            // List<Gift> Gifts= new()
            // {
            //     new Gift { Id = 1, Title = "Exotic Spices", Url = "www.EasternAlliance.com", Priority = 3},
            //     new Gift { Id = 2, Title = "Ceremonial Sword", Url = "www.NorthernKingdom.com", Priority = 4},
            //     new Gift { Id = 3, Title = "Board Game", Url = "www.WesternEmpire.com", Priority = 2}
            // };
            
            dbContext.AddRange(users);
            dbContext.AddRange(groups);
            //dbContext.AddRange(gifts);
            dbContext.SaveChanges();
        
        }//end main

        public static void RegisterGroupAndUser(Group theGroup, User theUser)
        {
            theGroup.Users.Add(theUser);
            theUser.Groups.Add(theGroup);
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
