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
    [HostAuthentication("AADBearer")]
    [Authorize]
    public class MaxLegPressApiController : ApiController
    {
        private TodoListWebAppContext db = new TodoListWebAppContext();

        // GET: api/MaxLegPressApi
        public IQueryable<MaxLegPressModel> GetMaxLegPresses()
        {
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            return db.MaxLegPresses.Where(a => a.Owner == owner);
        }

        // GET: api/MaxLegPressApi/5
        [ResponseType(typeof(MaxLegPressModel))]
        public IHttpActionResult GetMaxLegPressModel(int id)
        {
            MaxLegPressModel maxLegPressModel = db.MaxLegPresses.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (maxLegPressModel == null || maxLegPressModel.Owner != owner)
            {
                return NotFound();
            }

            return Ok(maxLegPressModel);
        }

        // PUT: api/MaxLegPressApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMaxLegPressModel(int id, MaxLegPressModel maxLegPressModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != maxLegPressModel.ID)
            {
                return BadRequest();
            }

            db.Entry(maxLegPressModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaxLegPressModelExists(id))
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

        // POST: api/MaxLegPressApi
        [ResponseType(typeof(MaxLegPressModel))]
        public IHttpActionResult PostMaxLegPressModel(MaxLegPressModel maxLegPressModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            maxLegPressModel.Owner = owner;
            maxLegPressModel.Logged = DateTime.UtcNow;
            db.MaxLegPresses.Add(maxLegPressModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = maxLegPressModel.ID }, maxLegPressModel);
        }

        // DELETE: api/MaxLegPressApi/5
        [ResponseType(typeof(MaxLegPressModel))]
        public IHttpActionResult DeleteMaxLegPressModel(int id)
        {
            MaxLegPressModel maxLegPressModel = db.MaxLegPresses.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (maxLegPressModel == null || maxLegPressModel.Owner != owner)
            {
                return NotFound();
            }

            db.MaxLegPresses.Remove(maxLegPressModel);
            db.SaveChanges();

            return Ok(maxLegPressModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MaxLegPressModelExists(int id)
        {
            return db.MaxLegPresses.Count(e => e.ID == id) > 0;
        }
    }
}