using System.Collections.Generic;

namespace SecretSanta.Data
{
    public class GroupUser
    {
        public int GroupId { get; set; }
        public Group Group { get; set; } = new();
        public int UserId { get; set; }
        public User User { get; set; } = new();

        
        public void RegisterGroupAndUser(Group theGroup, User theUser)
        {
            
            this.User = theUser;
            this.UserId = theUser.Id;

            this.Group = theGroup;
            this.GroupId = theGroup.Id;
            
        }
    }
}