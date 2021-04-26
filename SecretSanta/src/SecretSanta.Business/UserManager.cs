using System.Collections.Generic;
using SecretSanta.Data;
using System.Linq;//cal: needed for FirstOrDefault
using System;//cal: needed for exceptions

namespace SecretSanta.Business
{
    public class UserManager : IUserRepository
    {
         public User Create(User item)
        {
            TestData.Users.Add(item);
            return item;
        }

        public User? GetItem(int id)
        {
            if(id < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }
            return TestData.Users.FirstOrDefault(x => x.Id == id);
        }

        public ICollection<User> List()
        {
            return TestData.Users;
        }

        public bool Remove(int id)
        {
            User? foundUser = TestData.Users.FirstOrDefault(x => x.Id == id);
            if (foundUser is not null)
            {
                TestData.Users.Remove(foundUser);
                return true;
            }
            return false;
        }

        public void Save(User item)
        {
            Remove(item.Id);
            Create(item);
        }
    }
    
}