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
using System.Collections.Specialized;
using System.Net.Http.Headers;

namespace RESTService.Controllers
{
    public class UsersController : ApiController
    {
        private RESTServiceContext db = new RESTServiceContext();

        // GET: api/Users
        public IEnumerable<User> GetUsers()
        {
            return db.Users.ToList<User>();
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [Route("api/users/getid/{googleID}")]
        public async Task<IHttpActionResult> GetID(string googleID)
        {
            // Assumption: default value is 0
            int id = db.Users.Where(x => x.GoogleId == googleID).Select(s => s.Id).SingleOrDefault();
            return Ok(id);
        }

        [Route("api/users/dropcookie/{userID}")]
        public HttpResponseMessage DropCookie(string userID)
        {
            //validate userId

            var response = Request.CreateResponse(HttpStatusCode.Created);

            var nv = new NameValueCollection();
            //set userid
            nv["uid"] = userID;
            //should generate a token for that user/session that is encrypted and known only on server.
            nv["token"] = "1234567890";
            var cookie = new CookieHeaderValue("bpoc", nv);
            cookie.Path = "/";
            //cookie.Secure = true;
            cookie.Domain = Request.RequestUri.Host.Contains("localhost") ? null : Request.RequestUri.Host;
            response.Headers.AddCookies(new CookieHeaderValue[] { cookie });

            return response;
        }

        [Route("api/users/checkcookie")]
        public HttpResponseMessage CheckCookie()
        {
        
            //all this code should really be in a shared class or at the least a baseController that others inherit from
            //could use a  messageHandler

            string userId = "";
            string sessionToken = "";
         

            CookieHeaderValue cookie = Request.Headers.GetCookies("bpoc").FirstOrDefault();
            if (cookie != null)
            {
                CookieState cookieState = cookie["bpoc"];

                userId = cookieState["uid"];
                //validate token per userId
                sessionToken = cookieState["token"];
               }
            return Request.CreateResponse(HttpStatusCode.OK,sessionToken);
        }

        [Route("api/users/namesearch/{name}")]
        public async Task<IHttpActionResult> NameSearch(int userID, string queryString)
        {
            //  TODO: Remove userID and instead use cookie

            // Split input search string and search on all passed names
            queryString = queryString.ToLower();
            var searchTokens = queryString.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
            var matches = new List<User>();
            foreach (var searchTerm in searchTokens)
            {
                List<User> currentMatches = db.Users.Where(
                    x => x.GivenName.ToLower().Contains(searchTerm) ||
                         x.FamilyName.ToLower().Contains(searchTerm)
                ).ToList();

                matches.AddRange(currentMatches);
            }

            // Retrieve querying user's friend list
            // TODO: Only retrieve friends list of current user. How? And below error not handled
            User currentUser = db.Users.Where(x => x.Id == userID).Single(); // Throws an error if user not found
            List<Friend> friends = currentUser.Friends;

            // Iterate through results, add friend status, and then construct reply object
            var reply = new List<Tuple<int, String, String>>(); // id, name, friendStatus
            foreach(User u in matches)
            {
                // Retrieve friend status
                String status = friends.Where(x => x.UserId == u.Id).Select(s => s.Status).SingleOrDefault();
                if (status == null){ status = "NotFriend"; }

                String name = u.GivenName + " " + u.FamilyName;

                reply.Add(new Tuple<int, String, String>(u.Id, name, status));
                var y = new UserDTO { Email = u.Email, Id = u.Id };
                y = u.ToUserDTO();
                return Ok(y);
            }

            return Ok(reply);

            // TODO: Limit function to only return first 5, and then wait for if user requests more
 
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        [ResponseType(typeof(Location))]
        [Route("api/users/userlocation")]
        public async Task<IHttpActionResult> PostUserLocation(Location location)
        {
            //verify our in data
            //look up user
            //authenticated
            //update location
            //return status
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (location != null && location.UserId <=0) return BadRequest(ModelState);

            //db.Locations.Add(location);
            return Ok();
           // return CreatedAtRoute("DefaultApi", StatusCode.Ok);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> DeleteUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}