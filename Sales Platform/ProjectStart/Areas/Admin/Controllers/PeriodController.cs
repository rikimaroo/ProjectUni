using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer.ViewModels;
using DataLayer;

namespace ProjectStart.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PeriodController : Controller
    {
        ProjectStart_DBEntities db = new ProjectStart_DBEntities();
        // GET: Admin/Periods
        public ActionResult ShowPeriod()
        {
            List<ShowPeriodViewModel> periodList = new List<ShowPeriodViewModel>();

            var periods = db.Periods.ToList();
            foreach (var item in periods)
            {
                periodList.Add(new ShowPeriodViewModel
                {
                    PeriodID = item.PeriodID,
                    Title = item.Title,
                    Year = item.StartDate.Year,
                    Month = item.StartDate.Month,
                });
            }
            return View(periodList);
        }


        [HttpGet]
        public ActionResult Create()
        {
            Period period = new Period();

            return PartialView(period);
        }

        [HttpPost]
        public ActionResult Create(Period period)
        {
            if (ModelState.IsValid)
            {
                db.Periods.Add(period);
                db.SaveChanges();
            }
            return RedirectToAction("ShowPeriod");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var period = db.Periods.Find(id);

            return PartialView(period);
        }

        [HttpPost]
        public ActionResult Edit(Period period)
        {
            if (ModelState.IsValid)
            {
                var update = db.Periods.First(p => p.PeriodID == period.PeriodID);
                update.Title = period.Title;
                update.StartDate = period.StartDate;
                update.EndDate = period.EndDate;

                db.SaveChanges();
            }

            return RedirectToAction("ShowPeriod", "Periods");
        }
    }
}