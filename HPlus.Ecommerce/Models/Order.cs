//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TBJ.Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int OrderId { get; set; }
        public Nullable<int> Oder_FK_Product { get; set; }
        public Nullable<int> Oder_FK_User { get; set; }
        public Nullable<int> Oder_FK_Inovice { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public Nullable<int> OrderQuantity { get; set; }
        public Nullable<decimal> OrderBill { get; set; }
        public Nullable<int> OrderUnitPrice { get; set; }
    
        public virtual Invoice Invoice { get; set; }
        public virtual Product Product { get; set; }
        public virtual Users User { get; set; }
    }
}
