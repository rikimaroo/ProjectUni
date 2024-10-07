using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using DataLayer.ViewModels;

namespace ProjectStart.Areas.Admin.Controllers
{
    public class VendorController : Controller
    {
        ProjectStart_DBEntities db = new ProjectStart_DBEntities();
        // GET: Admin/Vendor
        public ActionResult VendorGroup()
        {
            return View();
        }

        public ActionResult ShowVendorGroup(int? orderid)
        {
            List<Vendor_Groups> vendorGroups = new List<Vendor_Groups>();
            vendorGroups = db.Vendor_Groups.ToList();
            ViewBag.OrderID = orderid;

            return PartialView(vendorGroups);
        }

        public ActionResult ShowVendor(int? orderid)
        {
            List<DataLayer.Vendor> vendor = new List<DataLayer.Vendor>();
            vendor = db.Vendors.ToList();
            ViewBag.OrderID = orderid;

            return PartialView(vendor);
        }

        public ActionResult Create()
        {
            Vendor_Groups vendorGroup = new Vendor_Groups();
            return PartialView(vendorGroup);
        }

        [HttpPost]
        public ActionResult Create(Vendor_Groups vendorgroups)
        {
            db.Vendor_Groups.Add(vendorgroups);
            db.SaveChanges();
            return RedirectToAction("VendorGroup");
        }

        public ActionResult Edit(int id)
        {
            var vendorList = new UsersVendorViewModel();
            var list = db.Vendor_Groups.First(p => p.GroupID == id);

            vendorList.GroupID = list.GroupID;
            vendorList.Title = list.Title;

            ViewBag.UserList = db.Users.Where(p => p.RoleID == 3).ToList();
            //ViewBag.GroupList = db.Vendor_Groups.ToList();
            ViewBag.vendorSelectedGroup = db.Vendor_Selected_Group.Where(p => p.GroupID == id).ToList();

            return View(vendorList);
        }

        [HttpPost]
        public ActionResult Edit(UsersVendorViewModel usersVendorGroup)
        {
            
            var vendorSelected = new Vendor_Selected_Group();
            int userid = int.Parse(usersVendorGroup.User);
            if (!db.Vendor_Selected_Group.Any(p=> p.GroupID == usersVendorGroup.GroupID && p.UserID == userid))
            {
                vendorSelected.GroupID = usersVendorGroup.GroupID;
                vendorSelected.UserID = int.Parse(usersVendorGroup.User);

                db.Vendor_Selected_Group.Add(vendorSelected);
                db.SaveChanges();
            }

            return RedirectToAction("Edit", "Vendor", usersVendorGroup.GroupID);
        }

        public void Delete(int id)
        {
            var vendor = db.Vendor_Selected_Group.Find(id);
            db.Vendor_Selected_Group.Remove(vendor);
            db.SaveChanges();
        }

        public ActionResult VendorProduct()
        {
            List<VendorProductViewModel> vendorProductList = new List<VendorProductViewModel>();

            var list = db.Vendor_Products.ToList();

            foreach (var item in list)
            {
                var UserName = db.Users.First(p => p.UserID == item.UserIDRef).UserName.ToString();
                var ProductTitle = db.Products.First(p => p.ProductID == item.ProductIDRef).Title.ToString();//in code v khat bala khili sangine nabayad intor bashe

                vendorProductList.Add(new VendorProductViewModel
                {
                    DiscountValue = item.DiscountValue,
                    Price = item.Price,
                    User = UserName,
                    Product = ProductTitle
                });
            }


            return View(vendorProductList);
        }

        public void SendToVendor(int id, int orderid)
        {
            List<Recive_Vendor_Products> reciveVendorProduct = new List<Recive_Vendor_Products>();
            
            var list = db.Vendor_Selected_Group.Where(p => p.GroupID == id).ToList();
            var productid = db.Orders.First(p => p.OrderID == orderid).ProductID;

            
            foreach (var item in list)
            {
                if (!db.Recive_Vendor_Products.Any(p => p.UserIDRef == item.UserID && p.ProductIDRef == productid))
                {
                    reciveVendorProduct.Add(new Recive_Vendor_Products
                    {
                        UserIDRef = item.UserID,
                        ProductIDRef = productid
                    });
                }
                else
                {
                    break;
                }
            }
            db.Recive_Vendor_Products.AddRange(reciveVendorProduct);
            db.SaveChanges();
            RedirectToAction("ReportOrders", " Orders");
        }

        public void SendToVendorSingle(int id, int orderid)
        {

            var productid = db.Orders.First(p => p.OrderID == orderid).ProductID;

            if (!db.Recive_Vendor_Products.Any(p=> p.UserIDRef == id && p.ProductIDRef == productid))
            {
                db.Recive_Vendor_Products.Add(new Recive_Vendor_Products
                {
                    UserIDRef = id,
                    ProductIDRef = productid
                });
            }
            db.SaveChanges();
            RedirectToAction("ReportOrders", " Orders");
        }
    }
}