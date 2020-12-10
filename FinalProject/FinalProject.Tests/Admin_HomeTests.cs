using FinalProject.Areas.Admin.Controllers;
using FinalProject.Areas.Admin.Models;
using FinalProject.Areas.Admin.Models.ViewModels;
using FinalProject.Data.Repositories;
using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FinalProject.Tests
{
    public class Admin_HomeTests
    {
        [Fact]
        public void Index_GET_ReturnViewModel()
        {
            // arrange
            var rep = new Mock<IRepository<Report>>();
            var rep_rt = new Mock<IRepository<ReportType>>();
            var controller = new HomeController(rep.Object, rep_rt.Object);

            // act
            //var result = controller.Index(new IndexViewModel { });

            // assert
            //Assert.IsType<ViewResult>(result);
        }

        //[Fact]
        //public void Edit_GET_ValidID_ReturnViewModel()
        //{
        //    // arrange
        //    var rep = new Mock<IRepository<Report>>();
        //    var rep_rt = new Mock<IRepository<ReportType>>();
        //    var controller = new HomeController(rep.Object, rep_rt.Object);

        //    // act
        //    var result = controller.Edit(0);

        //    // assert
        //    Assert.IsType<ViewResult>(result);
        //}
    }
}
