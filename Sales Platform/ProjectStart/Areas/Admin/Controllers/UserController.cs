using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using DataLayer.ViewModels;
using System.Web.Security;

namespace ProjectStart.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private ProjectStart_DBEntities db = new ProjectStart_DBEntities();

        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Role);
            return View(users.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountOrders = db.Orders.Where(p => p.UserID == id).Count().ToString();

            return View(user);
        }

        public ActionResult Create()
        {
            var model = new User();
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleTitle");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RoleID,UserName,Email,Password,ActiveCode,IsActive,RegisterDate")] User user, string Company, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                var listUsers = db.Users.ToList();
                if (!listUsers.Any(p => p.Email == user.Email.Trim().ToLower()))
                {
                    if (!listUsers.Any(p => p.UserName == user.UserName.Trim().ToLower()))
                    {
                        user.RegisterDate = DateTime.Now;
                        user.ActiveCode = Guid.NewGuid().ToString();
                        user.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(user.Password, "MD5");
                        db.Users.Add(user);
                        db.SaveChanges();

                        if (user.RoleID == 3)
                        {
                            if (form["Company"] == "" || form["Address"] == "" || form["PhoneNumber"] == "" ||
                            form["MobileNumber"] == "" || form["WebSite"] == "")
                            {
                                ModelState.AddModelError("Password", "پر کردن تمامی فیلد ها اجباری است");
                            }
                            else
                            {
                                var userID = db.Users.Where(p => p.Email == user.Email && p.UserName == user.UserName).Select(p => p.UserID).ToList();
                                DataLayer.Vendor vendor = new DataLayer.Vendor()
                                {
                                    UserIDRef = int.Parse(userID[0].ToString()),
                                    Company = form["Company"],
                                    Address = form["Address"],
                                    PhoneNumber = form["PhoneNumber"],
                                    MobileNumber = form["MobileNumber"],
                                    WebSite = form["WebSite"]
                                };

                                db.Vendors.Add(vendor);
                                db.SaveChanges();
                                return RedirectToAction("Index");
                            }
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("UserName", "نام کاربری قبلا ثبت شده است");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "ایمل قبلا ثبت شده است");
                }

                return RedirectToAction("Index");

            }

            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleTitle", user.RoleID);
            return View(user);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleTitle", user.RoleID);
            VendorViewModel vendor = new VendorViewModel();

            vendor.RoleID = user.RoleID;
            vendor.UserName = user.UserName;
            vendor.Email = user.Email;
            vendor.IsActive = user.IsActive;
            vendor.UserID = user.UserID;
            if (user.RoleID == 3)
            {
                if (db.Vendors.Any(p => p.UserIDRef == user.UserID))
                {
                    var vendorUser = db.Vendors.First(p => p.UserIDRef == user.UserID);
                    vendor.Company = vendorUser.Company;
                    vendor.Address = vendorUser.Address;
                    vendor.PhoneNumber = vendorUser.PhoneNumber;
                    vendor.MobileNumber = vendorUser.MobileNumber;
                    vendor.WebSite = vendorUser.WebSite;
                }
            }

            return View(vendor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,RoleID,UserName,Email,Password,ActiveCode,IsActive,RegisterDate,Company,Address,PhoneNumber,MobileNumber,WebSite")] VendorViewModel user)
        {
            if (ModelState.IsValid)
            {
                var editUser = db.Users.Find(user.UserID);
                editUser.UserID = user.UserID;
                editUser.Email = user.Email;
                editUser.UserName = user.UserName;
                editUser.IsActive = user.IsActive;
                editUser.RoleID = user.RoleID;

                if (user.RoleID == 3)
                {
                    if (db.Vendors.Any(p => p.UserIDRef == user.UserID))
                    {
                        var editVendorUser = db.Vendors.First(p => p.UserIDRef == user.UserID);
                        editVendorUser.Company = user.Company;
                        editVendorUser.Address = user.Address;
                        editVendorUser.PhoneNumber = user.PhoneNumber;
                        editVendorUser.MobileNumber = user.MobileNumber;
                        editVendorUser.WebSite = user.WebSite;
                    }

                    db.Vendors.Add(new DataLayer.Vendor
                    {
                        UserIDRef = user.UserID,
                        Company = user.Company,
                        Address = user.Address,
                        PhoneNumber = user.PhoneNumber,
                        MobileNumber = user.MobileNumber,
                        WebSite = user.WebSite,
                    });
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "RoleTitle", user.RoleID);
            return View(user);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
