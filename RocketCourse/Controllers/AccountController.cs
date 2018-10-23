using RocketCourse.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RocketCourse.Controllers
{
    public class AccountController : Controller
    {
        CourseContext db = new CourseContext();

        static bool chatLoad = true;

        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.ReturnURL = TempData["returnURL"];
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (CourseContext db = new CourseContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Mail == model.Mail && u.Password == model.Password);
                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Mail, true);
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    ModelState.AddModelError("", "Такого пользователя не существует");
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                
                User user = null;
                using (CourseContext db = new CourseContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Mail == model.Mail);
                }
                if (user == null)
                {
                    //if (uploadImage!=null)
                    //{
                        byte[] imageData = null;
                        using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                        {
                            imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                        }
                    //}
                    using (CourseContext db = new CourseContext())
                    {
                        //if (uploadImage != null)
                        //{
                            db.Users.Add(new User { Mail = model.Mail, Password = model.Password, Name = model.Name, Age = model.Age, Phone = model.Phone, Rolei = 3, Image = imageData });
                        //}
                        //else
                        //{
                        //    db.Users.Add(new User { Mail = model.Mail, Password = model.Password, Name = model.Name, Age = model.Age, Phone = model.Phone, Rolei = 3, Image = imageData });
                        //}
                        
                        db.SaveChanges();

                        user = db.Users.Where(u => u.Mail == model.Mail && u.Password == model.Password).FirstOrDefault();
                    }
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Mail, true);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult PersonalCabinet()
        {
            User user = db.Users.Include("Group").FirstOrDefault(u => u.Mail == User.Identity.Name);
            return View(user);
        }

        [HttpPost]
        public ActionResult PersonalCabinet(User user, HttpPostedFileBase uploadImage)
        {
            User mainUser = db.Users.FirstOrDefault(u => u.Mail == user.Mail);

            byte[] imageData = null;
            using (var binaryReader = new BinaryReader(uploadImage.InputStream))
            {
                imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
            }
            mainUser.Image = imageData;
            mainUser.Mail = user.Mail;
            mainUser.Phone = user.Phone;
            mainUser.Password = user.Password;

            db.SaveChanges();
  
            return RedirectToAction("index", "home");
        }

        [Authorize]
        [HttpGet]
        public ActionResult Chat()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Chat(string msg)
        {
            ModelState.Clear();
            User us = db.Users.FirstOrDefault(u => u.Mail == User.Identity.Name);
            Group gr = us.Group;
            Message message = new Message() { User = us, Time = DateTime.Now, Content = msg, Group = gr };
            db.Messages.Add(message);
            db.SaveChanges();
            if (chatLoad == true)
            {
                chatLoad = false;
                return PartialView("Message", db.Messages.Include("User"));
            }
            else
            {
                List<Message> msgs = new List<Message>();
                Message mes = new Message();
                foreach (var item in db.Messages)
                {
                    mes = item;
                }
                msgs.Add(mes);
                return PartialView("Message", msgs);
            }

        }
    }
}