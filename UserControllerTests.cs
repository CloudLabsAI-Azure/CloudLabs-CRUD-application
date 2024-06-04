using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using CRUD_application_2.Models; // Replace with your actual namespace
using CRUD_application_2.Controllers; // Replace with your actual namespace
using System.Web.Mvc;

[TestFixture]
public class UserControllerTests
{
    private UserController _userController;
    private List<User> _users;

    [SetUp]
    public void Setup()
    {
        _userController = new UserController();
        _users = new List<User>()
        {
            new User { Id = 1, Name = "Test1" },
            new User { Id = 2, Name = "Test2" }
        };
    }

    [Test]
    public void Index_ReturnsAViewResult_WithAListOfUsers()
    {
        var result = _userController.Index();
        Assert.That(result, Is.InstanceOf<ViewResult>());
        var viewResult = result as ViewResult;
        Assert.That(viewResult.Model, Is.EqualTo(_users));
    }

    [Test]
    public void Details_ReturnsAViewResult_WithAUser()
    {
        var user = _users.First();
        var result = _userController.Details(user.Id);
        Assert.That(result, Is.InstanceOf<ViewResult>());
        var viewResult = result as ViewResult;
        Assert.That(viewResult.Model, Is.EqualTo(user));
    }

    // Add similar tests for Create, Edit, Delete methods
    [Test]
    public void Create_ReturnsAViewResult_WithAUser()
    {
        var result = _userController.Create();
        Assert.That(result, Is.InstanceOf<ViewResult>());
        var viewResult = result as ViewResult;
        Assert.That(viewResult.Model, Is.InstanceOf<User>());
    }

    [Test]
    public void Create_Post_ReturnsARedirectToRouteResult_WhenModelStateIsValid()
    {
        var user = new User { Id = 3, Name = "Test3" };
        var result = _userController.Create(user);
        Assert.That(result, Is.InstanceOf<RedirectToRouteResult>());
        var redirectToRouteResult = result as RedirectToRouteResult;
        Assert.That(redirectToRouteResult.RouteValues["action"], Is.EqualTo("Index"));
    }

    [Test]
    public void Create_Post_ReturnsAViewResult_WhenModelStateIsNotValid()
    {
        var user = new User { Id = 3, Name = "Test3" };
        _userController.ModelState.AddModelError("Email", "Email is required");
        var result = _userController.Create(user);
        Assert.That(result, Is.InstanceOf<ViewResult>());
        var viewResult = result as ViewResult;
        Assert.That(viewResult.Model, Is.EqualTo(user));
    }

}
