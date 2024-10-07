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
    public class ProductController : Controller
    {
        private ProjectStart_DBEntities db = new ProjectStart_DBEntities();

        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.User);
            return View(products.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult Create()
        {
            Product product = new Product();
            var username = User.Identity.Name;
            var user = db.Users.Where(p => p.Email == username || p.UserName == username).Select(p => p.UserID).ToList();
            product.UserIDCreator = int.Parse(user[0].ToString());

            ViewBag.DiscountType = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem{Text = "درصدی", Value = "0"},
                    new SelectListItem{Text = "مبلغی", Value = "1"},
                }, "Value", "Text");

            ViewBag.PeriodID = new SelectList(db.Periods, "PeriodID", "Title");
            ViewBag.Categorys = db.Product_Categorys.ToList();
            ViewBag.BrandID = new SelectList(db.Product_Brand, "BrandID", "Title");

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,UserIDCreator,Title,ShortDescription,Text,Price,Available,DiscountType,DiscountValue,ImageName,CreateDate,Expiration,PeriodID,BrandID")] Product product, HttpPostedFileBase ImageProduct, List<int> SelectedCategory)
        {
            if (ModelState.IsValid)
            {
                if (SelectedCategory == null)
                {
                    ViewBag.ErrorSelectedCategory = true;
                    ViewBag.Categorys = db.Product_Categorys.ToList();
                    return View("Create");
                }

                product.ImageName = "no_image.jpg";
                if (ImageProduct != null && ImageProduct.IsImage())
                {
                    product.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(ImageProduct.FileName);
                    ImageProduct.SaveAs(Server.MapPath("/Images/ProductImages/" + product.ImageName));

                    ImageResizer resizer = new ImageResizer();
                    resizer.Resize(Server.MapPath("/Images/ProductImages/" + product.ImageName), Server.MapPath("/Images/ProductImages/Thumb/" + product.ImageName));
                }
                product.CreateDate = DateTime.Now;
                db.Products.Add(product);

                foreach (var item in SelectedCategory)
                {
                    db.Product_Selected_Category.Add(new Product_Selected_Category
                    {
                        ProductIDRef = product.ProductID,
                        CategoryIDRef = item
                    });
                }


                db.SaveChanges();

                return RedirectToAction("Index", "Product");
            }
            return View(product);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.DiscountType = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem{Text = "درصدی", Value = "0"},
                    new SelectListItem{Text = "مبلغی", Value = "1"},
                }, "Value", "Text");
            ViewBag.PeriodID = new SelectList(db.Periods, "PeriodID", "Title");
            ViewBag.Categorys = db.Product_Categorys.ToList();
            ViewBag.BrandID = new SelectList(db.Product_Brand, "BrandID", "Title");
            ViewBag.Selected_Category = db.Product_Selected_Category.ToList();


            ViewBag.UserIDCreator = new SelectList(db.Users, "UserID", "UserName", product.UserIDCreator);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,UserIDCreator,Title,ShortDescription,Text,Price,Available,DiscountType,DiscountValue,ImageName,Expiration,PeriodID,CreateDate,BrandID")] Product product, HttpPostedFileBase ImageProduct)
        {
            if (ModelState.IsValid)
            {
                if (ImageProduct != null && ImageProduct.IsImage())
                {
                    product.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(ImageProduct.FileName);
                    ImageProduct.SaveAs(Server.MapPath("/Images/ProductImages/" + product.ImageName));

                    ImageResizer resizer = new ImageResizer();
                    resizer.Resize(Server.MapPath("/Images/ProductImages/" + product.ImageName), Server.MapPath("/Images/ProductImages/Thumb/" + product.ImageName));
                }


                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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

        public ActionResult ShowProductCategory()
        {
            var listCategory = db.Product_Categorys.ToList();

            return View(listCategory);
        }

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
