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
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.Orders = new HashSet<Order>();
            this.Product_Selected_Category = new HashSet<Product_Selected_Category>();
            this.Recive_Vendor_Products = new HashSet<Recive_Vendor_Products>();
            this.Vendor_Products = new HashSet<Vendor_Products>();
        }
    
        public int ProductID { get; set; }
        public int PeriodID { get; set; }
        public int UserIDCreator { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Text { get; set; }
        public int Price { get; set; }
        public int Available { get; set; }
        public string ImageName { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<int> DiscountType { get; set; }
        public Nullable<int> DiscountValue { get; set; }
        public Nullable<System.DateTime> Expiration { get; set; }
        public Nullable<int> CategoryIDRef { get; set; }
        public int BrandID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
        public virtual Period Period { get; set; }
        public virtual Product_Brand Product_Brand { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product_Selected_Category> Product_Selected_Category { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Recive_Vendor_Products> Recive_Vendor_Products { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vendor_Products> Vendor_Products { get; set; }
    }
}
