namespace WebStoreProject.Models
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Web.Mvc;

    public partial class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Category")]
        public int CategoryID { get; set; }

        [Display(Name = "Price")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public double Price_brutto { get; set; }

        [Display(Name = "VAT")]
        public int? VatID { get; set; }

        [Required]
        [AllowHtml]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Logo file")]
        public string LogoImagePath { get; set; }

        [Required]
        [Display(Name = "Details file")]
        public string DetailsImagePath { get; set; }

        public virtual Category Category { get; set; }
        public virtual VAT Vat { get; set; }
        public virtual ICollection<Order_item> Order_item { get; set; }
    }

}
