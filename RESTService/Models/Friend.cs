
namespace RESTService.Models
{
    public class Friend
    {
        public int FriendId { get; set; }

        // ForeignKey
        public int UserId { get; set; }
        public User User { get; set; }
    }
}