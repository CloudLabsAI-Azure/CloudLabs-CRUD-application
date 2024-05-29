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
            return View(userlist);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            // Find the user in the userlist
            var user = userlist.FirstOrDefault(u => u.Id == id);

            // If the user wasn't found, return a HttpNotFoundResult
            if (user == null)
            {
                return HttpNotFound();
            }

            // If the user was found, pass it to the view
            return View(user);
        }


        // GET: User/Create
        // This method displays the form
        public ActionResult Create()
        {
            return View();
        }

        // This method handles the form submission
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                // Add the user to the list
                userlist.Add(user);

                // Redirect the user to the index page
                return RedirectToAction("Index");
            }

            // If the model state isn't valid, redisplay the form
            return View(user);
        }

        // GET: User/Edit/5
        // Edit methods
        public ActionResult Edit(int id)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = userlist.FirstOrDefault(u => u.Id == user.Id);
                if (existingUser != null)
                {
                    // Update the existing user
                    existingUser.Name = user.Name;
                    existingUser.Email = user.Email;
                    // Add other properties as needed

                    return RedirectToAction("Index");
                }
            }
            return View(user);
        }

        // Delete methods
        public ActionResult Delete(int id)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                userlist.Remove(user);
            }
            return RedirectToAction("Index");
        }

    }
}
