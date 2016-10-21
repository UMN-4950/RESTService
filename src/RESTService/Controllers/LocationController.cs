using System;
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
            LocationItems = locationItems;
        }

        // GET /api/location
        [HttpGet]
        public IEnumerable<Location> GetAll()
        {
            return LocationItems.GetAll();
        }

        // GET /api/location/id
        // uses GetLocation as rout name in HttpPost
        [HttpGet("{id}", Name = "GetLocation")]
        public IActionResult GetById(string id)
        {
            var location = LocationItems.Find(id);
            if (location == null)
            {
                return NotFound();
            }
            return new ObjectResult(location);
        }

        // POST /api/location - replacing the whole location
        [HttpPost]
        public IActionResult Create([FromBody] Location location)
        {
            if (location == null)
            {
                return BadRequest();
            }
            LocationItems.Add(location);
            // return the rout to new created object in header - returns 201
            return CreatedAtRoute("GetLocation", new { id = location.Key }, location);
        }

        // PUT /api/location
        // key included in body
        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Location location)
        {
            if (location == null || location.Key != id)
            {
                return BadRequest();
            }

            // check if we have this object
            if (LocationItems.Find(id) == null)
            {
                return NotFound();
            }

            LocationItems.Update(location);

            // returns 204
            return new NoContentResult();
        }

        // PATCH /api/location - updating a part of location
        // key not included in body
        [HttpPatch("{id}")]
        public IActionResult Update([FromBody] Location location, string id)
        {
            if (location == null)
            {
                return BadRequest();
            }

            // check if we have this object
            var item = LocationItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            location.Key = item.Key;

            LocationItems.Update(location);

            // returns 204
            return new NoContentResult();
        }

        //  DELETE /api/location
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (LocationItems.Find(id) == null)
            {
                return NotFound();
            }

            LocationItems.Remove(id);
            
            return new NoContentResult();
        }
    }
}
