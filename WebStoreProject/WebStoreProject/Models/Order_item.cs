namespace WebStoreProject.Models
{

    using System;
    using System.Collections.Generic;

    public partial class Order_item
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public int ProductID { get; set; }

        public int OrderID { get; set; }

        public virtual Product Product { get; set; }

        public virtual Orders Order { get; set; }
    }

}
