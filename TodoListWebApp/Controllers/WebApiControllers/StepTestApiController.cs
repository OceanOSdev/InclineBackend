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
    public class StepTestApiController : ApiController
    {
        private TodoListWebAppContext db = new TodoListWebAppContext();

        // GET: api/StepTestApi
        public IQueryable<StepTestModel> GetStepTests()
        {
            return db.StepTests;
        }

        // GET: api/StepTestApi/5
        [ResponseType(typeof(StepTestModel))]
        public IHttpActionResult GetStepTestModel(int id)
        {
            StepTestModel stepTestModel = db.StepTests.Find(id);
            if (stepTestModel == null)
            {
                return NotFound();
            }

            return Ok(stepTestModel);
        }

        // PUT: api/StepTestApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStepTestModel(int id, StepTestModel stepTestModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stepTestModel.ID)
            {
                return BadRequest();
            }

            db.Entry(stepTestModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StepTestModelExists(id))
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

        // POST: api/StepTestApi
        [ResponseType(typeof(StepTestModel))]
        public IHttpActionResult PostStepTestModel(StepTestModel stepTestModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StepTests.Add(stepTestModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = stepTestModel.ID }, stepTestModel);
        }

        // DELETE: api/StepTestApi/5
        [ResponseType(typeof(StepTestModel))]
        public IHttpActionResult DeleteStepTestModel(int id)
        {
            StepTestModel stepTestModel = db.StepTests.Find(id);
            if (stepTestModel == null)
            {
                return NotFound();
            }

            db.StepTests.Remove(stepTestModel);
            db.SaveChanges();

            return Ok(stepTestModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StepTestModelExists(int id)
        {
            return db.StepTests.Count(e => e.ID == id) > 0;
        }
    }
}