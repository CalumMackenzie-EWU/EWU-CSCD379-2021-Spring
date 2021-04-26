using System.Collections.Generic;

namespace SecretSanta.Data
{
    public static class TestData
    {
        public static List<User> Users{get;} = new()
        {
            new User(){Id = 0, FirstName = "David", LastName = "Goliath"},
            new User(){Id = 1, FirstName = "Samson", LastName = "Herryman"},
            new User(){Id = 2, FirstName = "Mother", LastName = "Tiamat"},
            new User(){Id = 3, FirstName = "Magenta", LastName = "Magneto"}
        };
            
    }
}