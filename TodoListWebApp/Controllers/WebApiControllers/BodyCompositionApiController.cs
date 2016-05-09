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
    public class BodyCompositionApiController : ApiController
    {
        private TodoListWebAppContext db = new TodoListWebAppContext();

        // GET: api/BodyCompositionApi
        public IQueryable<BodyComposition> GetBodyComps()
        {
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            return db.BodyComps.Where(a => a.Owner == owner);
        }

        // GET: api/BodyCompositionApi/5
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
        [ResponseType(typeof(BodyComposition))]
        public IHttpActionResult PostBodyComposition(BodyComposition bodyComposition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            bodyComposition.Owner = owner;
            bodyComposition.Logged = DateTime.Now;
            db.BodyComps.Add(bodyComposition);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = bodyComposition.ID }, bodyComposition);
        }

        // DELETE: api/BodyCompositionApi/5
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