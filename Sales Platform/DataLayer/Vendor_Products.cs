//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Vendor_Products
    {
        public int VendorProductID { get; set; }
        public int UserIDRef { get; set; }
        public int ProductIDRef { get; set; }
        public int Price { get; set; }
        public int DiscountValue { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}