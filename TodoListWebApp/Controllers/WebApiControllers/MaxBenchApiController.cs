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
    public class MaxBenchApiController : ApiController
    {
        private TodoListWebAppContext db = new TodoListWebAppContext();

        // GET: api/MaxBenchApi
        public IQueryable<MaxBenchModel> GetMaxBenches()
        {
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            return db.MaxBenches.Where(a => a.Owner == owner).OrderByDescending(x => x.Logged);
        }

        // GET: api/MaxBenchApi/5
        [ResponseType(typeof(MaxBenchModel))]
        public IHttpActionResult GetMaxBenchModel(int id)
        {
            MaxBenchModel maxBenchModel = db.MaxBenches.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (maxBenchModel == null || maxBenchModel.Owner != owner)
            {
                return NotFound();
            }

            return Ok(maxBenchModel);
        }

        // PUT: api/MaxBenchApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMaxBenchModel(int id, MaxBenchModel maxBenchModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != maxBenchModel.ID)
            {
                return BadRequest();
            }

            db.Entry(maxBenchModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaxBenchModelExists(id))
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

        // POST: api/MaxBenchApi
        [ResponseType(typeof(MaxBenchModel))]
        public IHttpActionResult PostMaxBenchModel(MaxBenchModel maxBenchModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            maxBenchModel.Owner = owner;
            //maxBenchModel.Logged = DateTime.UtcNow; 
            db.MaxBenches.Add(maxBenchModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = maxBenchModel.ID }, maxBenchModel);
        }

        // DELETE: api/MaxBenchApi/5
        [ResponseType(typeof(MaxBenchModel))]
        public IHttpActionResult DeleteMaxBenchModel(int id)
        {
            MaxBenchModel maxBenchModel = db.MaxBenches.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (maxBenchModel == null || maxBenchModel.Owner != owner)
            {
                return NotFound();
            }

            db.MaxBenches.Remove(maxBenchModel);
            db.SaveChanges();

            return Ok(maxBenchModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MaxBenchModelExists(int id)
        {
            return db.MaxBenches.Count(e => e.ID == id) > 0;
        }
    }
}