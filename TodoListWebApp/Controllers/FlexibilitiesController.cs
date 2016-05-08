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
    public class FlexibilityController : Controller
    {
        private TodoListWebAppContext db = new TodoListWebAppContext();

        // GET: Flexibilities
        public async Task<ActionResult> Index()
        {
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            var list = await db.Flexibilities.ToListAsync();
            return View(list.Where(a => a.Owner == owner));
        }

        // GET: Flexibilities/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flexibility flexibility = await db.Flexibilities.FindAsync(id);
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
        public async Task<ActionResult> Create([Bind(Include = "ID,Owner,SitAndReach,ArmAndShoulder,TrunkLift,Logged")] Flexibility flexibility)
        {
            if (ModelState.IsValid)
            {
                flexibility.Owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
                db.Flexibilities.Add(flexibility);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(flexibility);
        }

        // GET: Flexibilities/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flexibility flexibility = await db.Flexibilities.FindAsync(id);
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
        public async Task<ActionResult> Edit([Bind(Include = "ID,Owner,SitAndReach,ArmAndShoulder,TrunkLift,Logged")] Flexibility flexibility)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flexibility).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(flexibility);
        }

        // GET: Flexibilities/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flexibility flexibility = await db.Flexibilities.FindAsync(id);
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
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Flexibility flexibility = await db.Flexibilities.FindAsync(id);
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (flexibility == null || flexibility.Owner != owner)
            {
                return HttpNotFound();
            }
            db.Flexibilities.Remove(flexibility);
            await db.SaveChangesAsync();
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
