using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FinalProject.Models;
using FinalProject.Data;
using Microsoft.AspNetCore.Http;
using System.IO;
using FinalProject.OpenXML;
using FinalProject.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using FinalProject.Models.ViewModels;
using FinalProject.Models.DTOs;
using FinalProject.Models.Grid;
using FinalProject.Models.ExtensionMethods;

namespace FinalProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // This is the file type expected for Word documents
        private const string FileType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

        private IRepository<Report> _data { get; set; }
        private IRepository<ReportType> _reportType { get; set; }
        private IReportOps _reportOps { get; set; }

        public HomeController(IRepository<Report> rep, IRepository<ReportType> rt, IReportOps reportOps)
        {
            _data = rep;
            _reportType = rt;
            _reportOps = reportOps;
        }

        public RedirectToActionResult Index(ReportsGridDTO values)
        {
            // This code block makes sure the current route is always set
            var builder = new ReportsGridBuilder(HttpContext.Session, values, defaultSortField: nameof(Report.Date));
            builder.SetSearchRoute(values.SearchString);

            return RedirectToAction("List", builder.CurrentRoute);
        }

        public ViewResult List(ReportsGridDTO values)
        {
            var builder = new ReportsGridBuilder(HttpContext.Session, values, defaultSortField: nameof(Report.Date));
            builder.SetSearchRoute(values.SearchString);

            builder.SaveRouteSegments();

            var options = new ReportQueryOptions
            {
                Includes = "ReportType",
                OrderByDirection = builder.CurrentRoute.SortDirection,
                PageNumber = builder.CurrentRoute.PageNumber,
                PageSize = builder.CurrentRoute.PageSize
            };

            options.SortFilter(builder);

            var model = new ReportListViewModel
            {
                Reports = _data.List(options),
                ReportTypes = _reportType.List(new QueryOptions<ReportType>
                {
                    OrderBy = rt => rt.Name
                }),
                CurrentRoute = builder.CurrentRoute,
                TotalPages = builder.GetTotalPages(_data.Count)
            };

            return View(model);
        }

        [HttpPost]
        public RedirectToActionResult Filter(string[] filter, ReportsGridDTO values, bool clear = false)
        {
            // Added the ReportsGridDTO object above to pull the current search string
            var builder = new ReportsGridBuilder(HttpContext.Session);

            if (clear)
            {
                builder.ClearFilterSegments();
                builder.SetSearchRoute(null);
            }
            else
            {
                var reportTypes = _reportType.Get(filter[0].ToInt());
                builder.LoadFilterSegments(filter, reportTypes);
                builder.SetSearchRoute(values.SearchString);
            }
            builder.SaveRouteSegments();

            return RedirectToAction("List", builder.CurrentRoute);
        }

        public IActionResult Display(int id, DisplayViewModel model)
        {
            Report report = _data.Get(id);

            byte[] bytes = report.Content;

            model.html = _reportOps.ConvertToHTML(bytes);

            return View(model);
        }

        public IActionResult Download(int id)
        {
            Report report = _data.Get(id);

            byte[] file = report.Content;

            return File(file, FileType, report.Name + ".docx");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
