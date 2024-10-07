using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.ViewModels
{
    public class UsersVendorViewModel
    {
        public int UserID { get; set; }
        public int GroupID { get; set; }

        [Display(Name = "گروه")]
        public string Title { get; set; }

        [Display(Name = "کاربر")]
        public string User { get; set; }
    }

    public class VendorProductViewModel
    {
        [Display(Name = " نام محصول")]
        public string Product { get; set; }

        [Display(Name = "قیمت")]
        public int Price { get; set; }

        [Display(Name = "تخفیف")]
        public int DiscountValue { get; set; }

        [Display(Name = "نام تامین کننده")]
        public string User { get; set; }
    }
}
