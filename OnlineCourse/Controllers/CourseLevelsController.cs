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
    public class CourseLevelsController : Controller
    {
        private Model1 db = new Model1();

        // GET: CourseLevels
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View(db.CourseLevels.ToList());
        }

        // GET: CourseLevels/Details/5
        [Authorize(Roles = "admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseLevel courseLevel = db.CourseLevels.Find(id);
            if (courseLevel == null)
            {
                return HttpNotFound();
            }
            return View(courseLevel);
        }

        // GET: CourseLevels/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CourseLevels/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LevelName")] CourseLevel courseLevel)
        {
            if (ModelState.IsValid)
            {
                db.CourseLevels.Add(courseLevel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(courseLevel);
        }

        // GET: CourseLevels/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseLevel courseLevel = db.CourseLevels.Find(id);
            if (courseLevel == null)
            {
                return HttpNotFound();
            }
            return View(courseLevel);
        }

        // POST: CourseLevels/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LevelName")] CourseLevel courseLevel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseLevel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(courseLevel);
        }

        // GET: CourseLevels/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseLevel courseLevel = db.CourseLevels.Find(id);
            if (courseLevel == null)
            {
                return HttpNotFound();
            }
            return View(courseLevel);
        }

        // POST: CourseLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseLevel courseLevel = db.CourseLevels.Find(id);
            db.CourseLevels.Remove(courseLevel);
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
