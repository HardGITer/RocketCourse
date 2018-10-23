using RocketCourse.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RocketCourse.Controllers
{
    public class HomeController : Controller
    {
        CourseContext db = new CourseContext();

        public ActionResult Index()
        {
            var courses = db.Courses;
            ViewBag.Courses = courses;

            List<string> coursesNameList = new List<string>();
            //using (CourseContext db_ = new CourseContext())
            {
                foreach (var item in db.Courses)
                {
                    coursesNameList.Add(item.Name);
                }
                ViewBag.CoursesNameList = coursesNameList;
            }


            ViewBag.Groups = db.Groups;

            var users = db.Users;
            ViewBag.Users = users;

            ViewBag.UserName = User.Identity.Name;

            ////чет не работает..... хотя должно
            //if (User.Identity.IsAuthenticated == true)
            //{
            //    ViewBag.UserRole = db.Users.FirstOrDefault(u => u.Mail == User.Identity.Name).Role;
            //}

            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult OrderCourse(string courseName)
        {
            db.OrderedCourses.Add(new OrderedCourse { UserName = User.Identity.Name, OrderedCourseName = courseName, OrderDate = DateTime.Now });
            db.SaveChanges();

            Course course = db.Courses.FirstOrDefault(u => u.Name == courseName.ToString());
            ViewBag.CourseName = courseName.ToString();
            return View(course);
        }

        //[Authorize]
        //public string OrderCourse(PurchasedCourses purchased)
        //{
        //    User user = db.Users.FirstOrDefault(u => u.Mail == User.Identity.Name);
        //    purchased.PurchaserMail = User.Identity.Name;

        //    db.PurchasedCourses.Add(purchased);
        //    db.SaveChanges();
        //    return "Спасибо, " + User.Identity.Name + ", за покупку курса " + purchased.CourseName;
        //}

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult AddCourse()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult AddCourse(Course course)
        {
            db.Courses.Add(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        public void HelpMethod()
        {
            //db.Roles.Add(new Role { Name = "admin" });
            //db.Roles.Add(new Role { Name = "teacher" });
            //db.Roles.Add(new Role { Name = "user" });
            //db.Users.Add(new User { Name = "Матвей", Mail = "admin", Age = 18, Phone = "+3752555555555", Password = "admin", Rolei = 1 });
            //db.Users.Remove(db.Users.FirstOrDefault(u => u.Id == 2));
            db.SaveChanges();
        }

    }
}