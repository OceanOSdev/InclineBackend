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
    ///  Handles GETs, DELETEs, PUTs, and POSTs for Muscular Strength and Endurance Data.
    /// </summary>
    [Authorize]
    public class MuscularStrengthAndEnduranceApiController : ApiController
    {
        private TodoListWebAppContext db = new TodoListWebAppContext();

        // GET: api/MuscularStrengthAndEnduranceApi
        /// <summary>
        /// Gets all of the user's Muscular Strength and Endurance data.
        /// </summary>
        /// <returns></returns>
        public IQueryable<MuscularStrengthAndEndurance> GetMuscularStrengthsAndEndurances()
        {
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            return db.MuscularStrengthsAndEndurances.Where(a => a.Owner == owner);
        }

        // GET: api/MuscularStrengthAndEnduranceApi/5
        /// <summary>
        /// Gets a specific Muscular Strength and Endurance data entry.
        /// </summary>
        /// <param name="id">The ID of the Muscular Strength and Endurance data entry.</param>
        /// <returns></returns>
        [ResponseType(typeof(MuscularStrengthAndEndurance))]
        public IHttpActionResult GetMuscularStrengthAndEndurance(int id)
        {
            MuscularStrengthAndEndurance muscularStrengthAndEndurance = db.MuscularStrengthsAndEndurances.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (muscularStrengthAndEndurance == null || muscularStrengthAndEndurance.Owner != owner)
            {
                return NotFound();
            }

            return Ok(muscularStrengthAndEndurance);
        }

        // PUT: api/MuscularStrengthAndEnduranceApi/5
        /// <summary>
        /// Replaces a specific Muscular Strength and Endurance data entry.
        /// </summary>
        /// <param name="id">The ID of the Muscular Strength and Endurance data entry to replace.</param>
        /// <param name="muscularStrengthAndEndurance">The new Muscular Strength and Endurance data.</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMuscularStrengthAndEndurance(int id, MuscularStrengthAndEndurance muscularStrengthAndEndurance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != muscularStrengthAndEndurance.ID)
            {
                return BadRequest();
            }

            db.Entry(muscularStrengthAndEndurance).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MuscularStrengthAndEnduranceExists(id))
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

        // POST: api/MuscularStrengthAndEnduranceApi
        /// <summary>
        /// Adds a new Muscular Strength and Endurance data entry to the list.
        /// </summary>
        /// <param name="muscularStrengthAndEndurance">The Muscular Strength and Endurance data to add.</param>
        /// <returns></returns>
        [ResponseType(typeof(MuscularStrengthAndEndurance))]
        public IHttpActionResult PostMuscularStrengthAndEndurance(MuscularStrengthAndEndurance muscularStrengthAndEndurance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            muscularStrengthAndEndurance.Owner = owner;
            muscularStrengthAndEndurance.Logged = DateTime.UtcNow;
            db.MuscularStrengthsAndEndurances.Add(muscularStrengthAndEndurance);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = muscularStrengthAndEndurance.ID }, muscularStrengthAndEndurance);
        }

        // DELETE: api/MuscularStrengthAndEnduranceApi/5
        /// <summary>
        /// Removes a specific Muscular Strength and Endurance data entry.
        /// </summary>
        /// <param name="id">The ID of the data entry.</param>
        /// <returns></returns>
        [ResponseType(typeof(MuscularStrengthAndEndurance))]
        public IHttpActionResult DeleteMuscularStrengthAndEndurance(int id)
        {
            MuscularStrengthAndEndurance muscularStrengthAndEndurance = db.MuscularStrengthsAndEndurances.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (muscularStrengthAndEndurance == null || muscularStrengthAndEndurance.Owner != owner)
            {
                return NotFound();
            }

            db.MuscularStrengthsAndEndurances.Remove(muscularStrengthAndEndurance);
            db.SaveChanges();

            return Ok(muscularStrengthAndEndurance);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MuscularStrengthAndEnduranceExists(int id)
        {
            return db.MuscularStrengthsAndEndurances.Count(e => e.ID == id) > 0;
        }
    }
}