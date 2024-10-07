using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectStart.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductCategoryController : Controller
    {
        private ProjectStart_DBEntities db = new ProjectStart_DBEntities();

        public ActionResult ShowProductCategory()
        {
            var listCategory = db.Product_Categorys.ToList();

            return View(listCategory);
        }

        [HttpGet]
        public ActionResult CreateCategory(int? id)
        {
            Product_Categorys productCategory = new Product_Categorys();
            if (id != null)
            {
                productCategory.ParentID = id;
            }
            return PartialView(productCategory);
        }

        [HttpPost]
        public ActionResult CreateCategory(Product_Categorys productCategory)
        {
            db.Product_Categorys.Add(productCategory);
            db.SaveChanges();

            return RedirectToAction("ShowProductCategory", "Productcategory");
        }

        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            var productCategory = db.Product_Categorys.First(p => p.Product_CategoryID == id);

            return View(productCategory);
        }

        [HttpPost]
        public ActionResult EditCategory(Product_Categorys productCategory)
        {
            if (ModelState.IsValid)
            {
                var update = db.Product_Categorys.First(p => p.Product_CategoryID == productCategory.Product_CategoryID);
                update.Title = productCategory.Title;
                db.SaveChanges();
            }
            return RedirectToAction("ShowProductCategory");
        }

        [HttpPost]
        public ActionResult DeleteCategory(int id)
        {
            var delProduct = db.Product_Categorys.Find(id);
            if (db.Product_Selected_Category.Any(p => p.CategoryIDRef == delProduct.Product_CategoryID))
            {
                var productSelectedCat = db.Product_Selected_Category.Where(p => p.CategoryIDRef == delProduct.Product_CategoryID).ToList();
                foreach (var item in productSelectedCat)
                {
                    db.Product_Selected_Category.Remove(item);
                }
            }
            if (delProduct.ParentID == null)
            {
                if (db.Product_Categorys.Any(p => p.ParentID == delProduct.Product_CategoryID))
                {
                    var subDelProduct = db.Product_Categorys.Where(p => p.ParentID == delProduct.Product_CategoryID).ToList();
                    foreach (var item in subDelProduct)
                    {
                        db.Product_Categorys.Remove(item);
                    }
                }
            }

            db.Product_Categorys.Remove(delProduct);
            db.SaveChanges();

            return RedirectToAction("ShowProductCategory", "Productcategory");
        }

    }
}