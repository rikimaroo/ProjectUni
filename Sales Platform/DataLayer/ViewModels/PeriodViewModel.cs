using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.ViewModels
{
    public class ShowPeriodViewModel
    {
        public int PeriodID { get; set; }

        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Display(Name = "سال")]
        public int Year { get; set; }

        [Display(Name = "ماه")]
        public int Month { get; set; }
    }
}
