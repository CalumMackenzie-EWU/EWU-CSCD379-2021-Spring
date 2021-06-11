using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace SecretSanta.Data
{
    public class SampleData
    {
        public SampleData()
        {
            

            
        }

        public void StartSeeding()
        {
            string workFrom = @"..\SecretSanta.Data\";
            string previousPath = Directory.GetCurrentDirectory();
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

            var gifts= new[]
            {
                new Gift { Id = 1, Title = "Exotic Spices", Url = "www.EasternAlliance.com", Priority = 3, GiftFor = users[2]},
                new Gift { Id = 2, Title = "Ceremonial Sword", Url = "www.NorthernKingdom.com", Priority = 4, GiftFor = users[1]},
                new Gift { Id = 3, Title = "Board Game", Url = "www.WesternEmpire.com", Priority = 2, GiftFor = users[0]}
                // new Gift { Id = 1, Title = "Exotic Spices", Url = "www.EasternAlliance.com", Priority = 3, GiftFor = users[2], UserId = users[2].Id},
                // new Gift { Id = 2, Title = "Ceremonial Sword", Url = "www.NorthernKingdom.com", Priority = 4, GiftFor = users[1], UserId = users[1].Id},
                // new Gift { Id = 3, Title = "Board Game", Url = "www.WesternEmpire.com", Priority = 2, GiftFor = users[0], UserId = users[0].Id}
            };

            
            dbContext.AddRange(users);
            dbContext.AddRange(groups);
            dbContext.AddRange(gifts);
            dbContext.SaveChanges();
            Directory.SetCurrentDirectory(previousPath);
        }
        
        
        public static void RegisterGroupUser(Group theGroup, User theUser)
        {
            /*
            theGroup.GroupUser.Add(new GroupUser(){
                User = theUser,
                UserId = theUser.Id,
                Group = theGroup,
                GroupId = theGroup.Id
            });
            */
            GroupUser newGU = new GroupUser();
            newGU.RegisterGroupAndUser(theGroup, theUser);
            //theGroup.GroupUser.Add(newGU);
            
        }

        public static void RegisterGroupAssignment(Group theGroup, Assignment theAssignment)
        {
            GroupAssignment newGA = new GroupAssignment();
            newGA.RegisterGroupAndAssignment(theGroup, theAssignment);
            //theGroup.GroupAssignment.Add(newGA);
            
        }
        
        public void RegisterGroupAndUser(Group theGroup, User theUser)
        {
            theGroup.Users.Add(theUser);
            theUser.Groups.Add(theGroup);
        }
        
    }
}