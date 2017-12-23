using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStoreProject.Models;

namespace WebStoreProject.Controllers
{
    [Authorize(Roles = "ROLE_ADMIN")]
    public class OrdersAdminController : Controller
    {
        private DBModel db;

        public OrdersAdminController()
        {
            db = new DBModel();
        }
        // GET: OrdersAdmin
        public ActionResult Index()
        {
            List<Order_details> orders = db.Order_details.ToList();
            return View(orders);
        }

        // GET: OrdersAdmin/Details/5
        public ActionResult Details(int id)
        {
            Order_details details = db.Order_details.Find(id);
            return View(details);
        }

        // GET: OrdersAdmin/Edit/5
        public ActionResult Edit(int id)
        {
            Order_details details = db.Order_details.Find(id);

            List<Payment> paymentTypes = db.Payment.ToList();
            ViewBag.PaymentTypes = new SelectList(paymentTypes, "Id", "Name");

            List<Delivery> deliveryTypes = db.Delivery.ToList();
            ViewBag.DeliveryTypes = new SelectList(deliveryTypes, "Id", "Name");

            List<Status> status = db.Status.ToList();
            ViewBag.Status = new SelectList(status, "Id", "Name");

            return View(details);
        }

        // POST: OrdersAdmin/Edit/5
        [HttpPost]
        public ActionResult Edit(Order_details order)
        {
            try
            {
                Order_details orderToEdit = db.Order_details.Find(order.Id);
                UpdateModel(orderToEdit);
                db.SaveChanges();

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
