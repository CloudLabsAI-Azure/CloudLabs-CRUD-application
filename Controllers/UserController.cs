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
            return View(userlist.FirstOrDefault(x => x.Id == id));
        }
 
        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }
 
        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            // This method is responsible for handling the HTTP POST request to create a new user.
            // It receives user input from the form submission and adds the new user to the userlist.
            // If successful, it redirects to the Index action to display the updated list of users.
            // If an error occurs during the process, it returns the Create view to display any validation errors.

            user.Id = userlist.Count + 1;
            userlist.Add(user);
            return View(user);
        }
 
        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            // This method is responsible for displaying the view to edit an existing user with the specified ID.
            // It retrieves the user from the userlist based on the provided ID and passes it to the Edit view.

            var user = userlist.FirstOrDefault(x => x.Id == id);
            return View(user);
        }
 
        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User user)
        {
            // This method is responsible for handling the HTTP POST request to update an existing user with the specified ID.
            // It receives user input from the form submission and updates the corresponding user's information in the userlist.
            // If successful, it redirects to the Index action to display the updated list of users.
            // If no user is found with the provided ID, it returns a HttpNotFoundResult.
            // If an error occurs during the process, it returns the Edit view to display any validation errors.

            user.Id = id;
            var userIndex = userlist.FindIndex(x => x.Id == id);
            userlist[userIndex] = user;

            if (userlist[userIndex] == null)
            {
                return HttpNotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(user);
            }

            return RedirectToAction("Index");
        
        }
 
        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            // Remove the user
            var user = userlist.FirstOrDefault(x => x.Id == id);
            userlist.Remove(user);
            return RedirectToAction("Index");
        }
 
        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            // Delete the user from the FormCollection

            var user = userlist.FirstOrDefault(x => x.Id == id);
            userlist.Remove(user);
            return RedirectToAction("Index");

        }
    }
}
