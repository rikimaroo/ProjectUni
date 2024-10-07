using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class Product_BrandMetaData
    {
        [Key]
        public int BrandID { get; set; }
        [Display(Name = "عنوان")]
        public string Title { get; set; }
    }
    [MetadataType(typeof(Product_BrandMetaData))]

    public partial class Product_Brand
    {

    }
}
