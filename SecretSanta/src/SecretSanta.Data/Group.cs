using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecretSanta.Data
{
    //cal: Commenting out my GroupUser etc stuff atm. It isnt necessary to insert data into the db. But may come back to it
    //if I run into problems later down the line. As it is, it causes more headaches than help.
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
