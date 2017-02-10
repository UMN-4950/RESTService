using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RESTService.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string GoogleId { get; set; }
        [Required]
        public string Email { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        
        public ICollection<Location> Locations { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }

        public virtual ICollection<Friend> Friends { get; set; }
    }
}