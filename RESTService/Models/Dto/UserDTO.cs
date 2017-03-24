using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RESTService.Models
{
    public class UserDTO
    {
        public int Id { get; set; }
        [Required]
        public string GoogleId { get; set; }
        [Required]
        public string Email { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        }
}