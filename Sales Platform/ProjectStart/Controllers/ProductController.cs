using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using DataLayer.ViewModels;

namespace ProjectStart.Controllers
{
    public class ProductController : Controller
    {
        private ProjectStart_DBEntities db = new ProjectStart_DBEntities();
        // GET: Product
        public ActionResult LastProduct()
        {
            var listProduct = db.Products.Where(p => (p.Expiration >= DateTime.Now || p.Expiration == null) && p.Available > 0).OrderByDescending(p => p.CreateDate).Take(12).ToList();

            return PartialView(listProduct);
        }

    }
}