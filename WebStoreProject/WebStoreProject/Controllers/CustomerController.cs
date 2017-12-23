using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStoreProject.Models;

namespace WebStoreProject.Controllers
{
    [Authorize(Roles = "ROLE_ADMIN")]
    public class CustomerController : Controller
    {
        private DBModel db = new DBModel();

        // GET: Users
        public ActionResult Index()
        {
            List<ApplicationUser> users= db.Users.ToList();
            return View(users);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            ApplicationUser user = db.Users.Find(id);
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        public ActionResult Edit(ApplicationUser user)
        {
            try
            {
                ApplicationUser userToEdit = db.Users.Find(user.Id);
                UpdateModel(userToEdit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(string id)
        {
            ApplicationUser user = db.Users.Find(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Delete(ApplicationUser user)
        {
            try
            {
                ApplicationUser userToDelete = db.Users.Find(user.Id);
                db.Users.Remove(userToDelete);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
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
