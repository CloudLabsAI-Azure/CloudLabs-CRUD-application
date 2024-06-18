using CRUD_application_2.Models;
using System.Linq;
using System.Web.Mvc;
 
namespace CRUD_application_2.Controllers
{
    public class UserController : Controller
    {
        public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();
        // GET: User
        public ActionResult Index()
        {
            // Implement the Index method here
            return View(userlist);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }


        // GET: User/Create
        public ActionResult Create()
        {
            //Implement the Create method here
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            try
            {
                // Add user to the list
                userlist.Add(user);
                // Redirect to the Index view after successful addition
                return RedirectToAction("Index");
            }
            catch
            {
                // Return the same view with the user object in case of an error
                return View(user);
            }
        }


        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }


        // POST: User/Edit/5
        [HttpPost]        
        public ActionResult Edit(int id, User user)
        {
            try
            {
                var userToUpdate = userlist.FirstOrDefault(u => u.Id == id);
                if (userToUpdate == null)
                {
                    return HttpNotFound();
                }

                // Update the user details here
                userToUpdate.Name = user.Name;
                userToUpdate.Email = user.Email;
                userToUpdate.Age = user.Age;

                return RedirectToAction("Index");
            }
            catch
            {
                return View(user);
            }
        }


        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }


        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var userToDelete = userlist.FirstOrDefault(u => u.Id == id);
                if (userToDelete != null)
                {
                    userlist.Remove(userToDelete);
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
