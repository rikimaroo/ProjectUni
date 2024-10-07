using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using DataLayer.ViewModels;

namespace ProjectStart.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        ProjectStart_DBEntities db = new ProjectStart_DBEntities();
        // GET: Admin/Orders
        public ActionResult ReportOrders()
        {
            var orders = db.Orders.ToList();
            List<OrderReportViewModel> orderList = new List<OrderReportViewModel>();

            foreach (var item in orders.GroupBy(p=> p.ProductID).Select(p=> p.First()))
            {
                var product = db.Products.Where(p => p.ProductID == item.ProductID).Select(p => new 
                {
                    ImageName = p.ImageName,
                    Title = p.Title,
                    Period = p.PeriodID,
                }).ToList();
                int periodid = product[0].Period;
                var priodname = db.Periods.First(p => p.PeriodID == periodid).Title;

                orderList.Add(new OrderReportViewModel()
                {
                    OrderID = item.OrderID,
                    Title = product[0].Title,
                    ImageName = product[0].ImageName,
                    Count = orders.Where(p => p.ProductID == item.ProductID).Count(),
                    PeriodName = priodname,
                });

            }
            return View(orderList);
        }

        public ActionResult Details(int id)
        {
            var orderList = db.Orders.Where(p=> p.ProductID == db.Orders.FirstOrDefault(a=> a.OrderID == id).ProductID).ToList();

            List<OrderUserViewModel> orderUser = new List<OrderUserViewModel>();
            foreach (var item in orderList)
            {
                orderUser.Add(new OrderUserViewModel
                {
                    UserName = item.User.UserName,
                    Date = (DateTime)item.Date,
                    IsFinaly = (bool)item.IsFinaly
                });
            }
            return View(orderUser);
        }
    }
}