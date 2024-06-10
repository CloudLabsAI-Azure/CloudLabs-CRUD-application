using Xunit;
using System.Web.Mvc;
using CRUD_application_2.Controllers;
using CRUD_application_2.Models;
using System.Collections.Generic;
using System.Linq;

namespace CRUD_application_2.Tests.Controllers
{
    public class UserControllerTest
    {
        private UserController _controller;
        private List<User> _users;

        public UserControllerTest()
        {
            // Initialize the controller and a list of users
            _controller = new UserController();
            _users = new List<User>
            {
                new User { Id = 1, Name = "User1", Email = "user1@example.com" },
                new User { Id = 2, Name = "User2", Email = "user2@example.com" },
                new User { Id = 3, Name = "User3", Email = "user3@example.com" }
            };

            // Populate the controller's userlist
            UserController.userlist = _users;
        }

        [Fact]
        public void Index_ReturnsCorrectView_WithAllUsers()
        {
            // Act
            var result = _controller.Index(null) as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = result.Model as List<User>;
            Assert.Equal(3, model.Count);
        }

        [Fact]
        public void Details_ReturnsCorrectView_WithUser()
        {
            // Act
            var result = _controller.Details(1) as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = result.Model as User;
            Assert.Equal("User1", model.Name);
        }

        [Fact]
        public void Details_ReturnsHttpNotFound_ForInvalidId()
        {
            // Act
            var result = _controller.Details(999);

            // Assert
            Assert.IsType<HttpNotFoundResult>(result);
        }

        [Fact]
        public void Create_Post_AddsUserAndRedirects()
        {
            // Arrange
            var newUser = new User { Id = 4, Name = "User4", Email = "user4@example.com" };

            // Act
            var result = _controller.Create(newUser) as RedirectToRouteResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.RouteValues["action"]);
            Assert.Equal(4, UserController.userlist.Count);
            Assert.Equal("User4", UserController.userlist.Last().Name);
        }

        [Fact]
        public void Edit_Get_ReturnsCorrectView_WithUser()
        {
            // Act
            var result = _controller.Edit(1) as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = result.Model as User;
            Assert.Equal("User1", model.Name);
        }

        [Fact]
        public void Edit_Get_ReturnsHttpNotFound_ForInvalidId()
        {
            // Act
            var result = _controller.Edit(999);

            // Assert
            Assert.IsType<HttpNotFoundResult>(result);
        }

        [Fact]
        public void Edit_Post_UpdatesUserAndRedirects()
        {
            // Arrange
            var updatedUser = new User { Id = 1, Name = "UpdatedUser", Email = "updated@example.com" };

            // Act
            var result = _controller.Edit(1, updatedUser) as RedirectToRouteResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.RouteValues["action"]);
            Assert.Equal("UpdatedUser", UserController.userlist.First().Name);
            Assert.Equal("updated@example.com", UserController.userlist.First().Email);
        }

        [Fact]
        public void Edit_Post_ReturnsHttpNotFound_ForInvalidId()
        {
            // Arrange
            var updatedUser = new User { Id = 999, Name = "UpdatedUser", Email = "updated@example.com" };

            // Act
            var result = _controller.Edit(999, updatedUser);

            // Assert
            Assert.IsType<HttpNotFoundResult>(result);
        }

        [Fact]
        public void Delete_Get_ReturnsCorrectView_WithUser()
        {
            // Act
            var result = _controller.Delete(1) as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = result.Model as User;
            Assert.Equal("User1", model.Name);
        }

        [Fact]
        public void Delete_Get_ReturnsHttpNotFound_ForInvalidId()
        {
            // Act
            var result = _controller.Delete(999);

            // Assert
            Assert.IsType<HttpNotFoundResult>(result);
        }

        [Fact]
        public void Delete_Post_DeletesUserAndRedirects()
        {
            // Act
            var result = _controller.Delete(1, new FormCollection()) as RedirectToRouteResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.RouteValues["action"]);
            Assert.Equal(2, UserController.userlist.Count);
            Assert.DoesNotContain(UserController.userlist, u => u.Id == 1);
        }

        [Fact]
        public void Delete_Post_ReturnsHttpNotFound_ForInvalidId()
        {
            // Act
            var result = _controller.Delete(999, new FormCollection());

            // Assert
            Assert.IsType<HttpNotFoundResult>(result);
        }
    }
}
