using Microsoft.VisualStudio.TestTools.UnitTesting;
using CRUD_application_2.Controllers;
using CRUD_application_2.Models;
using System.Web.Mvc;
using System.Linq;

namespace CRUD_application_2.Tests.Controllers
{
    [TestClass]
    public class UserControllerTest
    {
        private UserController controller;

        [TestInitialize]
        public void Setup()
        {
            // Initialize UserController and any necessary data before each test
            controller = new UserController();
            UserController.userlist.Clear(); // Ensure userlist is empty before each test
        }

        [TestMethod]
        public void Index_ReturnsViewWithUsers()
        {
            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result.Model); // Ensure model is not null
            Assert.IsTrue(((System.Collections.Generic.List<User>)result.Model).Count > 0); // Check that model contains users
        }

        [TestMethod]
        public void Details_UserExists_ReturnsViewWithUser()
        {
            // Arrange
            UserController.userlist.Add(new User { Id = 1, Name = "Test User" });

            // Act
            var result = controller.Details(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result.Model);
            Assert.AreEqual(1, ((User)result.Model).Id);
        }

        [TestMethod]
        public void Details_UserDoesNotExist_ReturnsHttpNotFound()
        {
            // Act
            var result = controller.Details(99);

            // Assert
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void Create_Post_ValidUser_RedirectsToIndex()
        {
            // Arrange
            var user = new User { Id = 1, Name = "New User" };

            // Act
            var result = controller.Create(user) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Edit_Post_ValidUser_RedirectsToIndex()
        {
            // Arrange
            UserController.userlist.Add(new User { Id = 1, Name = "Existing User" });
            var updatedUser = new User { Id = 1, Name = "Updated User" };

            // Act
            var result = controller.Edit(1, updatedUser) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Updated User", UserController.userlist.First(u => u.Id == 1).Name);
        }

        [TestMethod]
        public void Delete_Post_ValidUser_RedirectsToIndex()
        {
            // Arrange
            UserController.userlist.Add(new User { Id = 1, Name = "User to Delete" });

            // Act
            var result = controller.Delete(1, null) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.IsFalse(UserController.userlist.Any(u => u.Id == 1));
        }
    }
}
