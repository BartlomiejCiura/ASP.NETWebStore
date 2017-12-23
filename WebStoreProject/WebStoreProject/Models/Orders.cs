namespace WebStoreProject.Models
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Orders
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter your address!")]
        [Display(Name = "Address")]
        public string Shipment { get; set; }

        public string UserID { get; set; }

        [Required(ErrorMessage = "Choose payment!")]
        public int PaymentID { get; set; }

        [Required(ErrorMessage = "Choose delivery!")]
        public int DeliveryID { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public double Value { get; set; }


        public virtual ApplicationUser User { get; set; }

        public virtual Payment Payment { get; set; }

        public virtual Delivery Delivery { get; set; }

        public virtual ICollection<Order_details> Order_details { get; set; }

        public virtual ICollection<Order_item> Order_item { get; set; }
    }

}
