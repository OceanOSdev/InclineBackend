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
    public class WeightApiController : ApiController
    {
        private TodoListWebAppContext db = new TodoListWebAppContext();

        // GET: api/WeightApi
        public IQueryable<WeightModel> GetWeights()
        {
            return db.Weights;
        }

        // GET: api/WeightApi/5
        [ResponseType(typeof(WeightModel))]
        public IHttpActionResult GetWeightModel(int id)
        {
            WeightModel weightModel = db.Weights.Find(id);
            if (weightModel == null)
            {
                return NotFound();
            }

            return Ok(weightModel);
        }

        // PUT: api/WeightApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWeightModel(int id, WeightModel weightModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != weightModel.ID)
            {
                return BadRequest();
            }

            db.Entry(weightModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeightModelExists(id))
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

        // POST: api/WeightApi
        [ResponseType(typeof(WeightModel))]
        public IHttpActionResult PostWeightModel(WeightModel weightModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Weights.Add(weightModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = weightModel.ID }, weightModel);
        }

        // DELETE: api/WeightApi/5
        [ResponseType(typeof(WeightModel))]
        public IHttpActionResult DeleteWeightModel(int id)
        {
            WeightModel weightModel = db.Weights.Find(id);
            if (weightModel == null)
            {
                return NotFound();
            }

            db.Weights.Remove(weightModel);
            db.SaveChanges();

            return Ok(weightModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WeightModelExists(int id)
        {
            return db.Weights.Count(e => e.ID == id) > 0;
        }
    }
}