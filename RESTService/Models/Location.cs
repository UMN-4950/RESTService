
using System;
using System.ComponentModel.DataAnnotations;

namespace RESTService.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
        private DateTime Time { get; set; }
    }
}
