using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStoreProject.Models;

namespace WebStoreProject.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private DBModel db;
        private string strCart = "Cart";

        public ShoppingCartController()
        {
            db = new DBModel();
        }

        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Delete(int id)
        {
            int position = GetProductIndex(id);
            List<ShoppingCart> listCart = (List<ShoppingCart>)Session[strCart];
            listCart.RemoveAt(position);
            return View("Index");
        }

        public ActionResult OrderNow(int id)
        {
            if(Session[strCart] == null)
            {
                List<ShoppingCart> listCart = new List<ShoppingCart>
                {
                    new ShoppingCart(db.Product.Find(id), 1)
                };
                Session[strCart] = listCart;
            }
            else
            {
                List<ShoppingCart> listCart = (List<ShoppingCart>)Session[strCart];
                if(IsProductAdded(id))
                {
                    int position = GetProductIndex(id);
                    listCart[position].Quantity++;
                } else
                {
                    listCart.Add(new ShoppingCart(db.Product.Find(id), 1));
                }
                Session[strCart] = listCart;
            }
            return View("Index");
        }

        private bool IsProductAdded(int id)
        {
            List<ShoppingCart> listCart = (List<ShoppingCart>)Session[strCart];
            foreach(ShoppingCart sc in listCart) 
            {
                if(sc.Product.Id == id)
                {
                    return true;
                }
            }
            return false;
        }

        private int GetProductIndex(int id)
        {
            List<ShoppingCart> listCart = (List<ShoppingCart>)Session[strCart];
            for(int i=0; i<listCart.Count; i++)
            {
                if (listCart[i].Product.Id == id) return i;
            }
            return -1;
        }

        public ActionResult Summary(FormCollection frc)
        {
            string[] quantities = frc.GetValues("cart.Quantity");
            List<ShoppingCart> listCart = (List<ShoppingCart>)Session[strCart];
            for(int i=0; i<listCart.Count; i++)
            {
                listCart[i].Quantity = Convert.ToInt32(quantities[i]);
            }

            Session[strCart] = listCart;
            ViewBag.Modal = true;

            return View("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
