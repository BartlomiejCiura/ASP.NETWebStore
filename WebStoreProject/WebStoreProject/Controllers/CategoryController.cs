using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStoreProject.Models;


namespace WebStoreProject.Controllers
{
    public class CategoryController : Controller
    {
       
        // GET: Catergory
        public ActionResult Index()
        {
            using (DBModel db = new DBModel())
            {
                List<Category> categories = db.Category.ToList();
                return View(categories);
            }
        }

        // GET: Catergory/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Catergory/Create
        public ActionResult Create()
        {
            using (DBModel db = new DBModel())
            {
                List<Category> categories = db.Category.ToList();
                ViewBag.Categories = new SelectList(categories, "Id", "Name");
                return View();
            }
        }

        // POST: Catergory/Create
        [HttpPost]
        public ActionResult Create(Category category)
        {
            try
            {
                // TODO: Add insert logic here
                using(DBModel db = new DBModel())
                {

                    db.Category.Add(category);
                    db.SaveChanges();
                        
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Catergory/Edit/5
        public ActionResult Edit(int id)
        {
            using (DBModel db = new DBModel())
            {
                List<Category> categories = db.Category.ToList();
                Category category = db.Category.Find(id);

                ViewBag.Categories = new SelectList(categories, "Id", "Name");

                return View(category);
            }
        }

        // POST: Catergory/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                using (DBModel db = new DBModel())
                {
                    Category category = db.Category.Find(id);
                    UpdateModel(category);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Catergory/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Catergory/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                using (DBModel db = new DBModel())
                {
                    Category category = db.Category.Find(id);
                    db.Category.Remove(category);
                    db.SaveChanges();

                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
