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
            return View();
        }

        // GET: OrdersAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrdersAdmin/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: OrdersAdmin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrdersAdmin/Edit/5
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

        // GET: OrdersAdmin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrdersAdmin/Delete/5
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
    }
}
