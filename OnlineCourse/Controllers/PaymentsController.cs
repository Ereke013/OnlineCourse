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
    public class PaymentsController : Controller
    {
        private Model1 db = new Model1();
        private ApplicationDbContext dbContext = new ApplicationDbContext();

        // GET: Payments
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var payments = db.Payments.Include(p => p.Cours).Include(p => p.Price);
            //var user = dbContext.Users;
            //ViewBag.UserId = new SelectList(dbContext.Users, "Id", "FullName");
            return View(payments.ToList());
        }

        // GET: Payments/Details/5
        [Authorize(Roles = "admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // GET: Payments/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "CourseName");
            ViewBag.PriceId = new SelectList(db.Prices, "Id", "Id");
            ViewBag.UserId = new SelectList(dbContext.Users, "Id", "FullName");
            return View();
        }

        // POST: Payments/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,CourseId,PriceId,Course_Duration")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                payment.DateOfPay = DateTime.Now;
                db.Payments.Add(payment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "Id", "CourseName", payment.CourseId);
            ViewBag.PriceId = new SelectList(db.Prices, "Id", "Id", payment.PriceId);
            return View(payment);
        }

        // GET: Payments/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "CourseName", payment.CourseId);
            ViewBag.PriceId = new SelectList(db.Prices, "Id", "Id", payment.PriceId);
            return View(payment);
        }

        // POST: Payments/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,CourseId,PriceId,Course_Duration")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "CourseName", payment.CourseId);
            ViewBag.PriceId = new SelectList(db.Prices, "Id", "Id", payment.PriceId);
            return View(payment);
        }

        // GET: Payments/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment payment = db.Payments.Find(id);
            db.Payments.Remove(payment);
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
