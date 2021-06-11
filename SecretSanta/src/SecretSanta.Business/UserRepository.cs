using System.Collections.Generic;
using SecretSanta.Data;
using System.Linq;

namespace SecretSanta.Business
{
    public class UserRepository : IUserRepository
    {
        private DbContext DbContext;
        private void GetContext()
        {
            DbContext = new SecretSanta.Data.DbContext();
        }
        public User Create(User item)
        {
            if (item is null)
            {
                throw new System.ArgumentNullException(nameof(item));
            }

            //MockData.Users[item.Id] = item;
            GetContext();
            DbContext.Users.Add(item);
            DbContext.SaveChanges();
            return item;
        }

        public User? GetItem(int id)
        {
            if (MockData.Users.TryGetValue(id, out User? user))
            // {
            //     return user;
            // }
            // return null;
            GetContext();
            return DbContext.Users.Find(id);
        }

        public ICollection<User> List()
        {
            //return MockData.Users.Values;
            return DbContext.Users.ToList();
        }

        public bool Remove(int id)
        {
            //return MockData.Users.Remove(id);
            User? toRemove = GetItem(id);
            if(toRemove is not null)
            {
                GetContext();
                DbContext.Users.Remove(toRemove);
                DbContext.SaveChanges();
                return true;
            }
            else{
                return false;
            }
        }

        public void Save(User item)
        {
            if (item is null)
            {
                throw new System.ArgumentNullException(nameof(item));
            }

            //MockData.Users[item.Id] = item;
            GetContext();
            Remove(item.Id);
            DbContext.Users.Add(item);
            DbContext.SaveChanges();
        }

        public List<User> GetAssignmentUsers(int id)
        {
            GetContext();
            List<Assignment> assignments = new List<Assignment>();
            assignments = DbContext.Assignments.Where(item => item.Giver.Id == id).ToList();
            List<User> rUsers = new List<User>();
            User temp = new User();
            int tempId = 0;

            foreach(Assignment asnmt in assignments)
            {
                tempId = DbContext.Assignments.Find(asnmt).Receiver.Id;
                if(tempId != 0)
                {
                     rUsers.AddRange(DbContext.Users.Where(item => item.Id == tempId));
                }
            }

            return rUsers;

        }

        public List<Gift> GetGifts(int id)
        {
            GetContext();
            return DbContext.Gifts.Where(item => item.GiftFor.Id == id).ToList();
        }
    }
}
