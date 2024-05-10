using CRUD_application_2.Controllers;
using CRUD_application_2.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Web.Mvc;


namespace Contact_Database_Application_Test
{
    [TestFixture]
    public class UserControllerTest
    {
        private UserController _controller;
        private List<User> _userList;

        [SetUp]
        public void Setup()
        {
            _userList = new List<User>
                {
                    new User { Id = 1, Name = "User1", Email = "user1@example.com" },
                    new User { Id = 2, Name = "User2", Email = "user2@example.com" }
                };

            UserController.userlist = _userList;
            _controller = new UserController();
        }

        [Test]
        public void Index_ReturnsCorrectView_WithAllUsers()
        {
            var result = _controller.Index() as ViewResult;

            Assert.That(result, Is.Not.Null);
            var model = result.Model as List<User>;
            Assert.AreEqual(2, model.Count);
        }

        [Test]
        public void Details_ReturnsCorrectView_WithUser()
        {
            var result = _controller.Details(1) as ViewResult;

            Assert.That(result, Is.Not.Null);
            var model = result.Model as User;
            Assert.AreEqual("User1", model.Name);
        }

        [Test]
        public void Details_ReturnsHttpNotFound_ForInvalidId()
        {
            var result = _controller.Details(999);

            Assert.That(result, Is.InstanceOf<HttpNotFoundResult>());
        }

        [Test]
        public void Create_RedirectsToIndex_AfterSuccessfulCreation()
        {
            var newUser = new User { Name = "User3", Email = "user3@example.com" };

            var result = _controller.Create(newUser) as RedirectToRouteResult;

            Assert.That(result, Is.Not.Null);            
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(3, UserController.userlist.Count);
        }

        [Test]
        public void Create_ReturnsSameView_WhenModelStateIsInvalid()
        {
            _controller.ModelState.AddModelError("Name", "Name is required");

            var newUser = new User { Email = "user3@example.com" };

            var result = _controller.Create(newUser) as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.AreEqual(newUser, result.Model);
        }

        [Test]
        public void Edit_ReturnsCorrectView_WithUser()
        {
            var result = _controller.Edit(1) as ViewResult;

            Assert.That(result, Is.Not.Null);
            var model = result.Model as User;
            Assert.AreEqual("User1", model.Name);
        }

        [Test]
        public void Edit_ReturnsHttpNotFound_ForInvalidId()
        {
            var result = _controller.Edit(999);

            Assert.That(result, Is.InstanceOf<HttpNotFoundResult>());
        }

        [Test]
        public void Edit_RedirectsToIndex_AfterSuccessfulEdit()
        {
            var editedUser = new User { Id = 1, Name = "EditedUser1", Email = "editeduser1@example.com" };

            var result = _controller.Edit(editedUser.Id, editedUser) as RedirectToRouteResult;

            Assert.That(result, Is.Not.Null);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("EditedUser1", UserController.userlist.Find(u => u.Id == 1).Name);
        }

        [Test]
        public void Edit_ReturnsSameView_WhenModelStateIsInvalid()
        {
            _controller.ModelState.AddModelError("Name", "Name is required");

            var editedUser = new User { Id = 1, Email = "editeduser1@example.com" };

            var result = _controller.Edit(editedUser.Id, editedUser) as ViewResult;

            Assert.That(result, Is.Not.Null);
            Assert.AreEqual(editedUser, result.Model);
        }

        [Test]
        public void Delete_ReturnsCorrectView_WithUser()
        {
            var result = _controller.Delete(1) as ViewResult;

            Assert.That(result, Is.Not.Null);
            var model = result.Model as User;
            Assert.AreEqual("User1", model.Name);
        }

        [Test]
        public void Delete_ReturnsHttpNotFound_ForInvalidId()
        {
            var result = _controller.Delete(999);

            Assert.That(result, Is.InstanceOf<HttpNotFoundResult>());
        }

        [Test]
        public void Delete_RedirectsToIndex_AfterSuccessfulDeletion()
        {
            var result = _controller.Delete(1, null) as RedirectToRouteResult;

            Assert.That(result, Is.Not.Null);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(1, UserController.userlist.Count);
        }

    }
}
