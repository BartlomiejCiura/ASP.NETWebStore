using System;
using System.Collections.Generic;
using System.IO;
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
                ApplicationUser user = db.Users.Where(u => u.Email.Equals(User.Identity.Name)).First();
                ViewBag.PriceDisplay = user.Price_display;

                if (user.Price_display.Equals("NETTO"))
                {
                    products.ForEach(p => {
                        int abc = 0;
                        if(p.Vat != null)
                        {
                            abc = p.Vat.Value.Value;
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
        public ActionResult Create(Product product, HttpPostedFileBase LogoImageFile, HttpPostedFileBase DetailsImageFile)
        {
            try
            {
                string logoFileName = Path.GetFileNameWithoutExtension(LogoImageFile.FileName);
                string logoExtension = Path.GetExtension(LogoImageFile.FileName);
                logoFileName = logoFileName + DateTime.Now.ToString("yymmssfff") + logoExtension;
                string logoImgPath = "~/Images/" + logoFileName;
                logoFileName = Path.Combine(Server.MapPath("~/Images/"), logoFileName);

                string detailsFileName = Path.GetFileNameWithoutExtension(DetailsImageFile.FileName);
                string detailsExtension = Path.GetExtension(DetailsImageFile.FileName);
                detailsFileName = detailsFileName + DateTime.Now.ToString("yymmssfff") + detailsExtension;
                string detailsImgPath = "~/Images/" + detailsFileName;
                detailsFileName = Path.Combine(Server.MapPath("~/Images/"), detailsFileName);

                LogoImageFile.SaveAs(logoFileName);
                DetailsImageFile.SaveAs(detailsFileName);

                product.LogoImagePath = logoImgPath;
                product.DetailsImagePath = detailsImgPath;
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
        public ActionResult Edit(Product product, HttpPostedFileBase LogoImageFile, HttpPostedFileBase DetailsImageFile)
        {
            try
            {
                Product productToEdit = db.Product.Find(product.Id);

                if(LogoImageFile != null)
                {
                    string logoFileName = Path.GetFileNameWithoutExtension(LogoImageFile.FileName);
                    string logoExtension = Path.GetExtension(LogoImageFile.FileName);
                    logoFileName = logoFileName + DateTime.Now.ToString("yymmssfff") + logoExtension;
                    string logoImgPath = "~/Images/" + logoFileName;
                    logoFileName = Path.Combine(Server.MapPath("~/Images/"), logoFileName);
                    LogoImageFile.SaveAs(logoFileName);
                    productToEdit.LogoImagePath = logoImgPath;
                }

                if(DetailsImageFile != null)
                {
                    string detailsFileName = Path.GetFileNameWithoutExtension(DetailsImageFile.FileName);
                    string detailsExtension = Path.GetExtension(DetailsImageFile.FileName);
                    detailsFileName = detailsFileName + DateTime.Now.ToString("yymmssfff") + detailsExtension;
                    string detailsImgPath = "~/Images/" + detailsFileName;
                    detailsFileName = Path.Combine(Server.MapPath("~/Images/"), detailsFileName);
                    DetailsImageFile.SaveAs(detailsFileName);
                    productToEdit.DetailsImagePath = detailsImgPath;
                }

                TryUpdateModel(productToEdit);
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
