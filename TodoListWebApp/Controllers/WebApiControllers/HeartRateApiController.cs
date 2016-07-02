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
    public class HeartRateApiController : ApiController
    {
        private TodoListWebAppContext db = new TodoListWebAppContext();

        // GET: api/HeartRateApi
        public IQueryable<HeartRateModel> GetHeartRates()
        {
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            return db.HeartRates.Where(a => a.Owner == owner).OrderByDescending(x => x.Logged);
        }

        // GET: api/HeartRateApi/5
        [ResponseType(typeof(HeartRateModel))]
        public IHttpActionResult GetHeartRateModel(int id)
        {
            HeartRateModel heartRateModel = db.HeartRates.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (heartRateModel == null || heartRateModel.Owner != owner)
            {
                return NotFound();
            }

            return Ok(heartRateModel);
        }

        // PUT: api/HeartRateApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHeartRateModel(int id, HeartRateModel heartRateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != heartRateModel.ID)
            {
                return BadRequest();
            }

            db.Entry(heartRateModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HeartRateModelExists(id))
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

        // POST: api/HeartRateApi
        [ResponseType(typeof(HeartRateModel))]
        public IHttpActionResult PostHeartRateModel(HeartRateModel heartRateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            heartRateModel.Owner = owner;
            db.HeartRates.Add(heartRateModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = heartRateModel.ID }, heartRateModel);
        }

        // DELETE: api/HeartRateApi/5
        [ResponseType(typeof(HeartRateModel))]
        public IHttpActionResult DeleteHeartRateModel(int id)
        {
            HeartRateModel heartRateModel = db.HeartRates.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (heartRateModel == null || heartRateModel.Owner != owner)
            {
                return NotFound();
            }

            db.HeartRates.Remove(heartRateModel);
            db.SaveChanges();

            return Ok(heartRateModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HeartRateModelExists(int id)
        {
            return db.HeartRates.Count(e => e.ID == id) > 0;
        }
    }
}