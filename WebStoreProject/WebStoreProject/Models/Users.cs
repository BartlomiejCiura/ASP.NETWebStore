namespace WebStoreProject.Models
{

    using System;
    using System.Collections.Generic;

    public partial class Users
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {

            Orders = new HashSet<Orders>();

        }


        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Price_display { get; set; }

        public int? Discount { get; set; }

        public string Role { get; set; }



        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<Orders> Orders { get; set; }

    }

}
