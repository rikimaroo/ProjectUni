using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer.ViewModels;
using DataLayer;

namespace ProjectStart.Controllers
{
    public class OrderController : Controller
    {
        ProjectStart_DBEntities db = new ProjectStart_DBEntities();
        // GET: Order
        public ActionResult ShowOrder()
        {
            List<OrderItemViewModel> orderitemList = new List<OrderItemViewModel>();
            //List<Product> productList = new List<Product>();

            if (Session["OrderItem"] != null)
            {
                List<OrderItem> order = Session["OrderItem"] as List<OrderItem>;
                foreach (var item in order)
                {
                    var productList = (db.Products.First(p => p.ProductID == item.ProductID));

                    orderitemList.Add(new OrderItemViewModel
                    {
                        Count = item.Count,
                        ProductID = item.ProductID,
                        Price = productList.Price,
                        ProductImage = productList.ImageName,
                        Title = productList.Title
                    });
                }
            }
            return View(orderitemList);
        }

        public ActionResult Payment()
        {
            List<Order> order = new List<Order>();

            if (Session["OrderItem"] != null)
            {
                List<OrderItem> orderitem = Session["OrderItem"] as List<OrderItem>;
                var userName = User.Identity.Name;
                var currentUser = db.Users.FirstOrDefault(p => p.UserName == userName);
                foreach (var item in orderitem)
                {
                    order.Add(new Order
                    {
                        UserID = currentUser.UserID,
                        ProductID = item.ProductID,
                        PeriodIDRef = 1,// movaghate ta bebinam bayad in chejor handle beshe
                        Date = DateTime.Now,
                        IsFinaly = true
                    });
                    
                }
                db.Orders.AddRange(order);
                db.SaveChanges();

                Session.Clear();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public void AddMinusOrderItem(int productID, bool? minus = false)
        {
            //minus = false;
            List<OrderItem> orderList = new List<OrderItem>();

            if (Session["OrderItem"] != null && minus == false)
            {
                orderList = Session["OrderItem"] as List<DataLayer.ViewModels.OrderItem>;
                var index = orderList.FindIndex(p => p.ProductID == productID);
                if (index == -1)
                {
                    orderList.Add(new OrderItem()
                    {
                        ProductID = productID,
                        Count = 1,
                    });
                }
                else
                {
                    orderList[index].Count += 1;
                }
                //orderList[index].Count = 1;
            }
            else if (Session["OrderItem"] != null && minus == true)
            {
                orderList = Session["OrderItem"] as List<DataLayer.ViewModels.OrderItem>;
                var index = orderList.FindIndex(p => p.ProductID == productID);
                orderList[index].Count -= 1;
                if (orderList[index].Count == 0)
                {
                    orderList.RemoveAt(index);
                }
            }
            else if(minus != true)
            {
                orderList.Add(new OrderItem()
                {
                    ProductID = productID,
                    Count = 1
                });
            }

            Session["OrderItem"] = orderList;
        }

        [HttpGet]
        public int CountOrderItem()
        {
            List<OrderItem> orderList = new List<OrderItem>();

            orderList = Session["OrderItem"] as List<OrderItem>;

            return orderList.Sum(p => p.Count);
        }
    }
}