using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineCourse.Models;

namespace OnlineCourse.Controllers
{
    public class CourseSelectionsController : Controller
    {
        private Model1 db = new Model1();

        // GET: CourseSelections
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var courseSelections = db.CourseSelections.Include(c => c.Payment);
            return View(courseSelections.ToList());
        }

        // GET: CourseSelections/Details/5
        [Authorize(Roles = "admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseSelection courseSelection = db.CourseSelections.Find(id);
            if (courseSelection == null)
            {
                return HttpNotFound();
            }
            return View(courseSelection);
        }

        // GET: CourseSelections/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.PaymentId = new SelectList(db.Payments, "Id", "UserId");
            return View();
        }

        // POST: CourseSelections/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PaymentId,Check")] CourseSelection courseSelection)
        {
            if (ModelState.IsValid)
            {
                db.CourseSelections.Add(courseSelection);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PaymentId = new SelectList(db.Payments, "Id", "UserId", courseSelection.PaymentId);
            return View(courseSelection);
        }

        // GET: CourseSelections/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseSelection courseSelection = db.CourseSelections.Find(id);
            if (courseSelection == null)
            {
                return HttpNotFound();
            }
            ViewBag.PaymentId = new SelectList(db.Payments, "Id", "UserId", courseSelection.PaymentId);
            return View(courseSelection);
        }

        // POST: CourseSelections/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PaymentId,Check")] CourseSelection courseSelection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseSelection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PaymentId = new SelectList(db.Payments, "Id", "UserId", courseSelection.PaymentId);
            return View(courseSelection);
        }

        // GET: CourseSelections/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseSelection courseSelection = db.CourseSelections.Find(id);
            if (courseSelection == null)
            {
                return HttpNotFound();
            }
            return View(courseSelection);
        }

        // POST: CourseSelections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseSelection courseSelection = db.CourseSelections.Find(id);
            db.CourseSelections.Remove(courseSelection);
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
