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
    public class PullUpApiController : ApiController
    {
        private TodoListWebAppContext db = new TodoListWebAppContext();

        // GET: api/PullUpApi
        public IQueryable<PullUpModel> GetPullUps()
        {
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            return db.PullUps.Where(a => a.Owner == owner).OrderByDescending(x => x.Logged);
        }

        // GET: api/PullUpApi/5
        [ResponseType(typeof(PullUpModel))]
        public IHttpActionResult GetPullUpModel(int id)
        {
            PullUpModel pullUpModel = db.PullUps.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (pullUpModel == null || pullUpModel.Owner != owner)
            {
                return NotFound();
            }

            return Ok(pullUpModel);
        }

        // PUT: api/PullUpApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPullUpModel(int id, PullUpModel pullUpModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pullUpModel.ID)
            {
                return BadRequest();
            }

            db.Entry(pullUpModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PullUpModelExists(id))
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

        // POST: api/PullUpApi
        [ResponseType(typeof(PullUpModel))]
        public IHttpActionResult PostPullUpModel(PullUpModel pullUpModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            pullUpModel.Owner = owner;
            pullUpModel.Logged = DateTime.UtcNow;
            db.PullUps.Add(pullUpModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pullUpModel.ID }, pullUpModel);
        }

        // DELETE: api/PullUpApi/5
        [ResponseType(typeof(PullUpModel))]
        public IHttpActionResult DeletePullUpModel(int id)
        {
            PullUpModel pullUpModel = db.PullUps.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (pullUpModel == null || pullUpModel.Owner != owner)
            {
                return NotFound();
            }

            db.PullUps.Remove(pullUpModel);
            db.SaveChanges();

            return Ok(pullUpModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PullUpModelExists(int id)
        {
            return db.PullUps.Count(e => e.ID == id) > 0;
        }
    }
}