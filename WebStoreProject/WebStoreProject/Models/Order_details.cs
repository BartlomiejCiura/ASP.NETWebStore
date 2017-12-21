namespace WebStoreProject.Models
{

    using System;
    using System.Collections.Generic;

    public partial class Order_details
    {
        public int Id { get; set; }

        public int OrderID { get; set; }

        public int StatusID { get; set; }

        public virtual Orders Order { get; set; }

        public virtual Status Status { get; set; }
    }

}
