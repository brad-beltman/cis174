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
using Microsoft.AspNetCore.Http;
using System.IO;
using FinalProject.OpenXML;
using FinalProject.Data.Repositories;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        // This is the file type expected for Word documents
        private const string FileType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

        private IRepository<Report> _data { get; set; }
        private IReportOps _reportOps { get; set; }

        public HomeController(IRepository<Report> rep, IReportOps reportOps)
        {
            _data = rep;
            _reportOps = reportOps;
        }

        public ViewResult Index(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                var reports = _data.List(new QueryOptions<Report>
                {
                    Includes = "ReportType",
                    WhereClauses = new WhereClauses<Report>
                    {
                        r => r.SearchIndex.Contains(searchString.ToLower())
                    },
                    OrderBy = r => r.Date
                });
                return View(reports);
            }
            else
            {
                var reports = _data.List(new QueryOptions<Report>
                {
                    Includes = "ReportType",
                    OrderBy = r => r.Date
                });
                return View(reports);
            }
        }

        public IActionResult Display(int id, DisplayViewModel model)
        {
            Report report = _data.Get(id);

            byte[] bytes = Convert.FromBase64String(report.Content);

            model.html = _reportOps.ConvertToHTML(bytes);

            return View(model);
        }

        public IActionResult Download(int id)
        {
            Report report = _data.Get(id);

            byte[] file = Convert.FromBase64String(report.Content);

            return File(file, FileType, report.Name + ".docx");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
