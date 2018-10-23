using RocketCourse.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RocketCourse.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        CourseContext db = new CourseContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetUsers()
        {
            return PartialView(db.Users.Include("Group"));
        }

        public ActionResult GetGroups()
        {
            ViewBag.Users = db.Groups;
            return PartialView(db.Groups.Include("Course"));
        }

        public ActionResult GetCourses()
        {
            return PartialView(db.Courses);
        }

        public ActionResult GetOrderedCourses()
        {
            return PartialView(db.OrderedCourses);
        }

        [HttpGet]
        public ActionResult AddGroup()
        {
            SelectList courses = new SelectList(db.Courses, "Id", "Name");
            ViewBag.Courses = courses;
            return PartialView("AddGroup");
        }

        [HttpPost]
        public ActionResult AddGroup(Group group)
        {
            //group.Messages.Add(new Message { Name = User.Identity.Name, Time = DateTime.Now });
            db.Groups.Add(group);
            db.SaveChanges();
            ViewBag.CourseId = db.Courses.FirstOrDefault(u => u.Id == group.CourseId).Name;
            return View("Index");
        }

        [HttpGet]
        public ActionResult AddUser()
        {
            SelectList groups = new SelectList(db.Groups, "Id", "GroupName");
            ViewBag.Groups = groups;
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddUser(User user)
        {
            if (user!= null)
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
            return RedirectToAction("index");
        }

        [HttpGet]
        public ActionResult AddUserInGroup(int groupid)
        {
            SelectList users = new SelectList(db.Users, "Id", "Name");
            SelectList groups = new SelectList(db.Groups, "Id", "Name");
            ViewBag.Users = users;
            ViewBag.Groups = groups;
            ViewBag.GroupId = groupid;
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddUserInGroup(User user, int groupid)
        {
            user.Id = Convert.ToInt32(user.Mail);
            Group group = db.Groups.Find(groupid);
            User importantUser = db.Users.Find(user.Id);
            importantUser.GroupId = group.Id;
            importantUser.Group = group;
            group.Users.Add(importantUser);

            db.SaveChanges();

            return RedirectToAction("index");
        }

        [HttpGet]
        public ActionResult AddCourse()
        {
            return PartialView("AddCourse");
        }

        [HttpPost]
        public ActionResult AddCourse(Course course)
        {
            db.Courses.Add(course);
            db.SaveChanges();
            return RedirectToAction("index");
        }

        public ActionResult AddMessage()
        {
            //db.Messages.Add(new Message { User = db.Users.FirstOrDefault(u => u.Mail == User.Identity.Name), Content = "test", Group = db.Groups.FirstOrDefault(u => u.Id ==8), Time = DateTime.Now });
            db.SaveChanges();
            return RedirectToAction("Index", "Admin");
        }

        public ActionResult DeleteGroup(int id)
        {
            db.Groups.Remove(db.Groups.FirstOrDefault(u => u.Id == id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteCourse(int id)
        {
            db.Courses.Remove(db.Courses.FirstOrDefault(u => u.Id == id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteOrder(int id)
        {
            db.OrderedCourses.Remove(db.OrderedCourses.FirstOrDefault(u => u.Id == id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteAllOrders()
        {
            db.OrderedCourses.RemoveRange(db.OrderedCourses);
            db.SaveChanges();
            return RedirectToAction("index");
        }

        public ActionResult DeleteUser(int id)
        {
            db.Users.Remove(db.Users.FirstOrDefault(u => u.Id == id));
            db.SaveChanges();
            db.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpGet]
        public ActionResult EditGroup(int id)
        {
            Group group = db.Groups.Find(id);
            if (group != null)
            {
                SelectList courses = new SelectList(db.Courses, "id", "Name");
                ViewBag.Courses = courses;
                return View(group);
            }
            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult EditGroup(Group group)
        {
            db.Entry(group).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditCourse(int id)
        {
            Course course = db.Courses.Find(id);
            if (course != null)
            {
                return View(course);
            }
            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult EditCourse(Course course)
        {
            db.Entry(course).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}