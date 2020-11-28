using FinalProject.Areas.Admin.Models;
using FinalProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Tests
{
    class MockObjects
    {
        // Mock TempData Object
        public static ITempDataDictionary GetMockTempData()
        {
            ITempDataProvider tempDataProvider = Mock.Of<ITempDataProvider>();
            TempDataDictionaryFactory tempDataDictionaryFactory = new TempDataDictionaryFactory(tempDataProvider);
            ITempDataDictionary tempData = tempDataDictionaryFactory.GetTempData(new DefaultHttpContext());
            return tempData;
        }

        // Mock ReportOps Object
        public static Mock<IReportOps> GetReportOps()
        {
            var reportOps = new Mock<IReportOps>();
            Dictionary<string, string> contents = new Dictionary<string, string>
            {
                { "headings", "mock" },
                { "content", "mock" }
            };
            Byte[] bytes = new byte[0];
            reportOps.Setup(f => f.CreateSearchIndex(bytes)).Returns(contents);
            return reportOps;
        }

        // Mock Identity Objects
        public static Mock<UserManager<TIDentityUser>> GetUserManagerMock<TIDentityUser>() where TIDentityUser : IdentityUser
        {
            return new Mock<UserManager<TIDentityUser>>(
                    new Mock<IUserStore<TIDentityUser>>().Object,
                    new Mock<IOptions<IdentityOptions>>().Object,
                    new Mock<IPasswordHasher<TIDentityUser>>().Object,
                    new IUserValidator<TIDentityUser>[0],
                    new IPasswordValidator<TIDentityUser>[0],
                    new Mock<ILookupNormalizer>().Object,
                    new Mock<IdentityErrorDescriber>().Object,
                    new Mock<IServiceProvider>().Object,
                    new Mock<ILogger<UserManager<TIDentityUser>>>().Object);
        }

        public static Mock<RoleManager<TIdentityRole>> GetRoleManagerMock<TIdentityRole>() where TIdentityRole : IdentityRole
        {
            return new Mock<RoleManager<TIdentityRole>>(
                    new Mock<IRoleStore<TIdentityRole>>().Object,
                    new IRoleValidator<TIdentityRole>[0],
                    new Mock<ILookupNormalizer>().Object,
                    new Mock<IdentityErrorDescriber>().Object,
                    new Mock<ILogger<RoleManager<TIdentityRole>>>().Object);
        }

        public static IdentityError GetIdentityErrors()
        {
            IdentityError identityErrors = new IdentityError { Description = "mock"};

            return identityErrors;
        }

        // Mock Document Objects
        public static Mock<IFormFile> GetWordDoc(string length = "regular")
        {
            var file = new Mock<IFormFile>();
            var content = "mock file";
            var filename = "mock.docx";
            string contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            string contentDisposition = "form-data; name=\"file\"; filename=\"mock.docx\"";
            HeaderDictionary headers = new HeaderDictionary();
            headers.Add("Content-Disposition", contentDisposition);
            headers.Add("Content-Type", contentType);
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            file.Setup(f => f.OpenReadStream()).Returns(ms);
            file.Setup(f => f.FileName).Returns(filename);
            file.Setup(f => f.Headers).Returns(headers);
            file.Setup(f => f.ContentDisposition).Returns(contentDisposition);
            file.Setup(f => f.ContentType).Returns(contentType);
            if (length == "regular")
            {
                file.Setup(f => f.Length).Returns(ms.Length);
            }
            else if (length == "zero")
            {
                // File contains nothing
                file.Setup(f => f.Length).Returns(0);
            }
            else if (length == "overflow")
            {
                // File size is too large
                file.Setup(f => f.Length).Returns(20000001);
            }
            return file;
        }

        public static Mock<IFormFile> GetPDFDoc()
        {
            var file = new Mock<IFormFile>();
            var content = "mock file";
            var filename = "mock.pdf";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            file.Setup(_ => _.OpenReadStream()).Returns(ms);
            file.Setup(_ => _.FileName).Returns(filename);
            file.Setup(_ => _.Length).Returns(ms.Length);
            return file;
        }

        // Mock ViewModels
        public static AddUserViewModel GetAddUserViewModel()
        {
            AddUserViewModel model = new AddUserViewModel
            {
                Username = "mock",
                Password = "MockPassword1!",
                ConfirmPassword = "MockPassword1!"
            };

            return model;
        }
    }
}
