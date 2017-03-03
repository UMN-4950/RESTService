using System.ComponentModel.DataAnnotations;

namespace RESTService.Models
{
    public class Friend
    {
        public int Id { get; set; }
        public string Status { get; set; }

        // ForeignKey
        public int UserId { get; set; }
        public User User { get; set; }
    }
}