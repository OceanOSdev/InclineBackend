using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TodoListWebApp.DAL;
using TodoListWebApp.Models;

namespace TodoListWebApp.Controllers
{
    public class RightAnglePushUpApiController : ApiController
    {
        private TodoListWebAppContext db = new TodoListWebAppContext();

        // GET: api/RightAnglePushUpApi
        public IQueryable<RightAnglePushUpModel> GetRightAnglePushUps()
        {
            return db.RightAnglePushUps;
        }

        // GET: api/RightAnglePushUpApi/5
        [ResponseType(typeof(RightAnglePushUpModel))]
        public IHttpActionResult GetRightAnglePushUpModel(int id)
        {
            RightAnglePushUpModel rightAnglePushUpModel = db.RightAnglePushUps.Find(id);
            if (rightAnglePushUpModel == null)
            {
                return NotFound();
            }

            return Ok(rightAnglePushUpModel);
        }

        // PUT: api/RightAnglePushUpApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRightAnglePushUpModel(int id, RightAnglePushUpModel rightAnglePushUpModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rightAnglePushUpModel.ID)
            {
                return BadRequest();
            }

            db.Entry(rightAnglePushUpModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RightAnglePushUpModelExists(id))
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

        // POST: api/RightAnglePushUpApi
        [ResponseType(typeof(RightAnglePushUpModel))]
        public IHttpActionResult PostRightAnglePushUpModel(RightAnglePushUpModel rightAnglePushUpModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RightAnglePushUps.Add(rightAnglePushUpModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = rightAnglePushUpModel.ID }, rightAnglePushUpModel);
        }

        // DELETE: api/RightAnglePushUpApi/5
        [ResponseType(typeof(RightAnglePushUpModel))]
        public IHttpActionResult DeleteRightAnglePushUpModel(int id)
        {
            RightAnglePushUpModel rightAnglePushUpModel = db.RightAnglePushUps.Find(id);
            if (rightAnglePushUpModel == null)
            {
                return NotFound();
            }

            db.RightAnglePushUps.Remove(rightAnglePushUpModel);
            db.SaveChanges();

            return Ok(rightAnglePushUpModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RightAnglePushUpModelExists(int id)
        {
            return db.RightAnglePushUps.Count(e => e.ID == id) > 0;
        }
    }
}