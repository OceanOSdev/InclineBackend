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
    public class HeightApiController : ApiController
    {
        private TodoListWebAppContext db = new TodoListWebAppContext();

        // GET: api/HeightApi
        public IQueryable<HeightModel> GetHeights()
        {
            return db.Heights;
        }

        // GET: api/HeightApi/5
        [ResponseType(typeof(HeightModel))]
        public IHttpActionResult GetHeightModel(int id)
        {
            HeightModel heightModel = db.Heights.Find(id);
            if (heightModel == null)
            {
                return NotFound();
            }

            return Ok(heightModel);
        }

        // PUT: api/HeightApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHeightModel(int id, HeightModel heightModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != heightModel.ID)
            {
                return BadRequest();
            }

            db.Entry(heightModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HeightModelExists(id))
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

        // POST: api/HeightApi
        [ResponseType(typeof(HeightModel))]
        public IHttpActionResult PostHeightModel(HeightModel heightModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Heights.Add(heightModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = heightModel.ID }, heightModel);
        }

        // DELETE: api/HeightApi/5
        [ResponseType(typeof(HeightModel))]
        public IHttpActionResult DeleteHeightModel(int id)
        {
            HeightModel heightModel = db.Heights.Find(id);
            if (heightModel == null)
            {
                return NotFound();
            }

            db.Heights.Remove(heightModel);
            db.SaveChanges();

            return Ok(heightModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HeightModelExists(int id)
        {
            return db.Heights.Count(e => e.ID == id) > 0;
        }
    }
}