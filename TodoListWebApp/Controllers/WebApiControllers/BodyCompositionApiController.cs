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
    /// Handles GETs, DELETEs, PUTs, and POSTs for Body Composition Data
    /// </summary>
    [Authorize]
    public class BodyCompositionApiController : ApiController
    {
        private TodoListWebAppContext db = new TodoListWebAppContext();

        // GET: api/BodyCompositionApi
        /// <summary>
        /// Get all of the user's Body Composition data.
        /// </summary>
        /// <returns>An IQueryable of BodyComposition</returns>
        public IQueryable<BodyComposition> GetBodyComps()
        {
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            return db.BodyComps.Where(a => a.Owner == owner);
        }

        // GET: api/BodyCompositionApi/5
        /// <summary>
        /// Get's a specific entry of the user's body composition data.
        /// </summary>
        /// <param name="id">The ID field of the entry you want.</param>
        /// <returns>A specific entry of the user's body composition data.</returns>
        [ResponseType(typeof(BodyComposition))]
        public IHttpActionResult GetBodyComposition(int id)
        {
            BodyComposition bodyComposition = db.BodyComps.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (bodyComposition == null || bodyComposition.Owner != owner)
            {
                return NotFound();
            }

            return Ok(bodyComposition);
        }

        // PUT: api/BodyCompositionApi/5
        /// <summary>
        /// Replaces the data at a specific entry.
        /// </summary>
        /// <param name="id">The ID of the entry to change.</param>
        /// <param name="bodyComposition">The new data.</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBodyComposition(int id, BodyComposition bodyComposition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bodyComposition.ID)
            {
                return BadRequest();
            }

            db.Entry(bodyComposition).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BodyCompositionExists(id))
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

        // POST: api/BodyCompositionApi
        /// <summary>
        /// Adds the Body Composition data entry to the list.
        /// </summary>
        /// <param name="bodyComposition">The body composition data.</param>
        /// <returns>The data that was sent.</returns>
        [ResponseType(typeof(BodyComposition))]
        public IHttpActionResult PostBodyComposition(BodyComposition bodyComposition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            bodyComposition.Owner = owner;
            bodyComposition.Logged = DateTime.UtcNow;
            db.BodyComps.Add(bodyComposition);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = bodyComposition.ID }, bodyComposition);
        }

        // DELETE: api/BodyCompositionApi/5
        /// <summary>
        /// Deletes a body composition entry.
        /// </summary>
        /// <param name="id">The ID of the entry.</param>
        /// <returns>The data that was removed.</returns>
        [ResponseType(typeof(BodyComposition))]
        public IHttpActionResult DeleteBodyComposition(int id)
        {
            BodyComposition bodyComposition = db.BodyComps.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (bodyComposition == null || bodyComposition.Owner != owner)
            {
                return NotFound();
            }

            db.BodyComps.Remove(bodyComposition);
            db.SaveChanges();

            return Ok(bodyComposition);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BodyCompositionExists(int id)
        {
            return db.BodyComps.Count(e => e.ID == id) > 0;
        }
    }
}