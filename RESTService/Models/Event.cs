using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RESTService.Models
{
    /// <summary>
    /// Each event has a list of Attendances
    /// Each event has a list of Locations
    /// </summary>
    public class Event
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Info { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<User> Attendance { get; set; }
        [Required]
        public List<Location> Locations { get; set; }
    }
}