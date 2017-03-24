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
    public class FriendsController : ApiController
    {
        private RESTServiceContext db = new RESTServiceContext();

        // GET: api/Friends
        public IEnumerable<Friend> GetFriends()
        {
            return db.Friends.ToList<Friend>();
        }

        [Route("api/friends/distance")]
        public HttpResponseMessage GetFriendsTemp()
        {
            List<dynamic> data = new List<dynamic>();
            data.Add(new { name = "Jack", distance = 1.3, id = 1 });

            data.Add(new { name = "Marcio", distance = 0.3, id = 2 });

            data.Add(new { name = "Sophita", distance = 2.3, id = 3 });
            return Request.CreateResponse(HttpStatusCode.OK, data);


        }

        // GET: api/Friends/5
        [ResponseType(typeof(Friend))]
        public async Task<IHttpActionResult> GetFriend(int id)
        {
            Friend friend = await db.Friends.FindAsync(id);
            if (friend == null)
            {
                return NotFound();
            }

            return Ok(friend);
        }

        // PUT: api/Friends/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFriend(int id, Friend friend)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != friend.Id)
            {
                return BadRequest();
            }

            db.Entry(friend).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FriendExists(id))
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

        // POST: api/Friends
        [ResponseType(typeof(Friend))]
        public async Task<IHttpActionResult> PostFriend(Friend friend)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Friends.Add(friend);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = friend.Id }, friend);
        }

        // DELETE: api/Friends/5
        [ResponseType(typeof(Friend))]
        public async Task<IHttpActionResult> DeleteFriend(int id)
        {
            Friend friend = await db.Friends.FindAsync(id);
            if (friend == null)
            {
                return NotFound();
            }

            db.Friends.Remove(friend);
            await db.SaveChangesAsync();

            return Ok(friend);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FriendExists(int id)
        {
            return db.Friends.Count(e => e.Id == id) > 0;
        }
    }
}