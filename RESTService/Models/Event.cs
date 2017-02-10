using System;
using System.ComponentModel.DataAnnotations;

namespace RESTService.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Info { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        //public List<User> Attendance { get; set; }
        //[Required]
        //public List<Location> Locations { get; set; }
    }
}