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
    /// Handles GETs, DELETEs, PUTs, and POSTs for Cardiovascular Fitness Data.
    /// </summary>
    [Authorize]
    public class CardiovascularFitnessApiController : ApiController
    {
        private TodoListWebAppContext db = new TodoListWebAppContext();

        // GET: api/CardiovascularFitnessApi
        /// <summary>
        /// Gets all of the user's cardiovascular fitness data.
        /// </summary>
        /// <returns>An IQueryable of the users cardiovascular fitness data.</returns>
        public IQueryable<CardiovascularFitness> GetCardios()
        {
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            return db.Cardios.Where(a => a.Owner == owner);
        }

        // GET: api/CardiovascularFitnessApi/5
        /// <summary>
        /// Gets a specific entry of the user's cardiovascular fitness data.
        /// </summary>
        /// <param name="id">The ID of the entry.</param>
        /// <returns>The specific data entry.</returns>
        [ResponseType(typeof(CardiovascularFitness))]
        public IHttpActionResult GetCardiovascularFitness(int id)
        {
            CardiovascularFitness cardiovascularFitness = db.Cardios.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (cardiovascularFitness == null || cardiovascularFitness.Owner != owner)
            {
                return NotFound();
            }

            return Ok(cardiovascularFitness);
        }

        // PUT: api/CardiovascularFitnessApi/5
        /// <summary>
        /// Replaces the data at a specific entry.
        /// </summary>
        /// <param name="id">The ID of the entry to replace.</param>
        /// <param name="cardiovascularFitness">The new data.</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCardiovascularFitness(int id, CardiovascularFitness cardiovascularFitness)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cardiovascularFitness.ID)
            {
                return BadRequest();
            }

            db.Entry(cardiovascularFitness).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardiovascularFitnessExists(id))
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

        // POST: api/CardiovascularFitnessApi
        /// <summary>
        /// Adds the cardiovascular fitness data entry to the list.
        /// </summary>
        /// <param name="cardiovascularFitness">The data to add.</param>
        /// <returns></returns>
        [ResponseType(typeof(CardiovascularFitness))]
        public IHttpActionResult PostCardiovascularFitness(CardiovascularFitness cardiovascularFitness)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            cardiovascularFitness.Owner = owner;
            cardiovascularFitness.Logged = DateTime.UtcNow;
            db.Cardios.Add(cardiovascularFitness);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cardiovascularFitness.ID }, cardiovascularFitness);
        }

        // DELETE: api/CardiovascularFitnessApi/5
        /// <summary>
        /// Deletes a Cardiovascular Fitness data entry.
        /// </summary>
        /// <param name="id">The ID of the data to delete.</param>
        /// <returns></returns>
        [ResponseType(typeof(CardiovascularFitness))]
        public IHttpActionResult DeleteCardiovascularFitness(int id)
        {
            CardiovascularFitness cardiovascularFitness = db.Cardios.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (cardiovascularFitness == null || cardiovascularFitness.Owner != owner)
            {
                return NotFound();
            }

            db.Cardios.Remove(cardiovascularFitness);
            db.SaveChanges();

            return Ok(cardiovascularFitness);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CardiovascularFitnessExists(int id)
        {
            return db.Cardios.Count(e => e.ID == id) > 0;
        }
    }
}