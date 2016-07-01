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
    /// <summary>
    /// Represents the measurements related to Arm and Shoulders.
    /// </summary>
    [HostAuthentication("AADBearer")]
    [Authorize]
    public class ArmAndShoulderApiController : ApiController
    {
        private TodoListWebAppContext db = new TodoListWebAppContext();

        // GET: api/ArmAndShoulderApi
        /// <summary>
        /// Returns a list of the user's Arm and Shoulders scores.
        /// </summary>
        /// <returns>A list of the user's Arm and Shoulders scores.</returns>
        public IQueryable<ArmAndShoulderModel> GetArmAndShoulders()
        {
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            return db.ArmAndShoulders.Where(a => a.Owner == owner).OrderByDescending(x => x.Logged);
        }

        // GET: api/ArmAndShoulderApi/5
        /// <summary>
        /// Queries the DBSet for an Arm and Shoulder measurement with the corresponding ID.
        /// </summary>
        /// <param name="id">The id of the specific Arm and Shoulders measurement</param>
        /// <returns>
        /// JSON representation of the measurement, or returns a 404 if either the
        /// measurement does not exist or doesn't belong to the authenticated user.
        /// </returns>
        [ResponseType(typeof(ArmAndShoulderModel))]
        public IHttpActionResult GetArmAndShoulderModel(int id)
        {
            ArmAndShoulderModel armAndShoulderModel = db.ArmAndShoulders.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (armAndShoulderModel == null || armAndShoulderModel.Owner != owner)
            {
                return NotFound();
            }

            return Ok(armAndShoulderModel);
        }

        // PUT: api/ArmAndShoulderApi/5
        /// <summary>
        /// Updates a specified entry of the Arm and Shoulders measurements.
        /// </summary>
        /// <param name="id">The id of the measurement to update.</param>
        /// <param name="armAndShoulderModel">The new Arm and Shoulders Measurement.</param>
        /// <returns>
        /// A bad request if either the model is not valid or if the entry to be updated does not belong
        /// to the user performing the update request. Otherwise No content. Well, unless there is a 
        /// concurrecy issue, in which case, RIP request.
        /// </returns>
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
        /// <summary>
        /// Creates a new Arm and Shoulders measurement.
        /// </summary>
        /// <param name="armAndShoulderModel">The measurement to add.</param>
        /// <returns>
        /// The JSON of the measurement if it succeeded. Otherwise a bad request.
        /// </returns>
        [ResponseType(typeof(ArmAndShoulderModel))]
        public IHttpActionResult PostArmAndShoulderModel(ArmAndShoulderModel armAndShoulderModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            //armAndShoulderModel.Logged = DateTime.UtcNow;
            armAndShoulderModel.Owner = owner;
            db.ArmAndShoulders.Add(armAndShoulderModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = armAndShoulderModel.ID }, armAndShoulderModel);
        }

        // DELETE: api/ArmAndShoulderApi/5
        /// <summary>
        /// Deletes a specified entry.
        /// </summary>
        /// <param name="id">The id of the entry to delete.</param>
        /// <returns>
        /// Json representation of the entry that was deleted.
        /// If the entry does not exist or is not owned by the requester,
        /// then a 404 will be returned.
        /// </returns>
        [ResponseType(typeof(ArmAndShoulderModel))]
        public IHttpActionResult DeleteArmAndShoulderModel(int id)
        {
            ArmAndShoulderModel armAndShoulderModel = db.ArmAndShoulders.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (armAndShoulderModel == null || armAndShoulderModel.Owner != owner)
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