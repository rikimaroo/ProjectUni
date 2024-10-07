using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class ProductMetaData
    {
        [Key]
        public int ProductID { get; set; }
        public int UserIDCreator { get; set; }
        [Display(Name = "عنوان محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(50)]
        public string Title { get; set; }

        [Display(Name = "توضیح مختصر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(150)]
        public string ShortDescription { get; set; }

        [Display(Name = "متن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.MultilineText)]
        [StringLength(1000)]
        public string Text { get; set; }

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Price { get; set; }

        [Display(Name = "تعداد موجود")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Available { get; set; }

        [Display(Name = "عکس محصول")]
        public string ImageName { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        public System.DateTime CreateDate { get; set; }

        [Display(Name = "تاریخ انقضا")]
        public Nullable<System.DateTime> Expiration { get; set; }

        [Display(Name = "نوع تخفیف")]
        public int DiscountType { get; set; }

        [Display(Name = "میزان تخفیف")]
        public int DiscountValue { get; set; }

        [Display(Name = "ایجاد کننده")]
        public virtual User User { get; set; }

        [Display(Name = "دوره")]
        public int PeriodID { get; set; }

        [Display(Name = "برند")]
        public int BrandID { get; set; }


    }
    [MetadataType(typeof(ProductMetaData))]
    public partial class Product
    {
    }

}
