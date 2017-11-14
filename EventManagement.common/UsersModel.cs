using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Models
{
    public class UsersModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
        public DateTime Dob { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int Country { get; set; }
        public string ProfilePhoto { get; set; }
        public long Id { get; set; }
        public string UserId { get; set; }
    }
}
