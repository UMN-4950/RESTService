using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RESTService.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string GoogleId { get; set; }
        [Required]
        public string Email { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        
        public virtual List<Location> Locations { get; set; }

        public virtual List<Notification> Notifications { get; set; }

        public virtual List<Friend> Friends { get; set; }
    }
}