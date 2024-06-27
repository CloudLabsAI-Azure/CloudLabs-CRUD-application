using CRUD_application_2.Models;
using System;
using System.Linq;
using System.Web.Mvc;
 
namespace CRUD_application_2.Controllers
{
    public class UserController : Controller
    {
        public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();

        public ActionResult Index(string searchString)
        {
            var users = from u in userlist
                        select u;

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.Name.Contains(searchString));
            }

            return View(users.ToList());
        }

     
 
        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            // Find the user by ID
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
            // Return the Create view
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            try
            {
                // Add the new user to the list
                userlist.Add(user);
                return RedirectToAction("Index");
            }
            catch
            {
                // If an error occurs, return the Create view again
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            // This method is responsible for displaying the view to edit an existing user with the specified ID.
            // It retrieves the user from the userlist based on the provided ID and passes it to the Edit view.
            // If no user is found with the provided ID, it returns a HttpNotFoundResult.
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
            // This method is responsible for handling the HTTP POST request to update an existing user with the specified ID.
            // It receives user input from the form submission and updates the corresponding user's information in the userlist.
            // If successful, it redirects to the Index action to display the updated list of users.
            // If no user is found with the provided ID, it returns a HttpNotFoundResult.
            // If an error occurs during the process, it returns the Edit view to display any validation errors.
            var existingUser = userlist.FirstOrDefault(u => u.Id == id);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                // Update other properties as needed
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            // Find the user by ID
            var userToDelete = userlist.FirstOrDefault(u => u.Id == id);
            if (userToDelete == null)
            {
                return HttpNotFound();
            }
            // Pass the user to the Delete view for confirmation
            return View(userToDelete);
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            // Find the user by ID
            var userToDelete = userlist.FirstOrDefault(u => u.Id == id);
            if (userToDelete == null)
            {
                return HttpNotFound();
            }
            // Remove the user from the list
            userlist.Remove(userToDelete);
            // Redirect to the Index action to display the updated list of users
            return RedirectToAction("Index");
        }
    }
}
