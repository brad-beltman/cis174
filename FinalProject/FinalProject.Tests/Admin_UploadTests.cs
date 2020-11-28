using System;
using Xunit;
using FinalProject.Controllers;
using FinalProject.Areas.Admin.Controllers;
using Moq;
using FinalProject.Data.Repositories;
using FinalProject.Models;
using FinalProject.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using OpenXmlPowerTools;
using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace FinalProject.Tests
{
    public class Admin_UploadTests
    {
        [Fact]
        public void Index_GET_ReturnViewResult()
        {
            // arrange
            var rep = new Mock<IRepository<Report>>();
            var rep_rt = new Mock<IRepository<ReportType>>();
            var reportOps = new Mock<IReportOps>();
            var controller = new UploadController(rep.Object, rep_rt.Object, reportOps.Object);

            // act
            var result = controller.Index(new UploadViewModel());

            // assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Index_POST_Valid_File_Format()
        {
            // arrange
            string expectedMessage = "The report was uploaded successfully";
            var report = new Report();
            var file = MockObjects.GetWordDoc();
            var rep = new Mock<IRepository<Report>>();
            var rep_rt = new Mock<IRepository<ReportType>>();
            var reportOps = MockObjects.GetReportOps();
            ITempDataDictionary tempData = MockObjects.GetMockTempData();
            var controller = new UploadController(rep.Object, rep_rt.Object, reportOps.Object)
            {
                TempData = tempData
            };

            // act
            var run = controller.Index(report, file.Object) as ViewResult;
            string actualMessage = controller.TempData["message"].ToString();

            // assert
            Assert.Equal(expectedMessage, actualMessage);
        }

        [Fact]
        public void Index_POST_Invalid_File_Extension()
        {
            // arrange
            string expectedMessage = "The file must be of type docx";
            var report = new Report();
            var file = MockObjects.GetPDFDoc();
            var rep = new Mock<IRepository<Report>>();
            var rep_rt = new Mock<IRepository<ReportType>>();
            var reportOps = MockObjects.GetReportOps();
            ITempDataDictionary tempData = MockObjects.GetMockTempData();
            var controller = new UploadController(rep.Object, rep_rt.Object, reportOps.Object)
            {
                TempData = tempData
            };

            // act
            var run = controller.Index(report, file.Object);
            string actualMessage = controller.TempData["fail_message"].ToString();

            // assert
            Assert.Equal(expectedMessage, actualMessage);
        }

        [Fact]
        public void Index_POST_File_Too_Large()
        {
            // arrange
            string expectedMessage = "The file is larger than 20MB";
            var report = new Report();
            var file = MockObjects.GetWordDoc("overflow");
            var rep = new Mock<IRepository<Report>>();
            var rep_rt = new Mock<IRepository<ReportType>>();
            var reportOps = MockObjects.GetReportOps();
            ITempDataDictionary tempData = MockObjects.GetMockTempData();
            var controller = new UploadController(rep.Object, rep_rt.Object, reportOps.Object)
            {
                TempData = tempData
            };

            // act
            var run = controller.Index(report, file.Object);
            string actualMessage = controller.TempData["fail_message"].ToString();

            // assert
            Assert.Equal(expectedMessage, actualMessage);
        }

        [Fact]
        public void Index_POST_File_Zero_Length()
        {
            // arrange
            string expectedMessage = "The file was empty";
            var report = new Report();
            var file = MockObjects.GetWordDoc("zero");
            var rep = new Mock<IRepository<Report>>();
            var rep_rt = new Mock<IRepository<ReportType>>();
            var reportOps = MockObjects.GetReportOps();
            ITempDataDictionary tempData = MockObjects.GetMockTempData();
            var controller = new UploadController(rep.Object, rep_rt.Object, reportOps.Object)
            {
                TempData = tempData
            };

            // act
            var run = controller.Index(report, file.Object);
            string actualMessage = controller.TempData["fail_message"].ToString();

            // assert
            Assert.Equal(expectedMessage, actualMessage);
        }
    }
}
