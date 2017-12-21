using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebStoreProject.Models;


namespace WebStoreProject.Controllers
{
    public class CategoryController : Controller
    {

        private DBModel db;

        public CategoryController()
        {
            db = new DBModel();
        }
       
        // GET: Catergory
        public ActionResult Index()
        {
            List<Category> categories = db.Category.ToList();
            return View(categories);
        }

        // GET: Catergory/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Catergory/Create
        public ActionResult Create()
        {
            List<Category> categories = db.Category.ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }

        // POST: Catergory/Create
        [HttpPost]
        public ActionResult Create(Category category, FormCollection form)
        {
            try
            {
                db.Category.Add(category);
                db.SaveChanges();    
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
            List<Category> categories = db.Category.ToList();
            Category category = db.Category.Find(id);
            categories.Remove(category);
            categories.Insert(0, null);
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View(category);
        }

        // POST: Catergory/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                Category category = db.Category.Find(id);
                UpdateModel(category);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Catergory/Delete/5
        public ActionResult Delete(int id)
        {
            ViewBag.Error = false;
            Category category = db.Category.Find(id);
            return View(category);
        }

        // POST: Catergory/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                
                Category category = db.Category.Find(id);
                db.Category.Remove(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                Category category = db.Category.Find(id);
                ViewBag.Error = true;
                ViewBag.ErrorMsg = "Some error occured! Contact with administration!";
                return View(category);
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
