using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Data
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        //public DateTime joined{get;set;} = System.DateTime.Now;
        //[EmailAddress]
        public string Email{get;set;} = "";

        public List<Group> Groups { get; } = new();
    }
}
