using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRUD_application_2.Controllers;
using CRUD_application_2.Models;
using Moq;
using NUnit.Framework;
using System.Web.Mvc;
using System.Collections.Generic;

namespace CRUD_application_2.UnitTests
{
    [TestFixture]
    public class UserControllerTests
    {
        private UserController _controller;
        private List<User> _userList;

        [SetUp]
        public void Setup()
        {
            _userList = new List<User>
            {
                new User { Id = 1, Name = "Test User 1", Email = "test1@example.com" },
                new User { Id = 2, Name = "Test User 2", Email = "test2@example.com" }
            };

            UserController.userlist = _userList;
            _controller = new UserController();
        }

        [Test]
        public void Index_ReturnsViewWithUserList()
        {
            var result = _controller.Index() as ViewResult;
            var model = result.Model as List<User>;

            Assert.AreEqual(_userList, model);
        }

        [Test]
        public void Details_ReturnsUserDetailsView()
        {
            var result = _controller.Details(1) as ViewResult;
            var model = result.Model as User;

            Assert.AreEqual(_userList.First(), model);
        }

        [Test]
        public void Create_ReturnsView()
        {
            var result = _controller.Create() as ViewResult;

            Assert.IsNotNull(result);
        }

        [Test]
        public void Create_Post_AddsUserAndRedirects()
        {
            var newUser = new User { Id = 3, Name = "Test User 3", Email = "test3@example.com" };
            var result = _controller.Create(newUser) as RedirectToRouteResult;

            Assert.IsTrue(UserController.userlist.Contains(newUser));
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [Test]
        public void Edit_ReturnsUserEditView()
        {
            var result = _controller.Edit(1) as ViewResult;
            var model = result.Model as User;

            Assert.AreEqual(_userList.First(), model);
        }

        [Test]
        public void Edit_Post_UpdatesUserAndRedirects()
        {
            var updatedUser = new User { Id = 1, Name = "Updated User", Email = "updated@example.com" };
            var result = _controller.Edit(updatedUser) as RedirectToRouteResult;

            var user = UserController.userlist.First(u => u.Id == 1);

            Assert.AreEqual(updatedUser.Name, user.Name);
            Assert.AreEqual(updatedUser.Email, user.Email);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [Test]
        public void Delete_ReturnsUserDeleteView()
        {
            var result = _controller.Delete(1) as ViewResult;
            var model = result.Model as User;

            Assert.AreEqual(_userList.First(), model);
        }

        [Test]
        public void DeleteConfirmed_RemovesUserAndRedirects()
        {
            var result = _controller.DeleteConfirmed(1) as RedirectToRouteResult;

            Assert.IsFalse(UserController.userlist.Any(u => u.Id == 1));
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
    }
