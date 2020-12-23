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
    public class CoursController : Controller
    {
        private Model1 db = new Model1();

        // GET: Cours
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var courses = db.Courses.Include(c => c.CourseLevel).Include(c => c.Department);
            return View(courses.ToList());
        }

        // GET: Cours/Details/5
        [Authorize(Roles = "admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Courses.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            return View(cours);
        }

        // GET: Cours/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.LevelId = new SelectList(db.CourseLevels, "Id", "LevelName");
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartmentName");
            return View();
        }

        // POST: Cours/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CourseName,LevelId,DepartmentId,Course_Duration")] Cours cours)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(cours);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LevelId = new SelectList(db.CourseLevels, "Id", "LevelName", cours.LevelId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartmentName", cours.DepartmentId);
            return View(cours);
        }

        // GET: Cours/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Courses.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            ViewBag.LevelId = new SelectList(db.CourseLevels, "Id", "LevelName", cours.LevelId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartmentName", cours.DepartmentId);
            return View(cours);
        }

        // POST: Cours/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CourseName,LevelId,DepartmentId,Course_Duration")] Cours cours)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cours).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LevelId = new SelectList(db.CourseLevels, "Id", "LevelName", cours.LevelId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartmentName", cours.DepartmentId);
            return View(cours);
        }

        // GET: Cours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Courses.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            return View(cours);
        }

        // POST: Cours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cours cours = db.Courses.Find(id);
            db.Courses.Remove(cours);
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
