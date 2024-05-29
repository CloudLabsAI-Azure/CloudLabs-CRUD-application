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
        [HttpGet]
        [Route("user/details{id}")]
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
        [HttpGet]
        [Route("User/Create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                userlist.Add(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }


        // GET: User/Edit/5
        [HttpGet]
        [Route("User/Edit{id}")]
        public ActionResult Edit(int id)
        {
            // This method is responsible for displaying the view to edit an existing user with the specified ID.
            // It retrieves the user from the userlist based on the provided ID and passes it to the Edit view.
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
 
        // POST: User/Edit/5
        [HttpPost]
        [Route("User/Edit{id}")]
        public ActionResult Edit(int id, User user)
        {
            // This method is responsible for handling the HTTP POST request to update an existing user with the specified ID.
            // It receives user input from the form submission and updates the corresponding user's information in the userlist.
            // If successful, it redirects to the Index action to display the updated list of users.
            // If no user is found with the provided ID, it returns a HttpNotFoundResult.
            // If an error occurs during the process, it returns the Edit view to display any validation errors.
            var existingUser = userlist.FirstOrDefault(u => u.Id == id);
            if (existingUser == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                // Add other properties as needed
                return RedirectToAction("Index");
            }
            return View(user);

        }

        // GET: User/Delete/5
        [HttpGet]
        [Route("User/Search{id}")]
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
        [Route("User/Delete{id}")]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            userlist.Remove(user);
            return RedirectToAction("Index");
        }

        // GET: User/Search
        [HttpGet]
        [Route("User/Search/{name}")]
        public ActionResult Search(string name)
        {
            var users = userlist.Where(u => u.Name.Contains(name)).ToList();
            return View("Index", users);
        }
    }
}
