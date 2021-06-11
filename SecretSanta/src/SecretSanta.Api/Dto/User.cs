using System.Collections.Generic;

namespace SecretSanta.Api.Dto
{
    public class User
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        //cal: added during display.
        public List<User> AssignmentList{get;set;}
        //public List<Gift> GiiftList{get;set;}
        //

        //public static User? ToDto(Data.User? user)
        public static User? ToDto(Data.User? user, List<User>? assignmentList = null)
        {
            if (user is null) return null;
            return new User
            {
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,

                //
                AssignmentList = assignmentList ?? new()
            };
        }

        public static Data.User? FromDto(User? user)
        {
            if (user is null) return null;
            return new Data.User
            {
                Id = user.Id,
                FirstName = user.FirstName ?? "",
                LastName = user.LastName ?? ""
            };
        }
    }
}
