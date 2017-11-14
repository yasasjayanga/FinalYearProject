using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Models
{
    public class EventModel
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Number { get; set; }
        [Required]
        public int Type { get; set; }
        [Required]
        [Range(1, 100, ErrorMessage = "Invalid Value")]
        public int Tickets { get; set; }
        [Required]
        public System.DateTime Date { get; set; }
        [Required]
        public string Venue { get; set; }
        [Required]
        public string Description { get; set; }
        public string Poster { get; set; }
        public int Status { get; set; }
        [Required]
        public string ContactEmail { get; set; }
        [Required]
        public string ContactName { get; set; }
        [Required]
        public string ContactPhone { get; set; }
    }
}
