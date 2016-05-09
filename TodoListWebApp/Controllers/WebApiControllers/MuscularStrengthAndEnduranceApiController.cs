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
    public class MuscularStrengthAndEnduranceApiController : ApiController
    {
        private TodoListWebAppContext db = new TodoListWebAppContext();

        // GET: api/MuscularStrengthAndEnduranceApi
        public IQueryable<MuscularStrengthAndEndurance> GetMuscularStrengthsAndEndurances()
        {
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            return db.MuscularStrengthsAndEndurances.Where(a => a.Owner == owner);
        }

        // GET: api/MuscularStrengthAndEnduranceApi/5
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