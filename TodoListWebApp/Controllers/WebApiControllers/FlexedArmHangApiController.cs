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
    public class FlexedArmHangApiController : ApiController
    {
        private TodoListWebAppContext db = new TodoListWebAppContext();

        // GET: api/FlexedArmHangApi
        public IQueryable<FlexedArmHangModel> GetFlexedArmHangs()
        {
            return db.FlexedArmHangs;
        }

        // GET: api/FlexedArmHangApi/5
        [ResponseType(typeof(FlexedArmHangModel))]
        public IHttpActionResult GetFlexedArmHangModel(int id)
        {
            FlexedArmHangModel flexedArmHangModel = db.FlexedArmHangs.Find(id);
            if (flexedArmHangModel == null)
            {
                return NotFound();
            }

            return Ok(flexedArmHangModel);
        }

        // PUT: api/FlexedArmHangApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFlexedArmHangModel(int id, FlexedArmHangModel flexedArmHangModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != flexedArmHangModel.ID)
            {
                return BadRequest();
            }

            db.Entry(flexedArmHangModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlexedArmHangModelExists(id))
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

        // POST: api/FlexedArmHangApi
        [ResponseType(typeof(FlexedArmHangModel))]
        public IHttpActionResult PostFlexedArmHangModel(FlexedArmHangModel flexedArmHangModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FlexedArmHangs.Add(flexedArmHangModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = flexedArmHangModel.ID }, flexedArmHangModel);
        }

        // DELETE: api/FlexedArmHangApi/5
        [ResponseType(typeof(FlexedArmHangModel))]
        public IHttpActionResult DeleteFlexedArmHangModel(int id)
        {
            FlexedArmHangModel flexedArmHangModel = db.FlexedArmHangs.Find(id);
            if (flexedArmHangModel == null)
            {
                return NotFound();
            }

            db.FlexedArmHangs.Remove(flexedArmHangModel);
            db.SaveChanges();

            return Ok(flexedArmHangModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FlexedArmHangModelExists(int id)
        {
            return db.FlexedArmHangs.Count(e => e.ID == id) > 0;
        }
    }
}