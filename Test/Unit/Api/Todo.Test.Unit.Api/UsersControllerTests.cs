using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Todo.Test.Unit.Api
{
    using Moq;
    using System.Reflection;
    using Todo.Domain.Models;
    using Todo.Domain.Interfaces;
    using Todo.Api.Controllers;
    using Todo.Api.Mappers;
    using Todo.Api.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Http;

    [TestClass]
    public class UsersControllerTests
    {
        //setup mock services
        private IUserServices GetUserServices(UserModel testUser)
        {
            var userServices = new Moq.Mock<IUserServices>();
            userServices.Setup( us => us.Create(It.IsAny<UserModel>())).Returns(testUser);
            userServices.Setup(us => us.Read(It.Is<int>(id => id == testUser.Id))).Returns(testUser);
            userServices.Setup(us => us.Update(It.Is<UserModel>( um => um.Id == testUser.Id))).Returns(testUser);
            userServices.Setup(us => us.Delete(It.Is<UserModel>(um => um.Id == testUser.Id))).Returns(true);
            userServices.Setup(us => us.Delete(It.Is<UserModel>(um => um.Id != testUser.Id))).Returns(false);
            userServices.Setup(us => us.Delete(It.Is<int>(id => id == testUser.Id))).Returns(true);
            userServices.Setup(us => us.Delete(It.Is<int>(id => id != testUser.Id))).Returns(false);
            userServices.Setup(us => us.Save(It.Is<UserModel>(um => um.Id == testUser.Id))).Returns(testUser);
            return userServices.Object;
        }

        private IListServices GetListServices()
        {
            var listServices = new Moq.Mock<IListServices>();
            return listServices.Object;
        }

        //for a given controller, method and param, return the expected route (there is probably a built-in for this)
        private string GetApiPath(Type controllerType, string method, string param)
        {
            //replace '[controller]' with name of controller , e.g. 'values' for ValuesController
            var route = controllerType.GetCustomAttribute<RouteAttribute>()
                .Template.Replace($"[controller]", controllerType.Name.ToLower().Substring(0, controllerType.Name.Length - "controller".Length));
            var routeParam = controllerType.GetMethod(method).GetCustomAttribute<HttpMethodAttribute>().Template;
            return param == null ? $"/{route}" : $"/{route}/{param}";
        }

        //setup mock controller context
        private ControllerContext GetControllerContext(Type controllerType, string method, string param)
        {
            var request = new Mock<HttpRequest>();
            request.Setup(r => r.Path).Returns(new PathString(GetApiPath(controllerType, method, param)));
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(hc => hc.Request).Returns(request.Object);
            var actionContext = new Mock<ActionContext>().Object;
            actionContext.HttpContext = httpContext.Object;
            actionContext.RouteData = new Microsoft.AspNetCore.Routing.RouteData();
            actionContext.ActionDescriptor = new Mock<ControllerActionDescriptor>().Object;
            var controllerContext = new ControllerContext(actionContext);
            return controllerContext;
        }

        [TestMethod]
        public void UsersControllerTest_TestGet()
        {
            var expectedModel = new UserModel { Id = 1, Username = "Bob" };
            var controller = new UsersController(GetUserServices(expectedModel), GetListServices());
            controller.ControllerContext = GetControllerContext(controller.GetType(), nameof(controller.Get), expectedModel.Id.ToString());
            IActionResult result = controller.Get(expectedModel.Id);
            var response = result as OkObjectResult;
            Assert.IsNotNull(response);
            var actualModel = response.Value as UserApiModel;
            Assert.IsNotNull(actualModel);
            Assert.AreEqual(expectedModel.ToApiModel().ToJson(), actualModel.ToJson());
        }

        [TestMethod]
        public void UsersControllerTest_TestPut()
        {            
            var expectedModel = new UserModel { Id = 1, Username = "Bob" };
            var controller = new UsersController(GetUserServices(expectedModel), GetListServices());
            controller.ControllerContext = GetControllerContext(controller.GetType(), nameof(controller.Put), expectedModel.Id.ToString());
            IActionResult result = controller.Put(expectedModel.Id, expectedModel.ToApiModel());
            var response = result as CreatedResult;
            Assert.IsNotNull(response);
            var actualModel = response.Value as UserApiModel;
            Assert.IsNotNull(actualModel);
            var expectedLocation = GetApiPath(controller.GetType(), nameof(controller.Put), expectedModel.Id.ToString());
            Assert.AreEqual(expectedLocation, response.Location);
            Assert.AreEqual(expectedModel.ToApiModel().ToJson(), actualModel.ToJson());
        }

        [TestMethod]
        public void UsersControllerTest_TestPost()
        {
            var expectedModel = new UserModel { Id = 1, Username = "Bob" };
            var controller = new UsersController(GetUserServices(expectedModel), GetListServices());
            controller.ControllerContext = GetControllerContext(typeof(UsersController), nameof(controller.Post), null);
            IActionResult result = controller.Post(expectedModel.ToApiModel());
            var response = result as CreatedResult;
            Assert.IsNotNull(response);
            var actualModel = response.Value as UserApiModel;
            Assert.IsNotNull(actualModel);
            var expectedLocation = GetApiPath(controller.GetType(), nameof(controller.Post), null) + $"/{expectedModel.Id}";
            Assert.AreEqual(expectedLocation, response.Location);
            Assert.AreEqual(expectedModel.ToApiModel().ToJson(), actualModel.ToJson());
        }

        [TestMethod]
        public void UsersControllerTest_TestDelete()
        {
            var expectedModel = new UserModel { Id = 1, Username = "Bob" };
            var controller = new UsersController(GetUserServices(expectedModel), GetListServices());
            controller.ControllerContext = GetControllerContext(controller.GetType(), nameof(controller.Delete), expectedModel.Id.ToString());
            IActionResult result = controller.Delete(expectedModel.Id);
            var response = result as OkObjectResult;
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Value);
            Assert.IsTrue(response.Value is bool);
            Assert.IsTrue((bool)response.Value);
        }

    }
}
