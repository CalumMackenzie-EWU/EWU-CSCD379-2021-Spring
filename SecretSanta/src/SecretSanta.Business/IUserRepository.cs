using System.Collections.Generic;
using SecretSanta.Data;
using System.Linq;

namespace SecretSanta.Business
{
    public interface IUserRepository
    {
        ICollection<User> List();
        User? GetItem(int id);
        bool Remove(int id);
        User Create(User item);
        void Save(User item);
        //cal: add during display.
        List<User> GetAssignmentUsers(int id);
        List<Gift> GetGifts(int id);
        
        

    }

}
