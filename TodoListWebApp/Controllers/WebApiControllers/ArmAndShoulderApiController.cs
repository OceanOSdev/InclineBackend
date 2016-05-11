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
    public class ArmAndShoulderApiController : ApiController
    {
        private TodoListWebAppContext db = new TodoListWebAppContext();

        // GET: api/ArmAndShoulderApi
        public IQueryable<ArmAndShoulderModel> GetArmAndShoulders()
        {
            return db.ArmAndShoulders;
        }

        // GET: api/ArmAndShoulderApi/5
        [ResponseType(typeof(ArmAndShoulderModel))]
        public IHttpActionResult GetArmAndShoulderModel(int id)
        {
            ArmAndShoulderModel armAndShoulderModel = db.ArmAndShoulders.Find(id);
            if (armAndShoulderModel == null)
            {
                return NotFound();
            }

            return Ok(armAndShoulderModel);
        }

        // PUT: api/ArmAndShoulderApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutArmAndShoulderModel(int id, ArmAndShoulderModel armAndShoulderModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != armAndShoulderModel.ID)
            {
                return BadRequest();
            }

            db.Entry(armAndShoulderModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArmAndShoulderModelExists(id))
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

        // POST: api/ArmAndShoulderApi
        [ResponseType(typeof(ArmAndShoulderModel))]
        public IHttpActionResult PostArmAndShoulderModel(ArmAndShoulderModel armAndShoulderModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ArmAndShoulders.Add(armAndShoulderModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = armAndShoulderModel.ID }, armAndShoulderModel);
        }

        // DELETE: api/ArmAndShoulderApi/5
        [ResponseType(typeof(ArmAndShoulderModel))]
        public IHttpActionResult DeleteArmAndShoulderModel(int id)
        {
            ArmAndShoulderModel armAndShoulderModel = db.ArmAndShoulders.Find(id);
            if (armAndShoulderModel == null)
            {
                return NotFound();
            }

            db.ArmAndShoulders.Remove(armAndShoulderModel);
            db.SaveChanges();

            return Ok(armAndShoulderModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArmAndShoulderModelExists(int id)
        {
            return db.ArmAndShoulders.Count(e => e.ID == id) > 0;
        }
    }
}