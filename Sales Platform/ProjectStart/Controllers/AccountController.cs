using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using DataLayer.ViewModels;
using System.Web.Security;
using ProjectStart.Utilities;


namespace ProjectStart.Controllers
{
    public class AccountController : Controller
    {
        private ProjectStart_DBEntities db = new ProjectStart_DBEntities();
        [Route("Register")]
        [HttpGet]
        public ActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();
            return View(model);
        }

        [Route("Register")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                if (!db.Users.Any(p=> p.UserName == register.UserName.Trim().ToLower()))
                {
                    if (!db.Users.Any(p=> p.Email == register.Email.Trim().ToLower()))
                    {
                        User newUser = new User()
                        {
                            UserName = register.UserName,
                            Email = register.Email,
                            Password = FormsAuthentication.HashPasswordForStoringInConfigFile(register.Password, "MD5"),
                            IsActive = false,
                            ActiveCode = Guid.NewGuid().ToString(),
                            RegisterDate = DateTime.Now,
                            RoleID = 1
                        };

                        db.Users.Add(newUser);
                        db.SaveChanges();

                        string body = PartialToStringClass.RenderPartialView("ManageEmail", "ActiveEmail", newUser);
                        SendEmail.Send(newUser.Email, "ایمیل فعالسازی", body);

                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "ایمیل وارد شده قبلا استفاده شده است");
                    }
                }
                else
                {
                    ModelState.AddModelError("UserName", "نام کاربری وارد شده قبلا استفاده شده است");
                }
            }

            return View(register);
        }

        public ActionResult ActiveUser(string id)
        {
            var user = db.Users.FirstOrDefault(p => p.ActiveCode == id);
            if (user == null)
            {
                return HttpNotFound();
            }

            user.IsActive = true;
            user.ActiveCode = Guid.NewGuid().ToString();
            ViewBag.UserName = user.UserName;
            db.SaveChanges();
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            LoginViewModel model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel login, string ReturnUrl = "/")
        {
            if (ModelState.IsValid)
            {
                if (db.Users.Any(p=> p.Email == login.Email.Trim().ToString() || p.UserName == login.Email.Trim().ToString()))
                {
                    var user = db.Users.FirstOrDefault(p => p.Email == login.Email.Trim().ToString() || p.UserName == login.Email.Trim().ToString());
                    string hashPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(login.Password, "MD5");
                    if (user.Password == hashPassword)
                    {
                        if (user.IsActive)
                        {
                            FormsAuthentication.SetAuthCookie(user.UserName, login.RememberMe);
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            ModelState.AddModelError("Email", "حساب کاربری تایید نشده است");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "نام کاربری یا ایمیل در سایت وجود ندارد");

                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "نام کاربری یا ایمیل در سایت وجود ندارد");
                }
            }

            return View(login);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
    }
}