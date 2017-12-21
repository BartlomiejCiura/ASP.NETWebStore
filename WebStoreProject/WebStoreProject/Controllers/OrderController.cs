using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStoreProject.Models;

namespace WebStoreProject.Controllers
{
    public class OrderController : Controller
    {
        private DBModel db;

        public OrderController()
        {
            db = new DBModel();
        }
        // GET: Order
        public ActionResult Index()
        {
            Users user = db.Users.Where(u => u.Email.Equals(User.Identity.Name)).First();

            List<ShoppingCart> temp = (List<ShoppingCart>)Session["Cart"];
            double totalPrice = temp.Sum(x => x.Product.Price_brutto * x.Quantity);

            ViewBag.PriceToDisplay = totalPrice;
            if (User.Identity.IsAuthenticated)
            {
                if (user.Price_display.Equals("NETTO"))
                {
                    temp.ForEach(x => {
                        if (x.Product.VAT.Value == null)
                        {
                            x.Product.VAT.Value = 0;
                        }
                    });
                    double? nullableNetto = temp.Sum(x => ((x.Product.Price_brutto / (100 + x.Product.VAT.Value)) * 100) * x.Quantity);
                    double netto = 0;
                    if (nullableNetto.HasValue)
                    {
                        netto = nullableNetto.Value;
                    }

                    
                    ViewBag.PriceToDisplay = String.Format("{0:N2}", Math.Truncate(netto * 100) / 100);
                    
                }
            }

            Orders order = new Orders()
            {
                User_id = user.Id,
                Users = user,
                Shipment = user.Address,
                Value = totalPrice
            };
            return View(order);
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult Create(Orders order)
        {
            try
            {
                List<ShoppingCart> listCart = (List<ShoppingCart>)Session["Cart"];
                db.Orders.Add(order);
                db.SaveChanges();
                
                foreach(ShoppingCart cart in listCart)
                {
                    Order_item orderItem = new Order_item()
                    {
                        Order_id = order.Id,
                        Product_id = cart.Product.Id,
                        Quantity = cart.Quantity
                    };
                    db.Order_item.Add(orderItem);
                    db.SaveChanges();
                }
                Session.Remove("Cart");

                Order_details orderDetails = new Order_details()
                {
                    Order_id = order.Id,
                    Status_id = 1
                };
                db.Order_details.Add(orderDetails);
                db.SaveChanges();

                // tutaj wyslac na strone ze zamowienie zostalo zlozone i ze liste zamowien mozna zobaczyc np. w profilu
                // i oczywisice zrobic trzeba w profilu mozliwosc zobaczenia zamowien
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            catch
            {
                // Error - Trzeba zrobic strone dla View ze blad wystapil albo jakas ogolna ze blad wystapil
                // bo jak teraz wywali errora gdzies to zwroci ze ta strona nie istnieje :P
                return View();
            }
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Order/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Order/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
