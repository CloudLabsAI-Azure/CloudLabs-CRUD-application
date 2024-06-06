using CRUD_application_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
 
namespace CRUD_application_2.Controllers
{
    public class UserController : Controller
    {
        public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();

        [HttpGet]
        [Route("User")]
        public ActionResult Index(string searchString)
        {
            var users = userlist.AsEnumerable();
            if (!string.IsNullOrEmpty(searchString))
            {
                users = userlist.Where(u => u.Name.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0 || u.Email.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0);
                return View(users.ToList());
            }

            return View(userlist);
        }

        [HttpGet]
        [Route("User/Details/{id}")]
        public ActionResult Details(int id)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return HttpNotFound();

            return View("Details", user);
        }

        [HttpGet]
        [Route("User/Create")]
        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [Route("User/Create")]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                userlist.Add(user);
                return RedirectToAction("Index");
            }

            return View("Create", user);
        }

        [HttpGet]
        [Route("User/Edit/{id}")]
        public ActionResult Edit(int id)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return HttpNotFound();

            return View("Edit", user);
        }

        [HttpPost]
        [Route("User/Edit/{id}")]
        public ActionResult Edit(int id, User user)
        {
            var existingUser = userlist.FirstOrDefault(u => u.Id == id);
            if (existingUser == null)
                return HttpNotFound();

            if (ModelState.IsValid)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;

                return RedirectToAction("Index");
            }

            return View("Edit", user);
        }

        [HttpGet]
        [Route("User/Delete/{id}")]
        public ActionResult Delete(int id)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return HttpNotFound();

            return View("Delete", user);
        }

        [HttpPost]
        [Route("User/Delete/{id}")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return HttpNotFound();

            string name = collection["username"];
            string email = collection["email"];

            userlist.Remove(user);

            return RedirectToAction("Index");
        }
    }
}