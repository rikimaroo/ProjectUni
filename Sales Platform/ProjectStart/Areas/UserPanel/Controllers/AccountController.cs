using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using DataLayer.ViewModels;
using System.Web.Security;

namespace ProjectStart.Areas.UserPanel.Controllers
{
    public class AccountController : Controller
    {
        ProjectStart_DBEntities db = new ProjectStart_DBEntities();
        // GET: UserPanel/Account
        public ActionResult Index()
        {
            var user = db.Users.Include("Vendors").First(p => p.UserName == User.Identity.Name);

            return View(user);
        }

        [HttpPost]
        public ActionResult ChangeInformation(User user, FormCollection form)
        {
            var temp = form["UserName"].ToString();
            //aslan kamel nashodeee
            return RedirectToAction("Index", "Account");
        }
        
        [HttpGet]
        public ActionResult ChangePassword()
        {
            ChangePasswrodViewModel change = new ChangePasswrodViewModel();
            return View(change);
        }

        [HttpPost]
        public ActionResult ChangePasswrod(ChangePasswrodViewModel change)
        {
            if (ModelState.IsValid)
            {
                var pass = FormsAuthentication.HashPasswordForStoringInConfigFile(change.OldPassword, "MD5");
                var user = db.Users.First(p => p.UserName == User.Identity.Name);
                if (user.Password == pass)
                {
                    user.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(change.NewPassword, "MD5");
                    db.SaveChanges();
                    //return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("OldPassword", "رمز عبور فعلی را صحیح نیست!!!");
                }
            }

            return RedirectToAction("index");
        }
    }

}