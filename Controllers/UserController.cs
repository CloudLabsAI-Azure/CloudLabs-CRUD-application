using CRUD_application_2.Models;
using System.Linq;
using System.Web.Mvc;
 
namespace CRUD_application_2.Controllers
{
    public class UserController : Controller
    {
        public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();
        // GET: User
        [HttpGet]
        [Route("User")]
        public ActionResult Index()
        {
            // Get all users from the userlist
            var users = userlist;

            // Pass the users to the Index view
            return View(users);
        }

        // GET: User/Details/5
        [HttpGet]
        [Route("User/Details/{id}")]
        public ActionResult Details(int id)
        {
            // Implement the details method here
            User user = userlist.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return HttpNotFound();
            }
        }

        // GET: User/Create
        [HttpGet]
        [Route("User/Create")]
        public ActionResult Create()
        {
            return View(userlist); 
            //Implement the Create method here
        }

        // POST: User/Create
        [HttpPost]
        [Route("User/Create")]
        public ActionResult Create(User user)
        {
            // Add the new user to the userlist
            userlist.Add(user);

            // Redirect to the Index action to display the updated list of users
            return RedirectToAction("Index");
        }

        // GET: User/Edit/5
        [HttpGet]
        [Route("User/Edit/{id}", Name = "EditUser")]
        public ActionResult Edit(int id)
        {
            // Find the user with the provided ID in the userlist
            User existingUser = userlist.FirstOrDefault(u => u.Id == id);

            if (existingUser != null)
            {
                // Pass the existingUser to the Edit view
                return View(existingUser);
            }
            else
            {
                // If no user is found with the provided ID, return a HttpNotFoundResult
                return HttpNotFound();
            }
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User user)
        {
            // Find the user with the provided ID in the userlist
            User existingUser = userlist.FirstOrDefault(u => u.Id == id);

            if (existingUser != null)
            {
                // Update the user's information with the new values
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;


                // Redirect to the Index action to display the updated list of users
                return RedirectToAction("Index");
            }
            else
            {
                // If no user is found with the provided ID, return a HttpNotFoundResult
                return HttpNotFound();
            }
        }

        // GET: User/Delete/5
        [HttpGet]
        [Route("User/Delete/{id}")]
        public ActionResult Delete(int id)
        {
            // Find the user with the provided ID in the userlist
            User user = userlist.FirstOrDefault(u => u.Id == id);

            if (user != null)
            {
                // Pass the user to the Delete view
                return View(user);
            }
            else
            {
                // If no user is found with the provided ID, return a HttpNotFoundResult
                return HttpNotFound();
            }
        }

       

        // POST: User/Delete/5
        [HttpPost]
        [Route("User/Delete/{id}")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            // Find the user with the provided ID in the userlist
            User user = userlist.FirstOrDefault(u => u.Id == id);

            if (user != null)
            {
                // Remove the user from the userlist
                userlist.Remove(user);

                // Redirect to the Index action to display the updated list of users
                return RedirectToAction("Index");
            }
            else
            {
                // If no user is found with the provided ID, return a HttpNotFoundResult
                return HttpNotFound();
            }
        }
    }
}
