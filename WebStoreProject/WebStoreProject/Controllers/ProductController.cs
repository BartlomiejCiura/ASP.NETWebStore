using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStoreProject.Models;

namespace WebStoreProject.Controllers
{
    public class ProductController : Controller
    {
        private DBModel db;

        public ProductController()
        {
            db = new DBModel();
        }

        // GET: Product
        public ActionResult Index()
        {
            List<Product> products = db.Product.ToList();

            if(User.Identity.IsAuthenticated)
            {
                Users user = db.Users.Where(u => u.Email.Equals(User.Identity.Name)).First();
                ViewBag.PriceDisplay = user.Price_display;

                if (user.Price_display.Equals("NETTO"))
                {
                    products.ForEach(x => {
                        if (x.VAT.Value == null)
                        {
                            x.VAT.Value = 0;
                        }
                    });

                    products.ForEach(p => {
                        int abc = 0;
                        if(!p.VAT.Value.HasValue)
                        {
                            p.VAT.Value = 0;

                        }
                        if(p.VAT.Value.HasValue)
                        {
                            abc = p.VAT.Value.Value;
                        }
                        
                        p.Price_brutto = (p.Price_brutto / (100 + abc)) * 100;
                    });
                    products.ForEach(p => p.Price_brutto = Math.Truncate(p.Price_brutto * 100) / 100);
                }
            }

            return View(products);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            Product product = db.Product.Find(id);
            return View(product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            List<VAT> vatList = db.VAT.ToList();
            ViewBag.VatList = new SelectList(vatList, "Id", "Value");

            List<Category> categories = db.Category.ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            try
            {
                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        [Authorize(Roles = "ROLE_ADMIN")]
        public ActionResult Edit(int id)
        {
            Product product = db.Product.Find(id);

            List<VAT> vatList = db.VAT.ToList();
            ViewBag.VatList = new SelectList(vatList, "Id", "Value");

            List<Category> categories = db.Category.ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [Authorize(Roles = "ROLE_ADMIN")]
        public ActionResult Edit(Product product)
        {
            try
            {
                Product productToEdit = db.Product.Find(product.Id);
                UpdateModel(productToEdit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        [Authorize(Roles = "ROLE_ADMIN")]
        public ActionResult Delete(int id)
        {
            ViewBag.Error = false;
            Product product = db.Product.Find(id);
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost]
        [Authorize(Roles = "ROLE_ADMIN")]
        public ActionResult Delete(Product product)
        {
            try
            {
                  Product productToDelete = db.Product.Find(product.Id);
                  db.Product.Remove(productToDelete);
                  db.SaveChanges();
                  return RedirectToAction("Index");
            }
            catch
            {
                Product productToDelete = db.Product.Find(product.Id);
                ViewBag.Error = true;
                ViewBag.ErrorMsg = "Some error occured! Contact with administration!";
                return View(productToDelete);
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
