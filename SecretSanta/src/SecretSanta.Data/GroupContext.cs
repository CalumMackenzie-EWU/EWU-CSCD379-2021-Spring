using System.Collections.Generic;

namespace SecretSanta.Data
{
    public static class GroupContext
    {
        static GroupContext()
        {
            Groups[0].Users.Add(Users[0]);
            Groups[0].Users.Add(Users[1]);
            Groups[0].Users.Add(Users[2]);
            Groups[1].Users.Add(Users[2]);
            Groups[2].Users.Add(Users[0]);
            Groups[2].Users.Add(Users[1]);
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
    }
}