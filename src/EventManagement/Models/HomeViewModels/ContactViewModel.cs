using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Models.AccountViewModels
{
    public class ContactViewModel
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }

        [Required]      
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }
        
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
