﻿using Microsoft.AspNet.Identity;
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
            ApplicationUser user = db.Users.Find(User.Identity.GetUserId());

            List<ShoppingCart> temp = (List<ShoppingCart>)Session["Cart"];
            double totalPrice = temp.Sum(x => x.Product.Price_brutto * x.Quantity);
            
            if(user.Discount.HasValue)
            {
                double discount = totalPrice * user.Discount.Value / 100;
                totalPrice = totalPrice - discount;
            }

            ViewBag.PriceToDisplay = String.Format("{0:N2}", totalPrice);
            if (User.Identity.IsAuthenticated)
            {
                if (user.Price_display.Equals("NETTO"))
                {
                    temp.ForEach(x => {
                        if (x.Product.Vat.Value == null)
                        {
                            x.Product.Vat.Value = 0;
                        }
                    });
                    double? nullableNetto = temp.Sum(x => ((x.Product.Price_brutto / (100 + x.Product.Vat.Value)) * 100) * x.Quantity);
                    double netto = 0;
                    if (nullableNetto.HasValue)
                    {
                        netto = nullableNetto.Value;
                    }

                    if (user.Discount.HasValue)
                    {
                        double discount = netto * user.Discount.Value / 100;
                        netto = netto - discount;
                    }

                    ViewBag.PriceToDisplay = String.Format("{0:N2}", Math.Truncate(netto * 100) / 100);
                    
                }
            }

            List<Payment> paymentTypes = db.Payment.ToList();
            ViewBag.PaymentTypes = new SelectList(paymentTypes, "Id", "Name");

            List<Delivery> deliveryTypes = db.Delivery.ToList();
            ViewBag.DeliveryTypes = new SelectList(deliveryTypes, "Id", "Name");

            Orders order = new Orders()
            {
                User = user,
                UserID = user.Id,
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
                        OrderID = order.Id,
                        ProductID = cart.Product.Id,
                        Quantity = cart.Quantity
                    };
                    db.Order_item.Add(orderItem);
                    db.SaveChanges();
                }
                Session.Remove("Cart");

                Order_details orderDetails = new Order_details()
                {
                    Order = order,
                    StatusID = 1
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
