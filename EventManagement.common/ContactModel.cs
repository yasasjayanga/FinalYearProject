using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.common
{
    public class ContactModel
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
    }
}
