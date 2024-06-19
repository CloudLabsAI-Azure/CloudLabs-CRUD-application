using NUnit.Framework;
using Moq;
using System.Web.Mvc;
using CRUD_application_2.Controllers;
using CRUD_application_2.Models;
using System.Collections.Generic;
using System.Linq;

namespace CRUD_application_2.Tests.Controllers
{
    [TestFixture]
    public class UserControllerTests
    {
        private UserController controller;
        private List<User> users;

        [SetUp]
        public void Setup()
        {
            // Initialize UserController and a test user list before each test
            controller = new UserController();
            users = new List<User>
            {
                new User { Id = 1, Name = "John Doe", Email = "john@example.com" },
                new User { Id = 2, Name = "Jane Doe", Email = "jane@example.com" }
            };

            // Simulate the static user list in UserController
            UserController.userlist = users;
        }

        [Test]
        public void Index_ReturnsViewWithUsers()
        {
            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            var model = result.Model as List<User>;
            Assert.AreEqual(2, model.Count);
        }

        [Test]
        public void Details_UserExists_ReturnsViewWithUser()
        {
            // Act
            var result = controller.Details(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            var user = result.Model as User;
            Assert.AreEqual("John Doe", user.Name);
        }

        [Test]
        public void Details_UserDoesNotExist_ReturnsHttpNotFound()
        {
            // Act
            var result = controller.Details(999);

            // Assert
            Assert.IsInstanceOf<HttpNotFoundResult>(result);
        }

        [Test]
        public void Create_PostAddsUserAndRedirects()
        {
            // Arrange
            var newUser = new User { Id = 3, Name = "New User", Email = "newuser@example.com" };

            // Act
            var result = controller.Create(newUser) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(3, UserController.userlist.Count);
        }

        // Additional tests for Edit, Delete, etc. can be implemented similarly
    }
}
