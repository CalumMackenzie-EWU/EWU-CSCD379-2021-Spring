using System;
using System.Collections.Generic;
using System.Linq;
using SecretSanta.Business;
using SecretSanta.Data;

namespace SecretSanta.Api.Tests.Business
{
    internal class TestableGroupRepository : IGroupRepository
    {
        private Dictionary<int, Group> Groups { get; } = new();

        public Group Create(Group item)
        {
            Groups.Add(item.Id, item);
            return item;
        }

        public AssignmentResult? AssignmentResult { get; set; }
        public AssignmentResult GenerateAssignments(int groupId)
        {
            return AssignmentResult ?? throw new InvalidOperationException();
        }

        public Group? GetItem(int id)
        {
            Groups.TryGetValue(id, out Group? rv);
            return rv;
        }

        public ICollection<Group> List() => Groups.Values;

        public bool Remove(int id) => Groups.Remove(id);

        public void Save(Group item) => Groups[item.Id] = item;

        public bool AddUser(int groupId, int userId)
        {
            throw new NotImplementedException();
        }

        public bool RemoveUser(int groupId, int userId)
        {
            throw new NotImplementedException();
        }

        public List<User> GetUsers(int groupId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Assignment> GetAssignments(int groupId)
        {
            throw new NotImplementedException();
        }
    }
}
