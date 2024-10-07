using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class Vendor_ProductMetaData
    {
        [Key]
        public int VendorProductID { get; set; }

        [Display(Name = "محصول")]
        public int ProductIDRef { get; set; }

        [Display(Name = "قیمت")]
        public int Price { get; set; }

        [Display(Name = "تخفیف")]
        public int DiscountValue { get; set; }

        [Display(Name = "نام تامین کننده")]
        public int UserIDRef { get; set; }
    }
    [MetadataType(typeof(Vendor_ProductMetaData))]

    public partial class Vendor_Products
    {

    }
}
