namespace WebStoreProject.Models
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Orders
    {
        [Key]
        public int Id { get; set; }

        public string UserID { get; set; }

        [Required]
        public string Payment { get; set; }

        [Required]
        public string Shipment { get; set; }

        public double Value { get; set; }


        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Order_details> Order_details { get; set; }

        public virtual ICollection<Order_item> Order_item { get; set; }
    }

}
