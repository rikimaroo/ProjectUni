using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.ViewModels
{
    public class OrderItem
    {
        public int ProductID { get; set; }
        public int Count { get; set; }
    }

    public class OrderItemViewModel
    {
        public int ProductID { get; set; }
        public int Count { get; set; }
        public string ProductImage { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
    }

    public class OrderReportViewModel
    {
        public int OrderID { get; set; }

        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Display(Name = "تصویر")]
        public string ImageName { get; set; }

        [Display(Name = "تعداد")]
        public int Count { get; set; }

        [Display(Name = "دوره")]
        public string PeriodName { get; set; }
    }

    public class OrderUserViewModel
    {
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public bool IsFinaly { get; set; }


    }
}
