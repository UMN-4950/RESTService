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
        public string EventName { get; set; }
        public string EventInfo { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public List<User> EventAttendance { get; set; }
        [Required]
        public List<Location> EventLocations { get; set; }
    }
}