using System.Data.Entity;

namespace RESTService.Models
{
    public class RESTServiceContext : DbContext
    {
        public RESTServiceContext() : base("name=RESTServiceContext")
        {
        }

        public DbSet<Location> Locations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Notification> Notifications { get; set; }
    }
}
