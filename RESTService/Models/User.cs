using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RESTService.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        public List<User> Friends { get; set; }
        public List<Location> Checkins { get; set; }
    }
}