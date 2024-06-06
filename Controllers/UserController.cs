using CRUD_application_2.Models;
using System.Linq;
using System.Web.Mvc;
 
namespace CRUD_application_2.Controllers
{
    public class UserController : Controller
    {
        public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();

        /// <summary>
        /// Retrieves the list of users and displays them in the Index view.
        /// </summary>
        /// <returns>The Index view with the list of users.</returns>
        public ActionResult Index()
        {
            return View(userlist);
        }

        /// <summary>
        /// Retrieves the details of a specific user based on the provided ID and displays them in the Details view.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The Details view with the details of the user.</returns>
        public ActionResult Details(int id)
        {
            return View(userlist.FirstOrDefault(x => x.Id == id));
        }

        /// <summary>
        /// Displays the view to create a new user.
        /// </summary>
        /// <returns>The Create view.</returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Handles the HTTP POST request to create a new user.
        /// </summary>
        /// <param name="user">The user object containing the information of the new user.</param>
        /// <returns>Redirects to the Index action to display the updated list of users.</returns>
        [HttpPost]
        public ActionResult Create(User user)
        {
            userlist.Add(user);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Displays the view to edit an existing user with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the user to edit.</param>
        /// <returns>The Edit view with the user to edit.</returns>
        public ActionResult Edit(int id)
        {
            User user = userlist.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        /// <summary>
        /// Handles the HTTP POST request to update an existing user with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="user">The user object containing the updated information.</param>
        /// <returns>Redirects to the Index action to display the updated list of users.</returns>
        [HttpPost]
        public ActionResult Edit(int id, User user)
        {
            User existingUser = userlist.FirstOrDefault(x => x.Id == id);
            if (existingUser == null)
            {
                return HttpNotFound();
            }
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Displays the view to delete an existing user with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>The Delete view with the user to delete.</returns>
        public ActionResult Delete(int id)
        {
            User user = userlist.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        /// <summary>
        /// Handles the HTTP POST request to delete an existing user with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <param name="collection">The form collection.</param>
        /// <returns>Redirects to the Index action to display the updated list of users.</returns>
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            User user = userlist.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            userlist.Remove(user);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Retrieves the list of users that match the provided search term and displays them in the Search view.
        /// </summary>
        /// <param name="searchTerm">The search term to match against user names.</param>
        /// <returns>The Search view with the list of matching users.</returns>
        public ActionResult Search(string searchTerm)
        {
            var searchResults = userlist.Where(user => user.Name.Contains(searchTerm)).ToList();
            return View(searchResults);
        }
    }
}
