using CRUD_application_2.Models;
using System.Linq;
using System.Web.Mvc;
using WebGrease.Activities;

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
            var user = userlist.FirstOrDefault(x => x.Id == id);
            if(user == null)
            {
                return HttpNotFound();
            }
            return View(userlist.FirstOrDefault(x => x.Id == id));
        }
 
        // GET: User/Create
        public ActionResult Create()
        {
            return View(new User());
        }
 
        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            // Implement the Create method (POST) here
            if (ModelState.IsValid)
            {
                userlist.Add(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }
 
        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            // This method is responsible for displaying the view to edit an existing user with the specified ID.
            // It retrieves the user from the userlist based on the provided ID and passes it to the Edit view.
            // If no user is found with the provided ID, it returns a HttpNotFoundResult.
            var user = userlist.FirstOrDefault(x => x.Id == id);
            if(user == null)
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

            try
            {
                // It receives user input from the form submission and updates the corresponding user's information in the userlist.
                var updateUser = userlist.FirstOrDefault(x => x.Id == id);
                // If successful, it redirects to the Index action to display the updated list of users.
                if (updateUser != null)
                {
                    updateUser.Name = user.Name;
                    updateUser.Email = user.Email;
                    return RedirectToAction("Index");
                }
                // If no user is found with the provided ID, it returns a HttpNotFoundResult.
                return HttpNotFound();
            }
            // If an error occurs during the process, it returns the Edit view to display any validation errors.
            catch (System.Exception)
            {
                // log error exception
                
                return RedirectToAction("Edit", id);
            }
 
            
        }
 
        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            // Implement the Delete method here
            var user = userlist.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            userlist.Remove(user);
            return RedirectToAction("Index");
        }
 
        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            // Implement the Delete method (POST) here
            if(collection != null)
            {
                collection.Remove("id");
                return RedirectToAction("Index");
            }
            return HttpNotFound();

        }
    }
}
