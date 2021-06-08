using System.Collections.Generic;

namespace SecretSanta.Data
{
    public class GroupAssignment
    {
        public int GroupId { get; set; }
        public Group Group { get; set; } = new();
        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; } = new();

        
        public void RegisterGroupAndAssignment(Group theGroup, Assignment theAssignment)
        {
            
            this.Assignment = theAssignment;
            this.AssignmentId = theAssignment.Id;

            this.Group = theGroup;
            this.GroupId = theGroup.Id;
            
        }
    }
}