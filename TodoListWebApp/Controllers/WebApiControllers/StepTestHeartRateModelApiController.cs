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

namespace TodoListWebApp.Controllers.WebApiControllers
{
    public class StepTestHeartRateModelApiController : ApiController
    {
        private TodoListWebAppContext db = new TodoListWebAppContext();

        // GET: api/StepTestHeartRateModelApi
        public IQueryable<StepTestHeartRateModel> GetStepTestHeartRates()
        {
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            return db.StepTestHeartRates.Where(a => a.Owner == owner).OrderByDescending(x => x.Logged);
        }

        // GET: api/StepTestHeartRateModelApi/5
        [ResponseType(typeof(StepTestHeartRateModel))]
        public IHttpActionResult GetStepTestHeartRateModel(int id)
        {
            StepTestHeartRateModel stepTestHeartRateModel = db.StepTestHeartRates.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (stepTestHeartRateModel == null || stepTestHeartRateModel.Owner != owner)
            {
                return NotFound();
            }

            return Ok(stepTestHeartRateModel);
        }

        // PUT: api/StepTestHeartRateModelApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStepTestHeartRateModel(int id, StepTestHeartRateModel stepTestHeartRateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stepTestHeartRateModel.ID)
            {
                return BadRequest();
            }

            db.Entry(stepTestHeartRateModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StepTestHeartRateModelExists(id))
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

        // POST: api/StepTestHeartRateModelApi
        [ResponseType(typeof(StepTestHeartRateModel))]
        public IHttpActionResult PostStepTestHeartRateModel(StepTestHeartRateModel stepTestHeartRateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            stepTestHeartRateModel.Owner = owner;
            db.StepTestHeartRates.Add(stepTestHeartRateModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = stepTestHeartRateModel.ID }, stepTestHeartRateModel);
        }

        // DELETE: api/StepTestHeartRateModelApi/5
        [ResponseType(typeof(StepTestHeartRateModel))]
        public IHttpActionResult DeleteStepTestHeartRateModel(int id)
        {
            StepTestHeartRateModel stepTestHeartRateModel = db.StepTestHeartRates.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (stepTestHeartRateModel == null || stepTestHeartRateModel.Owner != owner)
            {
                return NotFound();
            }

            db.StepTestHeartRates.Remove(stepTestHeartRateModel);
            db.SaveChanges();

            return Ok(stepTestHeartRateModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StepTestHeartRateModelExists(int id)
        {
            return db.StepTestHeartRates.Count(e => e.ID == id) > 0;
        }
    }
}