using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;
using TodoListWebApp.DAL;
using TodoListWebApp.Models;

namespace TodoListWebApp.Controllers
{
    /// <summary>
    ///  Handles GETs, DELETEs, PUTs, and POSTs for Flexibility Data.
    /// </summary>
    [Authorize]    
    public class FlexibilityApiController : ApiController
    {
        private TodoListWebAppContext db = new TodoListWebAppContext();

        // GET: api/FlexibilityApi
        /// <summary>
        /// Gets all of the users Flexibility data.
        /// </summary>
        /// <returns></returns>
        public IQueryable<Flexibility> GetFlexibilities()
        {
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            return db.Flexibilities.Where(a => a.Owner == owner);
        }

        // GET: api/FlexibilityApi/5
        /// <summary>
        /// Gets a specific data entry.
        /// </summary>
        /// <param name="id">The ID of the data entry to get.</param>
        /// <returns></returns>
        [ResponseType(typeof(Flexibility))]
        public IHttpActionResult GetFlexibility(int id)
        {
            Flexibility flexibility = db.Flexibilities.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (flexibility == null || flexibility.Owner != owner)
            {
                return NotFound();
            }

            return Ok(flexibility);
        }

        // PUT: api/FlexibilityApi/5
        /// <summary>
        /// Replaces a specific entry of the flexibility data.
        /// </summary>
        /// <param name="id">The ID of entry to replace.</param>
        /// <param name="flexibility">The new data.</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFlexibility(int id, Flexibility flexibility)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != flexibility.ID)
            {
                return BadRequest();
            }

            db.Entry(flexibility).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlexibilityExists(id))
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

        // POST: api/FlexibilityApi
        /// <summary>
        /// Adds new flexibility data to the list.
        /// </summary>
        /// <param name="flexibility">The flexibility data to post.</param>
        /// <returns></returns>
        [ResponseType(typeof(Flexibility))]
        public IHttpActionResult PostFlexibility(Flexibility flexibility)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            flexibility.Owner = owner;
            flexibility.Logged = DateTime.UtcNow;
            db.Flexibilities.Add(flexibility);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = flexibility.ID }, flexibility);
        }

        // DELETE: api/FlexibilityApi/5
        /// <summary>
        /// Removes a specific flexibility data entry.
        /// </summary>
        /// <param name="id">The entry to remove.</param>
        /// <returns></returns>
        [ResponseType(typeof(Flexibility))]
        public IHttpActionResult DeleteFlexibility(int id)
        {
            Flexibility flexibility = db.Flexibilities.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (flexibility == null || flexibility.Owner != owner)
            {
                return NotFound();
            }

            db.Flexibilities.Remove(flexibility);
            db.SaveChanges();

            return Ok(flexibility);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FlexibilityExists(int id)
        {
            return db.Flexibilities.Count(e => e.ID == id) > 0;
        }
    }
}