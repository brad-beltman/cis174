using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FinalProject.Areas.Admin.Controllers;
using Moq;
using Moq.Protected;
using Microsoft.AspNetCore.Identity;
using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace FinalProject.Tests
{
    public class Admin_UserTests
    {
        [Fact]
        public async void Index_GET_ReturnsViewResult()
        {
            // arrange
            var userManager = MockObjects.GetUserManagerMock<User>();
            userManager.Setup(u => u.FindByIdAsync(It.IsAny<String>())).Returns(Task.FromResult(new User()));
            var roleManager = MockObjects.GetRoleManagerMock<IdentityRole>().Object;
            var controller = new UserController(userManager.Object, roleManager);

            // act
            // Add await, else we'll get a therad back instead
            var result = await controller.Index(new UserViewModel());

            // assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Add_GET_ReturnsViewResult()
        {
            // arrange
            var userManager = MockObjects.GetUserManagerMock<User>();
            userManager.Setup(u => u.FindByIdAsync(It.IsAny<String>())).Returns(Task.FromResult(new User()));
            var roleManager = MockObjects.GetRoleManagerMock<IdentityRole>().Object;
            var controller = new UserController(userManager.Object, roleManager);

            // act
            var result = controller.Add();

            // assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void Delete_POST_Successful()
        {
            // arrange
            string expectedMessage = "User deleted successfully";
            var userManager = MockObjects.GetUserManagerMock<User>();
            userManager.Setup(u => u.FindByIdAsync(It.IsAny<String>())).Returns(Task.FromResult(new User()));
            userManager.Setup(u => u.DeleteAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);
            var roleManager = MockObjects.GetRoleManagerMock<IdentityRole>().Object;
            ITempDataDictionary tempData = MockObjects.GetMockTempData();
            var controller = new UserController(userManager.Object, roleManager)
            {
                TempData = tempData
            };

            // act
            var run = await controller.Delete(string.Empty) as ViewResult;
            string actualMessage = controller.TempData["message"].ToString();

            // assert
            Assert.Equal(expectedMessage, actualMessage);
        }

        [Fact]
        public async void Delete_POST_Unsuccessful()
        {
            // arrange
            var userManager = MockObjects.GetUserManagerMock<User>();
            userManager.Setup(u => u.FindByIdAsync(It.IsAny<String>())).Returns(Task.FromResult(new User()));
            userManager.Setup(u => u.DeleteAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Failed());
            var roleManager = MockObjects.GetRoleManagerMock<IdentityRole>().Object;
            ITempDataDictionary tempData = MockObjects.GetMockTempData();
            var controller = new UserController(userManager.Object, roleManager)
            {
                TempData = tempData
            };

            // act
            var run = await controller.Delete(string.Empty) as ViewResult;

            // assert
            // A "fail_message" should be present if the operation fails
            Assert.NotNull(controller.TempData["fail_message"]);
        }
    }
}
