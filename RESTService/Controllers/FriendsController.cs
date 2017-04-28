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
        private UsersController uc = new UsersController();

        // GET: api/Friends
        public IEnumerable<Friend> GetFriends()
        {
            return db.Friends.ToList<Friend>();
        }

        [Route("api/friends/getfriendslist/{userID}/")]
        [HttpPost]
        public IHttpActionResult GetFriendsList(int userID)
        {
            // Retrieve querying user
            User currentUser = uc.FindUser(userID);

            // Filter out friends without status "Friend"
            IEnumerable<Friend> friends = currentUser.Friends.Where(x => x != null && x.Status.Equals("Friend"));

            // Check if any registered friends
            if (!friends.Any())
            {
                return Content(HttpStatusCode.BadRequest, "No friends found.");
            }

            // TODO: Add current location reference
            // Retrieve current user's location

            // Calculate distance between user and friends

            // Retrieve profile picture

            // Construct reply object
            var reply = new List<Tuple<String, double, String>>(); // name, distance, id
            foreach(Friend f in friends)
            {
                String name = f.User.GivenName + " " + f.User.FamilyName;
                //double distance = distance(double lat1, double lon1, double lat2, double lon2); // TODO: Figure out utility class structure and implement
                Random random = new Random();
                double distance = random.NextDouble(); // Temporary

                reply.Add(new Tuple<String, double, String>(name, distance, f.User.GoogleId));
            }

            // Return result
            return Ok(reply);
        }

        [Route("api/friends/AddFriend/{userID}/{friendID}")]
        [HttpPost]
        public IHttpActionResult AddFriend(int userID, int friendID)
        {
            // Retrieve querying user and check if users already friends
            User currentUser = uc.FindUser(userID);

            if(currentUser.Friends.Where(x => x.UserId == friendID).SingleOrDefault() != null)
            {
                return Content(HttpStatusCode.Conflict, "Users already friends.");
            }

            // Update both Friends lists
            User friend = uc.FindUser(friendID);

            var newFriend1 = new Friend
            {
                Status = "Friend",
                UserId = friendID,
                User = friend
            };
            currentUser.Friends.Add(newFriend1);

            var newFriend2 = new Friend
            {
                Status = "Friend",
                UserId = userID,
                User = currentUser
            };
            friend.Friends.Add(newFriend2);

            // Update database tables
            var postFriend1 = PostFriend(newFriend1);
            var postFriend2 = PostFriend(newFriend2);

            return Ok();
        }

        [Route("api/friends/RemoveFriend/{userID}/{friendID}")]
        [HttpPost]
        public IHttpActionResult RemoveFriend(int userID, int friendID)
        {
            // Retrieve querying user and check if users are indeed friends
            User currentUser = uc.FindUser(userID);
            Friend friendToBeRemoved = currentUser.Friends.Where(x => x.UserId == friendID).SingleOrDefault();
            if (friendToBeRemoved == null)
            {
                return Content(HttpStatusCode.Conflict, "Users already not friends.");
            }

            // Double check both friend lists confirm friends
            User friend = uc.FindUser(friendID);
            Friend userToBeRemoved = friend.Friends.Where(x => x.UserId == userID).SingleOrDefault();
            if (userToBeRemoved == null)
            {
                return Content(HttpStatusCode.Conflict, "Friendship was not mutual.");
            }

            // Update user friend lists
            currentUser.Friends.Remove(friendToBeRemoved);
            friend.Friends.Remove(userToBeRemoved);

            // Update database tables
            var deleteFriend1 = DeleteFriend(friendToBeRemoved.Id);
            var deleteFriend2 = DeleteFriend(userToBeRemoved.Id);

            return Ok();
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