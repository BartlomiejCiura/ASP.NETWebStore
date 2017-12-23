namespace WebStoreProject.Models
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Status
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Status")]
        public string Name { get; set; }

        public virtual ICollection<Order_details> Order_details { get; set; }
    }

}
