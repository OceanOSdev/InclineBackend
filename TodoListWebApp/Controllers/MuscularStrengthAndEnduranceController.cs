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
    public class MuscularStrengthAndEnduranceController : Controller
    {
        private TodoListWebAppContext db = new TodoListWebAppContext();

        // GET: MuscularStrengthAndEndurance
        public ActionResult Index()
        {
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            var currentMuscularStrength = db.MuscularStrengthsAndEndurances.Where(a => a.Owner == owner);
            return View(currentMuscularStrength.ToList());
        }

        // GET: MuscularStrengthAndEndurance/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MuscularStrengthAndEndurance muscularStrengthAndEndurance = db.MuscularStrengthsAndEndurances.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (muscularStrengthAndEndurance == null || muscularStrengthAndEndurance.Owner != owner)
            {
                return HttpNotFound();
            }
            return View(muscularStrengthAndEndurance);
        }

        // GET: MuscularStrengthAndEndurance/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MuscularStrengthAndEndurance/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Owner,CurlUps,RightAnglePushUps,MaxBench,MaxLegPress,PullUps,FlexedArmHang,Logged")] MuscularStrengthAndEndurance muscularStrengthAndEndurance)
        {
            if (ModelState.IsValid)
            {
                string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
                muscularStrengthAndEndurance.Owner = owner;
                muscularStrengthAndEndurance.Logged = DateTime.UtcNow;
                db.MuscularStrengthsAndEndurances.Add(muscularStrengthAndEndurance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(muscularStrengthAndEndurance);
        }

        // GET: MuscularStrengthAndEndurance/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MuscularStrengthAndEndurance muscularStrengthAndEndurance = db.MuscularStrengthsAndEndurances.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (muscularStrengthAndEndurance == null || muscularStrengthAndEndurance.Owner != owner)
            {
                return HttpNotFound();
            }
            return View(muscularStrengthAndEndurance);
        }

        // POST: MuscularStrengthAndEndurance/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Owner,CurlUps,RightAnglePushUps,MaxBench,MaxLegPress,PullUps,FlexedArmHang,Logged")] MuscularStrengthAndEndurance muscularStrengthAndEndurance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(muscularStrengthAndEndurance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(muscularStrengthAndEndurance);
        }

        // GET: MuscularStrengthAndEndurance/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MuscularStrengthAndEndurance muscularStrengthAndEndurance = db.MuscularStrengthsAndEndurances.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (muscularStrengthAndEndurance == null || muscularStrengthAndEndurance.Owner != owner)
            {
                return HttpNotFound();
            }
            return View(muscularStrengthAndEndurance);
        }

        // POST: MuscularStrengthAndEndurance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MuscularStrengthAndEndurance muscularStrengthAndEndurance = db.MuscularStrengthsAndEndurances.Find(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (muscularStrengthAndEndurance == null || muscularStrengthAndEndurance.Owner != null)
                return HttpNotFound();
            db.MuscularStrengthsAndEndurances.Remove(muscularStrengthAndEndurance);
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
