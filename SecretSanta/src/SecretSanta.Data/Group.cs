using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecretSanta.Data
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";

        //public List<GroupUser> GroupUser = new();
        //public List<GroupAssignment> GroupAssignment = new();
        

        //[NotMapped]
        //public List<User> Users { get; } = new();  
        public List<User> Users { get; set;} 
        //public List<Assignment> Assignments { get; } = new();
        public List<Assignment> Assignments { get; set;}

        
    }
}
