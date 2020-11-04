using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FinalProject.Areas.Admin.Models;
using FinalProject.Data;
using FinalProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProject.Controllers
{
    [Area("Admin")]
    public class UploadController : Controller
    {
        private readonly DocSearchContext _context;

        public UploadController(DocSearchContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ViewResult Index()
        {
            var model = new UploadViewModel()
            {
                ReportTypes = _context.ReportTypes.ToList()
            };
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
                    // Using this to dynamically get the file name without asking the user
                    report.Name = Path.GetFileNameWithoutExtension(file.FileName);

                    // Need to write the file as a memorystream and convert it to bytes before base64 encoding for storage
                    byte[] bytes;
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        bytes = ms.ToArray();
                    }

                    // Convert to base64 for easy storage
                    report.Content = Convert.ToBase64String(bytes);

                    _context.Reports.Add(report);
                    _context.SaveChanges();

                    TempData["message"] = "The report was uploaded successfully";
                }
            }
            return RedirectToAction("Index", "Upload");
        }
    }
}
