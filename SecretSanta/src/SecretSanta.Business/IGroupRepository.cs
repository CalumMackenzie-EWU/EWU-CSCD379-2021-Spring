using System.Collections.Generic;
using SecretSanta.Data;
using System.Linq;

namespace SecretSanta.Business
{
    public interface IGroupRepository
    {
        ICollection<Group> List();
        Group? GetItem(int id);
        bool Remove(int id);
        Group Create(Group item);
        void Save(Group item);
        AssignmentResult GenerateAssignments(int groupId);
        //cal: added during display.
        bool AddUser(int groupId, int userId);
        bool RemoveUser(int groupId, int userId);
        List<User> GetUsers(int groupId);
        IQueryable<Assignment> GetAssignments(int groupId);
        
    }

}
