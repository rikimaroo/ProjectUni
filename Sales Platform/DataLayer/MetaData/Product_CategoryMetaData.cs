using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace DataLayer
{
    public class Product_CategoryMetaData
    {
        [Key]
        public int Product_CategoryID { get; set; }
        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Display(Name = "زیر مجموعه")]
        public Nullable<int> ParentID { get; set; }
    }
    [MetadataType(typeof(Product_CategoryMetaData))]
    
    public partial class Product_Categorys
    {

    }
}
