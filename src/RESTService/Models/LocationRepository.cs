using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace RESTService.Models
{
    public class LocationRepository : ILocationRepository
    {
        /// <summary>
        /// mapping a location to a string key
        /// using a thread-safe collection
        /// todo: add linq queries from db
        /// </summary>
        private static ConcurrentDictionary<string, Location> _locations = new ConcurrentDictionary<string, Location>();

        public void Add(Location location)
        {
            location.Key = Guid.NewGuid().ToString();
            _locations.TryAdd(location.Key, location);
        }

        public IEnumerable<Location> GetAll()
        {
            return _locations.Values;
        }

        public void Update(Location locatoin)
        {
            throw new System.NotImplementedException();
        }

        public Location Find(string key)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(string key)
        {
            throw new System.NotImplementedException();
        }
    }
}
