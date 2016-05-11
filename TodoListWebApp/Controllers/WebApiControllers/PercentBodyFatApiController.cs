﻿using System;
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
    public class PercentBodyFatApiController : ApiController
    {
        private TodoListWebAppContext db = new TodoListWebAppContext();

        // GET: api/PercentBodyFatApi
        public IQueryable<PercentBodyFatModel> GetPercentBodyFats()
        {
            return db.PercentBodyFats;
        }

        // GET: api/PercentBodyFatApi/5
        [ResponseType(typeof(PercentBodyFatModel))]
        public IHttpActionResult GetPercentBodyFatModel(int id)
        {
            PercentBodyFatModel percentBodyFatModel = db.PercentBodyFats.Find(id);
            if (percentBodyFatModel == null)
            {
                return NotFound();
            }

            return Ok(percentBodyFatModel);
        }

        // PUT: api/PercentBodyFatApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPercentBodyFatModel(int id, PercentBodyFatModel percentBodyFatModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != percentBodyFatModel.ID)
            {
                return BadRequest();
            }

            db.Entry(percentBodyFatModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PercentBodyFatModelExists(id))
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

        // POST: api/PercentBodyFatApi
        [ResponseType(typeof(PercentBodyFatModel))]
        public IHttpActionResult PostPercentBodyFatModel(PercentBodyFatModel percentBodyFatModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PercentBodyFats.Add(percentBodyFatModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = percentBodyFatModel.ID }, percentBodyFatModel);
        }

        // DELETE: api/PercentBodyFatApi/5
        [ResponseType(typeof(PercentBodyFatModel))]
        public IHttpActionResult DeletePercentBodyFatModel(int id)
        {
            PercentBodyFatModel percentBodyFatModel = db.PercentBodyFats.Find(id);
            if (percentBodyFatModel == null)
            {
                return NotFound();
            }

            db.PercentBodyFats.Remove(percentBodyFatModel);
            db.SaveChanges();

            return Ok(percentBodyFatModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PercentBodyFatModelExists(int id)
        {
            return db.PercentBodyFats.Count(e => e.ID == id) > 0;
        }
    }
}