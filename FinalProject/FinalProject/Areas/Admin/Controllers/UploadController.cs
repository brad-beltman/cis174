using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FinalProject.Areas.Admin.Models;
using FinalProject.Data;
using FinalProject.Models;
using FinalProject.OpenXML;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProject.Controllers
{
    [Area("Admin")]
    public class UploadController : Controller
    {
        private readonly DocSearchContext _context;
        private IReportOps _reportOps { get; set; }

        public UploadController(DocSearchContext context, IReportOps reportOps)
        {
            _context = context;
            _reportOps = reportOps;
        }

        [HttpGet]
        public ViewResult Index(UploadViewModel model)
        {
            model.ReportTypes = _context.ReportTypes.ToList();

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
                        // Using this to dynamically get the file name without asking the user
                        report.Name = Path.GetFileNameWithoutExtension(file.FileName);

                        // Need to write the file as a memorystream and convert it to bytes before base64 encoding for storage
                        byte[] bytes;
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            bytes = ms.ToArray();

                            report.SearchIndex = _reportOps.CreateSearchIndex(bytes);
                        }

                        // Convert to base64 for easy storage
                        report.Content = Convert.ToBase64String(bytes);

                        _context.Reports.Add(report);
                        _context.SaveChanges();

                        TempData["message"] = "The report was uploaded successfully";
                    }
                    else
                    {
                        TempData["message"] = "The file is larger than 20MB";
                    }
                }
                else
                {
                    TempData["message"] = "The file was empty";
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
