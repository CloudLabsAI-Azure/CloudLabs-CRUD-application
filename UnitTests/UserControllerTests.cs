using CRUD_application_2.Controllers;
using CRUD_application_2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Xunit;

namespace CRUD_application_2.Tests
{
    public class UserControllerTests
    {
        //[Fact]
        //public void Index_ReturnsViewResult_WithUserList()
        //{
        //    // Arrange
        //    var controller = new UserController();
        //    var expectedUserList = new List<User>();

        //    // Act
        //    var result = controller.Index() as ViewResult;

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.Equal(expectedUserList, result.Model);
        //}

        [Fact]
        public void Details_WithValidId_ReturnsViewResult_WithUser()
        {
            // Arrange
            var controller = new UserController();
            var user = new User { Id = 1 };
            UserController.userlist.Add(user);

            // Act
            var result = controller.Details(1) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user, result.Model);
        }

        [Fact]
        public void Details_WithInvalidId_ReturnsHttpNotFoundResult()
        {
            // Arrange
            var controller = new UserController();

            // Act
            var result = controller.Details(1) as HttpNotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Create_WithValidUser_RedirectsToIndex()
        {
            // Arrange
            var controller = new UserController();
            var user = new User();

            // Act
            var result = controller.Create(user) as System.Web.Mvc.RedirectToRouteResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.RouteName);
        }

        [Fact]
        public void Create_WithInvalidUser_ReturnsViewResult_WithUser()
        {
            // Arrange
            var controller = new UserController();
            var user = new User();
            controller.ModelState.AddModelError("Name", "Name is required.");

            // Act
            var result = controller.Create(user) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user, result.Model);
        }

        [Fact]
        public void Edit_WithValidId_ReturnsViewResult_WithUser()
        {
            // Arrange
            var controller = new UserController();
            var user = new User { Id = 1 };
            UserController.userlist.Add(user);

            // Act
            var result = controller.Edit(1) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user, result.Model);
        }

        [Fact]
        public void Edit_WithInvalidId_ReturnsHttpNotFoundResult()
        {
            // Arrange
            var controller = new UserController();

            // Act
            var result = controller.Edit(1) as HttpNotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Edit_WithValidUser_RedirectsToIndex()
        {
            // Arrange
            var controller = new UserController();
            var user = new User { Id = 1 };

            // Act
            var result = controller.Edit(1, user) as System.Web.Mvc.RedirectToRouteResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.RouteName);
        }

        [Fact]
        public void Edit_WithInvalidUser_ReturnsViewResult_WithUser()
        {
            // Arrange
            var controller = new UserController();
            var user = new User { Id = 1 };
            controller.ModelState.AddModelError("Name", "Name is required.");

            // Act
            var result = controller.Edit(1, user) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user, result.Model);
        }

        [Fact]
        public void Delete_WithValidId_ReturnsViewResult_WithUser()
        {
            // Arrange
            var controller = new UserController();
            var user = new User { Id = 1 };
            UserController.userlist.Add(user);

            // Act
            var result = controller.Delete(1) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user, result.Model);
        }

        [Fact]
        public void Delete_WithInvalidId_ReturnsHttpNotFoundResult()
        {
            // Arrange
            var controller = new UserController();

            // Act
            var result = controller.Delete(1) as HttpNotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Delete_WithValidIdAndFormCollection_RedirectsToIndex()
        {
            // Arrange
            var controller = new UserController();
            var user = new User { Id = 1 };
            UserController.userlist.Add(user);
            var collection = new FormCollection
            {
                ["username"] = "John",
                ["email"] = "john@example.com"
            };

            // Act
            var result = controller.Delete(1, collection) as System.Web.Mvc.RedirectToRouteResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.RouteName);
        }
    }
}