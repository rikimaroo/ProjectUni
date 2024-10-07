using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public class Vendor_GroupsMetaData
    {
        [Key]
        public int GroupID { get; set; }

        [Display(Name ="عنوان گروه")]
        public string Title { get; set; }
    }
    [MetadataType(typeof(Vendor_GroupsMetaData))]

    public partial class Vendor_Groups
    {

    }
}
