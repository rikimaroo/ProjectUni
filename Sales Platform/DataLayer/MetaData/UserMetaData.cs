using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class UserMetaData
    {
        public int UserID { get; set; }

        [Display(Name = "نقش کاربر")]
        [Required(ErrorMessage = " لطفا مقدار {0} را وارد کنید")]
        public int RoleID { get; set; }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = " لطفا مقدار {0} را وارد کنید")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = " لطفا مقدار {0} را وارد کنید")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = " لطفا مقدار {0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "کد فعالسازی")]
        public string ActiveCode { get; set; }

        [Display(Name = "فعال")]
        [Required(ErrorMessage = " لطفا مقدار {0} را وارد کنید")]
        public bool IsActive { get; set; }

        [Display(Name = "تاریخ ثبت نام")]
        public System.DateTime RegisterDate { get; set; }

    }
    [MetadataType(typeof(UserMetaData))]
    public partial class User
    {

    }
}
