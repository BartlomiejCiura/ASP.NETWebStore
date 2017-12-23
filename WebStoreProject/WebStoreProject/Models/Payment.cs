using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebStoreProject.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Payment")]
        public string Name { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}