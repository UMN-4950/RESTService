using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RESTService.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string GoogleId { get; set; }
        [Required]
        public string Email { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        
        // one to one relation with Location
        public Location CurrentLocation { get; set; }

        public ICollection<Notification> Notifications { get; set; }

        [ForeignKey("UserId")]
        public ICollection<User> Friends { get; set; }
    }
}