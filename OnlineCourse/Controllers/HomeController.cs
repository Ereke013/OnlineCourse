using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OnlineCourse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OnlineCourse.Controllers
{
    public class HomeController : Controller
    {

        Model1 db = new Model1();
        private ApplicationDbContext context = new ApplicationDbContext();

        public ActionResult Index()
        {
            //var courses = db.Courses.Include(p => p.Department).Include(b=>b.Level);
            //return View(courses.ToList());
            //var courses = db.Courses.Include(p => p.Department).Include(b => b.CourseLevel);
            //var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            //var userManager = new UserManager<ApplicationUser>(store);
            //ApplicationUser user = userManager.FindByNameAsync(User.Identity.Name).Result;
            //Session["currentUser"] = user;
            return View();
        }

        [Authorize]
        public ActionResult GetMyRoles()
        {
            IList<string> roles = new List<string> { "Роль не определена" };
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            if (user != null)
                roles = userManager.GetRoles(user.Id);
            return View(roles);
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Welcome()
        {
            //var courses = db.Courses.Include(p => p.Depatment).Include(b => b.CourseLevel);
            var courses = db.Courses;
            return View(courses.ToList());
            //return View();

        }

        public ActionResult Categories()
        {
            var vm = new CourseDep();
            vm.depData = db.Departments.ToList();
            vm.courseData = db.Courses.ToList();
            return View(vm);
        }

        public ActionResult Courses(String name)
        {
            var courses = db.Courses;
            if (name != null || name == "")
            {
                var search = courses.Where(a => a.CourseName.Contains(name)).ToList();
                return View(search.ToList());
            }
            return View(courses.ToList());
        }

        [HttpPost]
        public ActionResult CourseSearch(string name)
        {
            //var courses = db.Courses.Include(p => p.Department).Include(b => b.CourseLevel); 
            var courses = db.Courses;
            if (name != null || name == "")
            {
                var search = courses.Where(a => a.CourseName.Contains(name)).ToList();
                return View(search.ToList());
            }
            return View(courses.ToList());
        }

        public ActionResult myCourses()
        {
            //ApplicationUser user = context.Users.FirstOrDefault(u => u.UserName == username);
            var myCourse = db.CourseSelections;
            return View(myCourse.ToList());
        }

        // GET: Cours/Details/5
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

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult BuyCourse([Bind(Include = "Id,UserId,CourseId,PriceId,Course_Duration")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                //ApplicationDbContext oc = new ApplicationDbContext();
                //ApplicationUser user = oc.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                //payment.UserId = user.Id;
                payment.PriceId = 3;
                payment.Course_Duration = 3;
                payment.DateOfPay = DateTime.Now;
                db.Payments.Add(payment);
                db.SaveChanges();
                return RedirectToAction("Courses");
            }

            //ViewBag.CourseId = new SelectList(db.Courses, "Id", "CourseName", payment.CourseId);
            //ViewBag.PriceId = new SelectList(db.Prices, "Id", "Id", payment.PriceId);
            //return View(payment);
            return RedirectToAction("Index");
        }
    }
}