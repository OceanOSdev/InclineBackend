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
    public class SitAndReachApiController : ApiController
    {
        private TodoListWebAppContext db = new TodoListWebAppContext();

        // GET: api/SitAndReachApi
        public IQueryable<SitAndReachModel> GetSitAndReaches()
        {
            return db.SitAndReaches;
        }

        // GET: api/SitAndReachApi/5
        [ResponseType(typeof(SitAndReachModel))]
        public IHttpActionResult GetSitAndReachModel(int id)
        {
            SitAndReachModel sitAndReachModel = db.SitAndReaches.Find(id);
            if (sitAndReachModel == null)
            {
                return NotFound();
            }

            return Ok(sitAndReachModel);
        }

        // PUT: api/SitAndReachApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSitAndReachModel(int id, SitAndReachModel sitAndReachModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sitAndReachModel.ID)
            {
                return BadRequest();
            }

            db.Entry(sitAndReachModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SitAndReachModelExists(id))
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

        // POST: api/SitAndReachApi
        [ResponseType(typeof(SitAndReachModel))]
        public IHttpActionResult PostSitAndReachModel(SitAndReachModel sitAndReachModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SitAndReaches.Add(sitAndReachModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = sitAndReachModel.ID }, sitAndReachModel);
        }

        // DELETE: api/SitAndReachApi/5
        [ResponseType(typeof(SitAndReachModel))]
        public IHttpActionResult DeleteSitAndReachModel(int id)
        {
            SitAndReachModel sitAndReachModel = db.SitAndReaches.Find(id);
            if (sitAndReachModel == null)
            {
                return NotFound();
            }

            db.SitAndReaches.Remove(sitAndReachModel);
            db.SaveChanges();

            return Ok(sitAndReachModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SitAndReachModelExists(int id)
        {
            return db.SitAndReaches.Count(e => e.ID == id) > 0;
        }
    }
}