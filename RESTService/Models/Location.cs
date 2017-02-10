﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RESTService.Models
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
        public DateTime Time { get; set; }

        // ForeignKey
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
