using System;
using System.Collections.Generic;
using SecretSanta.Data;
using System.Linq;

namespace SecretSanta.Business
{
    public class GroupRepository : IGroupRepository
    {
        private DbContext DbContext;
        private void GetContext()
        {
            DbContext = new SecretSanta.Data.DbContext();
        }
        public Group Create(Group item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            //MockData.Groups[item.Id] = item;
            GetContext();
            DbContext.Groups.Add(item);
            DbContext.SaveChanges();
            return item;
        }

        public Group? GetItem(int id)
        {
            // if (MockData.Groups.TryGetValue(id, out Group? user))
            // {
            //     return user;
            // }
            // return null;
            GetContext();
            return DbContext.Groups.Find(id);
        }

        public ICollection<Group> List()
        {
            //return MockData.Groups.Values;
            return DbContext.Groups.ToList();
        }

        public bool Remove(int id)//cal:!!!!THIS MIGHT NOT BE ENOUGH!!!!
        {
            //return MockData.Groups.Remove(id);
            Group? toRemove = GetItem(id);
            if(toRemove is not null)
            {
                GetContext();
                DbContext.Groups.Remove(toRemove);
                DbContext.SaveChanges();
                return true;
            }
            else{
                return false;
            }
        }

        public void Save(Group item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            //MockData.Groups[item.Id] = item;
            if(Remove(item.Id))
            {
                Create(item);
            }
        }

        public AssignmentResult GenerateAssignments(int groupId)
        {
            // if (!MockData.Groups.TryGetValue(groupId, out Group? group))
            // {
            //     return AssignmentResult.Error("Group not found");
            // }
            GetContext();
            Group? theGroup = DbContext.Groups.Find(groupId);
            if(theGroup is null)
            {
                return AssignmentResult.Error("Group not found");
            }

            Random random = new();
            //var groupUsers = new List<User>(group.Users);
            List<User> groupUsers = new List<User>(theGroup.Users);

            if (groupUsers.Count < 3)
            {
                return AssignmentResult.Error($"Group {theGroup.Name} must have at least three users");
            }

            var users = new List<User>();
            //Put the users in a random order
            while(groupUsers.Count > 0)
            {
                int index = random.Next(groupUsers.Count);
                users.Add(groupUsers[index]);
                groupUsers.RemoveAt(index);
            }

            //The assignments are created by linking the current user to the next user.
            theGroup.Assignments.Clear();
            for(int i = 0; i < users.Count; i++)
            {
                int endIndex = (i + 1) % users.Count;
                //group.Assignments.Add(new Assignment(users[i], users[endIndex]));
                
                //cal:added to try and get my basic db set up.
                theGroup.Assignments.Add(new Assignment());
                theGroup.Assignments[theGroup.Assignments.Count].Giver = users[i];
                theGroup.Assignments[theGroup.Assignments.Count].Receiver = users[endIndex];
                theGroup.Assignments[theGroup.Assignments.Count].Id = theGroup.Assignments.Max(g => g.Id) + 1;
            }
            DbContext.SaveChanges();
            return AssignmentResult.Success();
        }

        public bool AddUser(int groupId, int userId)
        {
            GetContext();
            Group? group = DbContext.Groups.Find(groupId);
            User? user = DbContext.Users.Find(userId);

            if(user is not null && group is not null)
            {
                group.Users.Add(user);
                DbContext.SaveChanges();
                return true;
            }
            else{
                return false;
            }   
        }

        public bool RemoveUser(int groupId, int userId)
        {
            GetContext();
            Group? group = DbContext.Groups.Find(groupId);
            User? user = DbContext.Users.Find(userId);

            if(user is not null && group is not null)
            {
                group.Users.Remove(user);
                DbContext.SaveChanges();
                return true;
            }
            else{
                return false;
            }
        }

        public List<User> GetUsers(int groupId)
        {
            GetContext();
            List<Group> groups = DbContext.Groups.Where(item => item.Id == groupId).ToList();
            List<User> rUsers = new List<User>();

            foreach(Group theGroup in groups)
            {
                rUsers.AddRange(theGroup.Users);
            }
            return rUsers;
        }

        public IQueryable<Assignment> GetAssignments(int groupId)
        {
            GetContext();
            return DbContext.Assignments.Where(item => item.GroupId == groupId);
        }
    }
}
