using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Models
{
    public class UsersViewModel
    {
        [Key]
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Dob { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int Country { get; set; }
        public string ProfilePhoto { get; set; }

        public string UserId { get; set; }
    }
}
