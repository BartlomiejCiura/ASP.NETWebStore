namespace WebStoreProject.Models
{

    using System;
    using System.Collections.Generic;

    public partial class VAT
    {
        public int Id { get; set; }

        public int? Value { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }

}
