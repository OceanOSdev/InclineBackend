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
    public class TrunkLiftApiController : ApiController
    {
        private TodoListWebAppContext db = new TodoListWebAppContext();

        // GET: api/TrunkLiftApi
        public IQueryable<TrunkLiftModel> GetTrunkLifts()
        {
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            return db.TrunkLifts.Where(a => a.Owner == owner);
        }

        // GET: api/TrunkLiftApi/5
        [ResponseType(typeof(TrunkLiftModel))]
        public IHttpActionResult GetTrunkLiftModel(int id)
        {
            TrunkLiftModel trunkLiftModel = db.TrunkLifts.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (trunkLiftModel == null || trunkLiftModel.Owner != owner)
            {
                return NotFound();
            }

            return Ok(trunkLiftModel);
        }

        // PUT: api/TrunkLiftApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTrunkLiftModel(int id, TrunkLiftModel trunkLiftModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trunkLiftModel.ID)
            {
                return BadRequest();
            }

            db.Entry(trunkLiftModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrunkLiftModelExists(id))
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

        // POST: api/TrunkLiftApi
        [ResponseType(typeof(TrunkLiftModel))]
        public IHttpActionResult PostTrunkLiftModel(TrunkLiftModel trunkLiftModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            trunkLiftModel.Owner = owner;
            trunkLiftModel.Logged = DateTime.UtcNow;
            db.TrunkLifts.Add(trunkLiftModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = trunkLiftModel.ID }, trunkLiftModel);
        }

        // DELETE: api/TrunkLiftApi/5
        [ResponseType(typeof(TrunkLiftModel))]
        public IHttpActionResult DeleteTrunkLiftModel(int id)
        {
            TrunkLiftModel trunkLiftModel = db.TrunkLifts.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (trunkLiftModel == null || trunkLiftModel.Owner != owner)
            {
                return NotFound();
            }

            db.TrunkLifts.Remove(trunkLiftModel);
            db.SaveChanges();

            return Ok(trunkLiftModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TrunkLiftModelExists(int id)
        {
            return db.TrunkLifts.Count(e => e.ID == id) > 0;
        }
    }
}