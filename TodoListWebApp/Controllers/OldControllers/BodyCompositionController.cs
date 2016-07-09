using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using TodoListWebApp.DAL;
using TodoListWebApp.Models;
using TodoListWebApp.ViewModels;

namespace TodoListWebApp.Controllers
{
    [Authorize]
    public class BodyCompositionController : Controller
    {
        private TodoListWebAppContext db = new TodoListWebAppContext();

        // GET: BodyComposition
        public ActionResult Index()
        {
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            var data = new BodyCompViewModel()
            {
                BodyFat = db.PercentBodyFats.Where(a => a.Owner == owner).ToList(),
                Height = db.Heights.Where(a => a.Owner == owner).ToList(),
                Weight = db.Weights.Where(a => a.Owner == owner).ToList()
            };
            return View(data);
        }

        // GET: BodyComposition/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BodyComposition bodyComposition = db.BodyComps.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (bodyComposition == null || bodyComposition.Owner != owner)
            {
                return HttpNotFound();
            }
            return View(bodyComposition);
        }

        // GET: BodyComposition/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BodyComposition/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Owner,Height,Weight,BodyFat,Logged")] BodyComposition bodyComposition)
        {
            if (ModelState.IsValid)
            {
                bodyComposition.Owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
                bodyComposition.Logged = DateTime.UtcNow;
                db.BodyComps.Add(bodyComposition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bodyComposition);
        }

        // GET: BodyComposition/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BodyComposition bodyComposition = db.BodyComps.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (bodyComposition == null || bodyComposition.Owner == owner)
            {
                return HttpNotFound();
            }
            return View(bodyComposition);
        }

        // POST: BodyComposition/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Owner,Height,Weight,BodyFat,Logged")] BodyComposition bodyComposition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bodyComposition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bodyComposition);
        }

        // GET: BodyComposition/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BodyComposition bodyComposition = db.BodyComps.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (bodyComposition == null || bodyComposition.Owner != owner)
            {
                return HttpNotFound();
            }
            return View(bodyComposition);
        }

        // POST: BodyComposition/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BodyComposition bodyComposition = db.BodyComps.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (bodyComposition == null || bodyComposition.Owner != owner)
            {
                return HttpNotFound();
            }
            db.BodyComps.Remove(bodyComposition);
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
