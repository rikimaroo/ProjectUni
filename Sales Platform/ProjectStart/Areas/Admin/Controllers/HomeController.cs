using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectStart.Areas.Admin.Controllers
{
    [Authorize(Roles =  "Admin")]
    public class HomeController : Controller
    {
        ProjectStart_DBEntities db = new ProjectStart_DBEntities();
        public ActionResult Index()
        {
            DateTime acceptable = DateTime.Now.AddDays(-7);
            var result = db.Orders.Where(p => p.IsFinaly == true && p.Date >= acceptable).ToList();
            List<DataLayer.ViewModels.KendoUi> model = new List<DataLayer.ViewModels.KendoUi>();
            int count = 0;

            for (int i = 0; i < 7; i++)
            {
                foreach (var item in result.Where(p=> p.Date == DateTime.Now.Date.AddDays(-i)))
                {
                    count++;
                }
                model.Add(new DataLayer.ViewModels.KendoUi { Count = count, Day = DateTime.Now.AddDays(-i) });
                count = 0;
            }
            return View(model);
        }

/*        public JsonResult GetCountOrders()
        {
            DateTime acceptable = DateTime.Now.AddDays(-7);
            var result = db.Orders.Where(p => p.IsFinaly == true && p.Date >= acceptable).Select(p=> p.Date);
            return Json(result);
        }*/

        public ActionResult SideLeft()
        {
            return PartialView();
        }
    }
}