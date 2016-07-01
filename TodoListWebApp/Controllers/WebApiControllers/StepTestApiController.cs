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
    public class StepTestApiController : ApiController
    {
        private TodoListWebAppContext db = new TodoListWebAppContext();

        // GET: api/StepTestApi
        public IQueryable<StepTestModel> GetStepTests()
        {
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            return db.StepTests.Where(a => a.Owner == owner).OrderByDescending(x => x.Logged);
        }

        // GET: api/StepTestApi/5
        [ResponseType(typeof(StepTestModel))]
        public IHttpActionResult GetStepTestModel(int id)
        {
            StepTestModel stepTestModel = db.StepTests.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (stepTestModel == null || stepTestModel.Owner != owner)
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
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            stepTestModel.Owner = owner;
            //stepTestModel.Logged = DateTime.UtcNow;
            db.StepTests.Add(stepTestModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = stepTestModel.ID }, stepTestModel);
        }

        // DELETE: api/StepTestApi/5
        [ResponseType(typeof(StepTestModel))]
        public IHttpActionResult DeleteStepTestModel(int id)
        {
            StepTestModel stepTestModel = db.StepTests.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (stepTestModel == null || stepTestModel.Owner != owner)
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