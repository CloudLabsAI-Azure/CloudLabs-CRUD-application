using CRUD_application_2.Controllers;
using CRUD_application_2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CRUD_application_2.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        private List<User> userlist;

        [TestInitialize]
        public void Initialize()
        {
            userlist = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "john@example.com" },
                new User { Id = 2, Name = "Jane", Email = "jane@example.com" },
                new User { Id = 3, Name = "Bob", Email = "bob@example.com" }
            };
        }

        [TestMethod]
        public void Details_ReturnsViewWithUser()
        {
            // Arrange
            var controller = new UserController();
            var userlist = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "john@example.com" },
                new User { Id = 2, Name = "Jane", Email = "jane@example.com" }
            };
            UserController.userlist = userlist;

            // Act
            var result = controller.Details(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(User));
            var model = result.Model as User;
            Assert.AreEqual(1, model.Id);
            Assert.AreEqual("John", model.Name);
            Assert.AreEqual("john@example.com", model.Email);
        }

        [TestMethod]
        public void Details_ReturnsHttpNotFoundWhenUserNotFound()
        {
            // Arrange
            var controller = new UserController();
            var userlist = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "john@example.com" },
                new User { Id = 2, Name = "Jane", Email = "jane@example.com" }
            };
            UserController.userlist = userlist;

            // Act
            var result = controller.Details(3) as HttpNotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create_ValidUser_RedirectsToIndex()
        {
            // Arrange
            var controller = new UserController();
            var user = new User
            {
                Name = "John Doe",
                Email = "johndoe@example.com"
            };

            // Act
            var result = controller.Create(user) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Create_InvalidUser_ReturnsView()
        {
            // Arrange
            var controller = new UserController();
            var user = new User();

            // Act
            var result = controller.Create(user) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void Edit_Get_ReturnsViewWithExistingUser()
        {
            // Arrange
            var controller = new UserController();
            var userlist = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "john@example.com" },
                new User { Id = 2, Name = "Jane", Email = "jane@example.com" }
            };
            UserController.userlist = userlist;

            // Act
            var result = controller.Edit(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Edit", result.ViewName);
            var model = result.Model as User;
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.Id);
            Assert.AreEqual("John", model.Name);
            Assert.AreEqual("john@example.com", model.Email);
        }

        [TestMethod]
        public void Edit_Post_UpdatesExistingUserAndRedirectsToIndex()
        {
            // Arrange
            var controller = new UserController();
            var userlist = new List<User>
            {
                new User { Id = 1, Name = "John", Email = "john@example.com" },
                new User { Id = 2, Name = "Jane", Email = "jane@example.com" }
            };
            UserController.userlist = userlist;
            var updatedUser = new User { Id = 1, Name = "Updated John", Email = "updatedjohn@example.com" };

            // Act
            var result = controller.Edit(1, updatedUser) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            var existingUser = userlist.FirstOrDefault(u => u.Id == 1);
            Assert.IsNotNull(existingUser);
            Assert.AreEqual("Updated John", existingUser.Name);
            Assert.AreEqual("updatedjohn@example.com", existingUser.Email);
        }

        [TestMethod]
        public void Delete_Get_ValidId_ReturnsView()
        {
            // Arrange
            var controller = new UserController();
            UserController.userlist = userlist;

            // Act
            var result = controller.Delete(1) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Delete_Get_InvalidId_ReturnsHttpNotFound()
        {
            // Arrange
            var controller = new UserController();
            UserController.userlist = userlist;

            // Act
            var result = controller.Delete(4) as HttpNotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Delete_Post_ValidId_ReturnsRedirectToAction()
        {
            // Arrange
            var controller = new UserController();
            UserController.userlist = userlist;

            // Act
            var result = controller.Delete(2, new FormCollection()) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Delete_Post_InvalidId_ReturnsHttpNotFound()
        {
            // Arrange
            var controller = new UserController();
            UserController.userlist = userlist;

            // Act
            var result = controller.Delete(4, new FormCollection()) as HttpNotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
