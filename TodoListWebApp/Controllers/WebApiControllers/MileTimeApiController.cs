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
    public class MileTimeApiController : ApiController
    {
        private TodoListWebAppContext db = new TodoListWebAppContext();

        // GET: api/MileTimeApi
        public IQueryable<MileTimeModel> GetMileTimes()
        {
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            return db.MileTimes.Where(a => a.Owner == owner);
        }

        // GET: api/MileTimeApi/5
        [ResponseType(typeof(MileTimeModel))]
        public IHttpActionResult GetMileTimeModel(int id)
        {
            MileTimeModel mileTimeModel = db.MileTimes.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (mileTimeModel == null || mileTimeModel.Owner != owner)
            {
                return NotFound();
            }

            return Ok(mileTimeModel);
        }

        // PUT: api/MileTimeApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMileTimeModel(int id, MileTimeModel mileTimeModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mileTimeModel.ID)
            {
                return BadRequest();
            }

            db.Entry(mileTimeModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MileTimeModelExists(id))
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

        // POST: api/MileTimeApi
        [ResponseType(typeof(MileTimeModel))]
        public IHttpActionResult PostMileTimeModel(MileTimeModel mileTimeModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            mileTimeModel.Owner = owner;
            mileTimeModel.Logged = DateTime.UtcNow;
            db.MileTimes.Add(mileTimeModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = mileTimeModel.ID }, mileTimeModel);
        }

        // DELETE: api/MileTimeApi/5
        [ResponseType(typeof(MileTimeModel))]
        public IHttpActionResult DeleteMileTimeModel(int id)
        {
            MileTimeModel mileTimeModel = db.MileTimes.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (mileTimeModel == null || mileTimeModel.Owner != owner)
            {
                return NotFound();
            }

            db.MileTimes.Remove(mileTimeModel);
            db.SaveChanges();

            return Ok(mileTimeModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MileTimeModelExists(int id)
        {
            return db.MileTimes.Count(e => e.ID == id) > 0;
        }
    }
}