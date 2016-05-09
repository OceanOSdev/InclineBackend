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
    [Authorize]
    public class CardiovascularFitnessApiController : ApiController
    {
        private TodoListWebAppContext db = new TodoListWebAppContext();

        // GET: api/CardiovascularFitnessApi
        public IQueryable<CardiovascularFitness> GetCardios()
        {
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            return db.Cardios.Where(a => a.Owner == owner);
        }

        // GET: api/CardiovascularFitnessApi/5
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