using System.Collections.Generic;

namespace SecretSanta.Data
{
    public static class SeedData
    {
        static SeedData()
        {
            /*
            Groups[2].Users.Add(Users[0]);
            Groups[2].Users.Add(Users[1]);
            Groups[2].Users.Add(Users[2]);
            Groups[1].Users.Add(Users[2]);
            Groups[0].Users.Add(Users[0]);
            Groups[0].Users.Add(Users[1]);
            */
            RegisterGroupAndUser(Groups[2], Users[0]);
            RegisterGroupAndUser(Groups[2], Users[1]);
            RegisterGroupAndUser(Groups[2], Users[2]);
            RegisterGroupAndUser(Groups[1], Users[2]);
            RegisterGroupAndUser(Groups[0], Users[0]);
            RegisterGroupAndUser(Groups[0], Users[1]);

            Gifts[0].GiftFor = Users[2];
            Gifts[1].GiftFor = Users[1];
            Gifts[2].GiftFor = Users[0];

            
        }

        public static List<Group> Groups { get; } = new()
        {
            new Group() { Id = 1, Name = "Black Eagles"},
            new Group() { Id = 2, Name = "Blue Lions"},
            new Group() { Id = 3, Name = "Golden Deer"}
        };

        public static List<User> Users { get; } = new()
        {
            new User() { Id = 1, FirstName = "Edelgard", LastName = "Hresvelg" },
            new User() { Id = 2, FirstName = "Dmitri", LastName = "Blaiddyd" },
            new User() { Id = 3, FirstName = "Claude", LastName = "Riegan" }
        };

        public static List<Gift> Gifts{get;} = new()
        {
            new Gift() { Id = 1, Title = "Exotic Spices", Url = "alliance@east.com", Priority = 3},
            new Gift() { Id = 2, Title = "Ceremonial Sword", Url = "kingdom@north.com", Priority = 4},
            new Gift() { Id = 3, Title = "Board Game", Url = "empire@west.com", Priority = 2}
        };

        public static void RegisterGroupAndUser(Group theGroup, User theUser)
        {
            theGroup.Users.Add(theUser);
            theUser.Groups.Add(theGroup);
        }
    }
}