using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer.ViewModels;
using DataLayer;

namespace ProjectStart.Areas.Vendor.Controllers
{
    [Authorize(Roles = "Vendor")]
    public class ProductController : Controller
    {
        ProjectStart_DBEntities db = new ProjectStart_DBEntities();
        // GET: Vendor/Product
        public ActionResult ShowProduct()
        {
            List<UsersVendorViewModel> reciveProductList = new List<UsersVendorViewModel>();

            var user = db.Users.First(p => p.UserName == User.Identity.Name).UserID;
            //var listrecive = db.Recive_Vendor_Products.Where(p => p.UserIDRef == user).Distinct().ToList();

            var listrecive = db.Recive_Vendor_Products.Where(p => p.UserIDRef == user).GroupBy(p => p.ProductIDRef).Select(p => p.FirstOrDefault()).ToList();
            //var listrecivetwo = db.Recive_Vendor_Products.Where(p => p.UserIDRef == user).Select(p=> p.UserIDRef).Distinct().ToList();

            foreach (var item in listrecive)
            {
                var listTitle = db.Recive_Vendor_Products.Where(p => p.Product.ProductID == p.ProductIDRef).Select(p => p.Product.Title).Distinct().ToList();

                reciveProductList.Add(new UsersVendorViewModel
                {
                    GroupID = item.RVP_ID,
                    Title = listTitle[0].ToString(),
                });
            }

            return View(reciveProductList);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var reciveVendorProducts = db.Recive_Vendor_Products.FirstOrDefault(p => p.RVP_ID == id);
            Vendor_Products vendorProduct = new Vendor_Products();
            if (db.Vendor_Products.Any(p => p.UserIDRef == reciveVendorProducts.UserIDRef && p.ProductIDRef == reciveVendorProducts.ProductIDRef))
            {
                var vendorProductItem = db.Vendor_Products.First(p => p.UserIDRef == reciveVendorProducts.UserIDRef && p.ProductIDRef == reciveVendorProducts.ProductIDRef);

                vendorProduct.DiscountValue = vendorProductItem.DiscountValue;
                vendorProduct.Price = vendorProductItem.Price;
            }


            vendorProduct.ProductIDRef = reciveVendorProducts.ProductIDRef;
            vendorProduct.UserIDRef = reciveVendorProducts.UserIDRef;




            return View(vendorProduct);
        }

        [HttpPost]
        public ActionResult Edit(Vendor_Products vendorProduct)
        {
            if (ModelState.IsValid)
            {
                if (vendorProduct.DiscountValue <= 100 && vendorProduct.DiscountValue >= 0)
                {
                    if (db.Vendor_Products.Any(p => p.UserIDRef == vendorProduct.UserIDRef && p.ProductIDRef == vendorProduct.ProductIDRef))
                    {
                        var updateVendorProduct = db.Vendor_Products.First(p => p.UserIDRef == vendorProduct.UserIDRef && p.ProductIDRef == vendorProduct.ProductIDRef);
                        updateVendorProduct.Price = vendorProduct.Price;
                        updateVendorProduct.DiscountValue = vendorProduct.DiscountValue;
                    }
                    else
                    {
                        db.Vendor_Products.Add(vendorProduct);
                    }
                    db.SaveChanges();
                }
            }
            return RedirectToAction("ShowProduct", "Product");
        }
    }
}