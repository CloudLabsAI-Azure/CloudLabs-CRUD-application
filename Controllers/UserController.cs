using CRUD_application_2.Models;
using System.Web.Mvc;
 
namespace CRUD_application_2.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController()
        {
            _userService = new UserService();
        }

        public ActionResult Index()
        {
            var users = _userService.GetAllUsers();
            return View(users);
        }

        public ActionResult Details(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                _userService.CreateUser(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public ActionResult Edit(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(int id, User user)
        {
            if (ModelState.IsValid)
            {
                _userService.UpdateUser(id, user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public ActionResult Delete(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            _userService.DeleteUser(id);
            return RedirectToAction("Index");
        }
    }
}
