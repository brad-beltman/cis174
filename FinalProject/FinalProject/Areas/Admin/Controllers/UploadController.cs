using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FinalProject.Areas.Admin.Models;
using FinalProject.Areas.Admin.Models.ViewModels;
using FinalProject.Data;
using FinalProject.Data.Repositories;
using FinalProject.Models;
using FinalProject.OpenXML;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class UploadController : Controller
    {
        private IReportOps _reportOps { get; set; }
        private IRepository<Report> _reports { get; set; }
        private IRepository<ReportType> _reportTypes { get; set; }

        public UploadController(IRepository<Report> rep, IRepository<ReportType> rt_rep, IReportOps reportOps)
        {
            _reports = rep;
            _reportTypes = rt_rep;
            _reportOps = reportOps;
        }

        [HttpGet]
        public ViewResult Index(UploadViewModel model)
        {
            model.ReportTypes = _reportTypes.List(new QueryOptions<ReportType> { });

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Report report, IFormFile file)
        {
            if (file != null)
            {
                if (file.Length > 0)
                {
                    // The file should be 20MB or less
                    if (file.Length < 20000000)
                    {
                        if (Path.GetExtension(file.FileName) == ".docx")
                        {
                            // Using this to dynamically get the file name without asking the user
                            report.Name = Path.GetFileNameWithoutExtension(file.FileName);

                            // Need to write the file as a memorystream and convert it to bytes before base64 encoding for storage
                            byte[] bytes;
                            using (var ms = new MemoryStream())
                            {
                                file.CopyTo(ms);
                                bytes = ms.ToArray();

                                Dictionary<string, string> content = _reportOps.CreateSearchIndex(bytes);

                                report.Headings = content["headings"];
                                report.SearchIndex = content["content"];
                            }

                            // Convert to base64 for easy storage
                            report.Content = Convert.ToBase64String(bytes);

                            _reports.Insert(report);
                            _reports.Save();

                            TempData["message"] = "The report was uploaded successfully";
                        }
                        else
                        {
                            TempData["fail_message"] = "The file must be of type docx";
                        }
                    }
                    else
                    {
                        TempData["fail_message"] = "The file is larger than 20MB";
                    }
                }
                else
                {
                    TempData["fail_message"] = "The file was empty";
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
