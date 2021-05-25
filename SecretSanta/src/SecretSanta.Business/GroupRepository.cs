using System;
using System.Collections.Generic;
using SecretSanta.Data;

namespace SecretSanta.Business
{
    public class GroupRepository : IGroupRepository
    {
        public Group Create(Group item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            MockData.Groups[item.Id] = item;
            return item;
        }

        public Group? GetItem(int id)
        {
            if (MockData.Groups.TryGetValue(id, out Group? user))
            {
                return user;
            }
            return null;
        }

        public ICollection<Group> List()
        {
            return MockData.Groups.Values;
        }

        public bool Remove(int id)
        {
            return MockData.Groups.Remove(id);
        }

        public void Save(Group item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            MockData.Groups[item.Id] = item;
        }

        //cal: below is stuff we are adding for asmt 8.
        public AssignmentResult GenerateAssignments(int id)
        {
            if(!MockData.Groups.ContainsKey(id))
            {
                return AssignmentResult.Error("Group not found.");

            }

            MockData.Groups[id].Assignments = new();
            List<User> users = MockData.Groups[id].Users;

            if(users.Count < 3)
            {
                return AssignmentResult.Error("A group must have atleast 3 users.");
            }

            Shuffle(users);

            for(int x = 0; x < users.Count; x++)
            {
                if(x < users.Count - 1)
                {
                    MockData.Groups[id].Assignments.Add(new Assignment(users[x], users[x + 1]));
                }
                else{
                    MockData.Groups[id].Assignments.Add(new Assignment(users[x], users[0]));
                }
            }
            return AssignmentResult.Success();
        }

         //cal: list shuffle taken from stack overflow
        private void Shuffle<T>(IList<T> list)
        {
            Random rand = new Random();
            int count = list.Count;
            while(count > 1)
            {
                count--;
                int nextRand = rand.Next(count+1);
                T value = list[nextRand];
                list[nextRand] = list[count];
                list[count] = value;
            }
        }
    }
}
