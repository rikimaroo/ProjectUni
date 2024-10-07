using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace ProjectStart.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BrandController : Controller
    {
        private ProjectStart_DBEntities db = new ProjectStart_DBEntities();
        public ActionResult ShowBrand()
        {
            var brandList = db.Product_Brand.ToList();
            return View(brandList);
        }

        [HttpGet]
        public ActionResult CreateBrand()
        {
            Product_Brand brand = new Product_Brand();
            return PartialView(brand);
        }

        [HttpPost]
        public ActionResult CreateBrand(Product_Brand brand)
        {
            db.Product_Brand.Add(brand);
            db.SaveChanges();
            return RedirectToAction("ShowBrand", "Product");
        }

        [HttpGet]
        public ActionResult EditBrand(int id)
        {
            var brand = db.Product_Brand.First(p => p.BrandID == id);

            return PartialView(brand);
        }

        [HttpPost]
        public ActionResult EditBrand(Product_Brand brand)
        {
            var update = db.Product_Brand.First(p=> p.BrandID == brand.BrandID);
            update.Title = brand.Title;
            db.SaveChanges();

            return RedirectToAction("ShowBrand");
        }
    }
}
