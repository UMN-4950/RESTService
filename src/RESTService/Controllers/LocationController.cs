using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RESTService.Models;

namespace RESTService.Controllers
{
    [Route("api/[controller]")]
    public class LocationController : Controller
    {
        public ILocationRepository LocationItems { get; set; }

        public LocationController(ILocationRepository locationItems)
        {
            this.LocationItems = locationItems;
        }

        // GET /api/location
        [HttpGet]
        public IEnumerable<Location> GetAll()
        {
            return LocationItems.GetAll();
        }

        // GET /api/location/id
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            Location location = LocationItems.Find(id);
            if (location == null)
            {
                return NotFound();
            }
            return new ObjectResult(location);
        }
    }
}
