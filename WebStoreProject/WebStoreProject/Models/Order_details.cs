//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebStoreProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order_details
    {
        public int Id { get; set; }
        public Nullable<int> Order_id { get; set; }
        public Nullable<int> Status_id { get; set; }
    
        public virtual Orders Orders { get; set; }
        public virtual Status Status { get; set; }
    }
}
