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
    [Authorize]
    public class CurlUpApiController : ApiController
    {
        private TodoListWebAppContext db = new TodoListWebAppContext();

        // GET: api/CurlUpApi
        public IQueryable<CurlUpModel> GetCurlUps()
        {
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            return db.CurlUps.Where(a => a.Owner == owner);
        }

        // GET: api/CurlUpApi/5
        [ResponseType(typeof(CurlUpModel))]
        public IHttpActionResult GetCurlUpModel(int id)
        {
            CurlUpModel curlUpModel = db.CurlUps.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (curlUpModel == null || curlUpModel.Owner != owner)
            {
                return NotFound();
            }

            return Ok(curlUpModel);
        }

        // PUT: api/CurlUpApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCurlUpModel(int id, CurlUpModel curlUpModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != curlUpModel.ID)
            {
                return BadRequest();
            }

            db.Entry(curlUpModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurlUpModelExists(id))
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

        // POST: api/CurlUpApi
        [ResponseType(typeof(CurlUpModel))]
        public IHttpActionResult PostCurlUpModel(CurlUpModel curlUpModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            curlUpModel.Owner = owner;
            curlUpModel.Logged = DateTime.UtcNow;
            db.CurlUps.Add(curlUpModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = curlUpModel.ID }, curlUpModel);
        }

        // DELETE: api/CurlUpApi/5
        [ResponseType(typeof(CurlUpModel))]
        public IHttpActionResult DeleteCurlUpModel(int id)
        {
            CurlUpModel curlUpModel = db.CurlUps.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (curlUpModel == null || curlUpModel.Owner != owner)
            {
                return NotFound();
            }

            db.CurlUps.Remove(curlUpModel);
            db.SaveChanges();

            return Ok(curlUpModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CurlUpModelExists(int id)
        {
            return db.CurlUps.Count(e => e.ID == id) > 0;
        }
    }
}