using System.Collections.Generic;

namespace RESTService.Models
{
    public interface ILocationRepository
    {
        /// <summary>
        /// repository for our data layer implementing basic CRUD operations
        /// Add:        adding a new location
        /// GetAll:     get all inserted locations on db
        /// Update:     update the current location
        /// Find:       find a location using its key
        /// Remove:     remove a location using its key
        /// </summary>

        void Add(Location location);
        IEnumerable<Location> GetAll();
        void Update(Location locatoin);
        Location Find(string key);
        Location Remove(string key);
    }
}
