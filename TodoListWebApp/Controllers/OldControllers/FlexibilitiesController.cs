using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TodoListWebApp.DAL;
using TodoListWebApp.Models;
using System.Security.Claims;

namespace TodoListWebApp.Controllers
{
    [Authorize]
    public class FlexibilitiesController : Controller
    {
        private TodoListWebAppContext db = new TodoListWebAppContext();

        // GET: Flexibilities
        public ActionResult Index()
        {
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            var list = db.Flexibilities.Where(a => a.Owner == owner);
            return View(list.ToList());
        }

        // GET: Flexibilities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flexibility flexibility = db.Flexibilities.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (flexibility == null || flexibility.Owner != owner)
            {
                return HttpNotFound();
            }
            return View(flexibility);
        }

        // GET: Flexibilities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Flexibilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Owner,SitAndReach,ArmAndShoulder,TrunkLift,Logged")] Flexibility flexibility)
        {
            if (ModelState.IsValid)
            {
                flexibility.Owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
                flexibility.Logged = DateTime.UtcNow;
                db.Flexibilities.Add(flexibility);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(flexibility);
        }

        // GET: Flexibilities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flexibility flexibility = db.Flexibilities.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (flexibility == null || flexibility.Owner != owner)
            {
                return HttpNotFound();
            }
            return View(flexibility);
        }

        // POST: Flexibilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Owner,SitAndReach,ArmAndShoulder,TrunkLift,Logged")] Flexibility flexibility)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flexibility).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(flexibility);
        }

        // GET: Flexibilities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flexibility flexibility = db.Flexibilities.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (flexibility == null || flexibility.Owner != owner)
            {
                return HttpNotFound();
            }
            return View(flexibility);
        }

        // POST: Flexibilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Flexibility flexibility = db.Flexibilities.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (flexibility == null || flexibility.Owner != owner)
            {
                return HttpNotFound();
            }
            db.Flexibilities.Remove(flexibility);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
