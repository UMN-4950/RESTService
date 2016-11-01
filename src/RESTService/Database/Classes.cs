using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTService.Database
{
    public class Location
    {
        public decimal Latitude { get; set; }
        public decimal Longitute { get; set; }
        public DateTime RecordedTime { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public List<Location> Locations { get; set; }
        public List<User> Friends { get; set; }
    }

    public class Event
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public Location Location { get; set; }
        public List<User> Attendees { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
