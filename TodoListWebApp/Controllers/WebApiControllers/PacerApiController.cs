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
    public class PacerApiController : ApiController
    {
        private TodoListWebAppContext db = new TodoListWebAppContext();

        // GET: api/PacerApi
        public IQueryable<PacerModel> GetPacers()
        {
            return db.Pacers;
        }

        // GET: api/PacerApi/5
        [ResponseType(typeof(PacerModel))]
        public IHttpActionResult GetPacerModel(int id)
        {
            PacerModel pacerModel = db.Pacers.Find(id);
            if (pacerModel == null)
            {
                return NotFound();
            }

            return Ok(pacerModel);
        }

        // PUT: api/PacerApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPacerModel(int id, PacerModel pacerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pacerModel.ID)
            {
                return BadRequest();
            }

            db.Entry(pacerModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PacerModelExists(id))
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

        // POST: api/PacerApi
        [ResponseType(typeof(PacerModel))]
        public IHttpActionResult PostPacerModel(PacerModel pacerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pacers.Add(pacerModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pacerModel.ID }, pacerModel);
        }

        // DELETE: api/PacerApi/5
        [ResponseType(typeof(PacerModel))]
        public IHttpActionResult DeletePacerModel(int id)
        {
            PacerModel pacerModel = db.Pacers.Find(id);
            if (pacerModel == null)
            {
                return NotFound();
            }

            db.Pacers.Remove(pacerModel);
            db.SaveChanges();

            return Ok(pacerModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PacerModelExists(int id)
        {
            return db.Pacers.Count(e => e.ID == id) > 0;
        }
    }
}