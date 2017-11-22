using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebStoreProject.Models
{
    public class ShoppingCart
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public ShoppingCart(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
    }
}