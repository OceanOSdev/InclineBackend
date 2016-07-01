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
    public class HalfMileTimeApiController : ApiController
    {
        private TodoListWebAppContext db = new TodoListWebAppContext();

        // GET: api/HalfMileTimeApi
        public IQueryable<HalfMileTimeModel> GetHalfMileTimes()
        {
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            return db.HalfMileTimes.Where(item => item.Owner == owner).OrderByDescending(x => x.Logged);
        }

        // GET: api/HalfMileTimeApi/5
        [ResponseType(typeof(HalfMileTimeModel))]
        public IHttpActionResult GetHalfMileTimeModel(int id)
        {
            HalfMileTimeModel halfMileTimeModel = db.HalfMileTimes.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (halfMileTimeModel == null || halfMileTimeModel.Owner != owner)
            {
                return NotFound();
            }

            return Ok(halfMileTimeModel);
        }

        // PUT: api/HalfMileTimeApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHalfMileTimeModel(int id, HalfMileTimeModel halfMileTimeModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != halfMileTimeModel.ID)
            {
                return BadRequest();
            }

            db.Entry(halfMileTimeModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HalfMileTimeModelExists(id))
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

        // POST: api/HalfMileTimeApi
        [ResponseType(typeof(HalfMileTimeModel))]
        public IHttpActionResult PostHalfMileTimeModel(HalfMileTimeModel halfMileTimeModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            halfMileTimeModel.Owner = owner;
            halfMileTimeModel.Logged = DateTime.UtcNow;
            db.HalfMileTimes.Add(halfMileTimeModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = halfMileTimeModel.ID }, halfMileTimeModel);
        }

        // DELETE: api/HalfMileTimeApi/5
        [ResponseType(typeof(HalfMileTimeModel))]
        public IHttpActionResult DeleteHalfMileTimeModel(int id)
        {
            HalfMileTimeModel halfMileTimeModel = db.HalfMileTimes.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (halfMileTimeModel == null || halfMileTimeModel.Owner != owner)
            {
                return NotFound();
            }

            db.HalfMileTimes.Remove(halfMileTimeModel);
            db.SaveChanges();

            return Ok(halfMileTimeModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HalfMileTimeModelExists(int id)
        {
            return db.HalfMileTimes.Count(e => e.ID == id) > 0;
        }
    }
}