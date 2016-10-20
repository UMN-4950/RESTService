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

        public LocationRepository()
        {
            // dummy location
            Add(new Location
            {
                Latitude = 1234,
                Longitude = 6789
            });

        }

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
            _locations[locatoin.Key] = locatoin;

            // can be also done via
            // _locations.TryUpdate(locatoin.Key, locatoin, locatoin);
        }

        public Location Find(string key)
        {
            if (key == null)
            {
                return null;
            }

            Location location;
            _locations.TryGetValue(key, out location);
            return location;
        }

        public Location Remove(string key)
        {
            Location location;
            _locations.TryRemove(key, out location);
            return location;
        }
    }
}
