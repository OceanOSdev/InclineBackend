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

namespace TodoListWebApp.Controllers
{
    [Authorize]
    public class CardiovascularFitnessController : Controller
    {
        private TodoListWebAppContext db = new TodoListWebAppContext();

        // GET: CardiovascularFitness
        public ActionResult Index()
        {
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            var currentUserCardio = db.Cardios.Where(a => a.Owner == owner);
            //return new HttpStatusCodeResult(418);
            return View(currentUserCardio.ToList());
        }

        // GET: CardiovascularFitness/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CardiovascularFitness cardiovascularFitness = db.Cardios.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (cardiovascularFitness == null || cardiovascularFitness.Owner != owner)
            {
                return HttpNotFound();
            }
            return View(cardiovascularFitness);
        }

        // GET: CardiovascularFitness/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CardiovascularFitness/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Owner,HalfMileTime,Pacer,MileTime,StepTestSteps,StepTestHeartRate,Logged")] CardiovascularFitness cardiovascularFitness)
        {
            if (ModelState.IsValid)
            {
                string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
                cardiovascularFitness.Owner = owner;
                cardiovascularFitness.Logged = DateTime.UtcNow;
                db.Cardios.Add(cardiovascularFitness);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cardiovascularFitness);
        }

        // GET: CardiovascularFitness/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CardiovascularFitness cardiovascularFitness = db.Cardios.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (cardiovascularFitness == null || cardiovascularFitness.Owner != owner)
            {
                return HttpNotFound();
            }
            return View(cardiovascularFitness);
        }

        // POST: CardiovascularFitness/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Owner,HalfMileTime,Pacer,MileTime,StepTestSteps,StepTestHeartRate,Logged")] CardiovascularFitness cardiovascularFitness)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cardiovascularFitness).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cardiovascularFitness);
        }

        // GET: CardiovascularFitness/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CardiovascularFitness cardiovascularFitness = db.Cardios.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (cardiovascularFitness == null || cardiovascularFitness.Owner != owner)
            {
                return HttpNotFound();
            }
            return View(cardiovascularFitness);
        }

        // POST: CardiovascularFitness/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CardiovascularFitness cardiovascularFitness = db.Cardios.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (cardiovascularFitness == null || cardiovascularFitness.Owner != owner)
                return HttpNotFound();
            db.Cardios.Remove(cardiovascularFitness);
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
