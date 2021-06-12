using System.Collections.Generic;
using SecretSanta.Data;
using System.Linq;
using System.IO;

namespace SecretSanta.Business
{
    public class UserRepository : IUserRepository
    {
        private DbContext DbContext;
        private string previousPath = Directory.GetCurrentDirectory();
        private void GetContext()
        {
            string workFrom = @"..\SecretSanta.Data\";
            //string previousPath = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(workFrom);
            DbContext = new SecretSanta.Data.DbContext();
            //Directory.SetCurrentDirectory(previousPath);
        }
        private void CloseOut()
        {
            Directory.SetCurrentDirectory(previousPath);
            DbContext.Dispose();
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

            CloseOut();
            return item;
        }

        public User? GetItem(int id)
        {
            //if (MockData.Users.TryGetValue(id, out User? user))
            // {
            //     return user;
            // }
            // return null;
            GetContext();

            var rUsers = DbContext.Users.Find(id);
            CloseOut();
            return rUsers;
        }

        public ICollection<User> List()
        {
            //return MockData.Users.Values;
            GetContext();
            var rUsers = DbContext.Users.ToList();
            CloseOut();
            return rUsers;
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
                CloseOut();
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
            CloseOut();
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
            CloseOut();
            return rUsers;

        }

        public List<Gift> GetGifts(int id)
        {
            GetContext();
            var rGifts = DbContext.Gifts.Where(item => item.GiftFor.Id == id).ToList();
            CloseOut();
            return rGifts;
        }
    }
}
