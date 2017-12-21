namespace WebStoreProject.Models
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Category")]
        public int? UpperCategoryID { get; set; }

        public virtual Category UpperCategory { get; set; }

        public virtual ICollection<Category> Category1 { get; set; }

        public virtual ICollection<Product> Product { get; set; }

    }

}