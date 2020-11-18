using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FinalProject.Models;
using FinalProject.Data;
using Microsoft.EntityFrameworkCore;
using System.IO;
using FinalProject.OpenXML;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly DocSearchContext _context;
        private IReportOps _reportOps { get; set; }

        public HomeController(DocSearchContext context, IReportOps reportOps)
        {
            _context = context;
            _reportOps = reportOps;
        }

        public ViewResult Index(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                return View(_context.Reports.Include(r => r.ReportType)
                    .Where(r => r.SearchIndex.Contains(searchString.ToLower())).ToList());
            }
            else
                return View(_context.Reports.Include(r => r.ReportType).ToList());
        }

        // Inject IReportDisplay directly into the action, since this is the only action that will need it.
        public IActionResult Display(int? id, DisplayViewModel model)
        {
            if (id == null)
            {
                return NotFound();
            }

            Report report = _context.Reports.Find(id);

            byte[] bytes = Convert.FromBase64String(report.Content);

            model.html = _reportOps.ConvertToHTML(bytes);

            return View(model);
        }

        public IActionResult Download(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Report report = _context.Reports.Find(id);

            byte[] file = Convert.FromBase64String(report.Content);

            return File(file, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", report.Name + ".docx");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
