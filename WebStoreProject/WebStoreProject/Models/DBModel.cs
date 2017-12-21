using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebStoreProject.Models
{
    public class DBModel : DbContext
    {
        public DBModel() : base("DefaultConnection") { }

        public DbSet<Category> Category { get; set; }

        public DbSet<Order_details> Order_details { get; set; }

        public DbSet<Order_item> Order_item { get; set; }

        public DbSet<Orders> Orders { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<Status> Status { get; set; }

        public DbSet<Users> Users { get; set; }

        public DbSet<VAT> VAT { get; set; }
    }
}