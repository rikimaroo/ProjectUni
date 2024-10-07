using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.ViewModels
{
    public class CreateProductViewModel
    {
        [Display(Name = "عنوان محصول")]
        [Required]
        public string Title { get; set; }

        [Display(Name = "توضیح مختصر")]
        [Required]
        public string ShortDescription { get; set; }

        [Display(Name = "متن")]
        [Required]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        [Display(Name = "قیمت")]
        [Required]
        public int Price { get; set; }

        [Display(Name = "تعداد موجود")]
        [Required]
        public int Available { get; set; }

        [Display(Name = "عکس محصول")]
        public string ImageName { get; set; }
    }
}
