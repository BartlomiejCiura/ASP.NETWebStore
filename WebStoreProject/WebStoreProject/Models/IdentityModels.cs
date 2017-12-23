using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebStoreProject.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        public string Price_display { get; set; }

        public int? Discount { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }

    public class DBModel : IdentityDbContext<ApplicationUser>
    {
        public DBModel()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static DBModel Create()
        {
            return new DBModel();
        }

        public DbSet<Category> Category { get; set; }

        public DbSet<Order_details> Order_details { get; set; }

        public DbSet<Order_item> Order_item { get; set; }

        public DbSet<Orders> Orders { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<Status> Status { get; set; }

        public DbSet<VAT> VAT { get; set; }

        public DbSet<Payment> Payment { get; set; }

        public DbSet<Delivery> Delivery { get; set; }
    }
}