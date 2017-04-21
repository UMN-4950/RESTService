using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RESTService.Models;

namespace RESTService.Controllers
{
    public class LocationsController : ApiController
    {
        private RESTServiceContext db = new RESTServiceContext();

        // POST: api/Locations/
        // http://...//Locations/postloation/16/2233/4546
        [Route("api/locations/postlocation/{id:int}/{lat}/{lon}")]
        public IHttpActionResult UpdateUserLocation(string lat, string lon, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = db.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var userId = user.Id;

            var newLocation = new Location
            {
                Latitude = Convert.ToDouble(lat),
                Longitude = Convert.ToDouble(lon),
                User = user,
                UserId = user.Id,
                Time = DateTime.Now
            };

            var postLocation = PostLocation(newLocation);

            return Ok();
        }

        // GET
        [Route("api/locations/getall/{id:int}")]
        public IHttpActionResult GetAllUserLocations(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = db.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user.Locations);
        }

        // GET
        [Route("api/locations/getlast/{id:int}")]
        public IHttpActionResult GetLastUserLocations(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = db.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var lastLocation = user.Locations.LastOrDefault();
            if (lastLocation == null)
            {
                return NotFound();
            }

            var output = new
            {
                lat = lastLocation.Latitude,
                lon = lastLocation.Longitude
            };

            return Ok(output);
        }

        // GET: api/Locations
        public IEnumerable<Location> GetLocations()
        {
            return db.Locations.ToList<Location>();
        }

        // GET: api/Locations/5
        [ResponseType(typeof(Location))]
        public async Task<IHttpActionResult> GetLocation(int id)
        {
            Location location = await db.Locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }

        // PUT: api/Locations/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLocation(int id, Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != location.Id)
            {
                return BadRequest();
            }

            db.Entry(location).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Locations
        [ResponseType(typeof(Location))]
        public IHttpActionResult PostLocation(Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Locations.Add(location);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = location.Id }, location);
        }

        // DELETE: api/Locations/5
        [ResponseType(typeof(Location))]
        public async Task<IHttpActionResult> DeleteLocation(int id)
        {
            Location location = await db.Locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            db.Locations.Remove(location);
            await db.SaveChangesAsync();

            return Ok(location);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LocationExists(int id)
        {
            return db.Locations.Count(e => e.Id == id) > 0;
        }
    }
}