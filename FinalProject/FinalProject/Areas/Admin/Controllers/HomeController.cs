using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Areas.Admin.Models.DTOs;
using FinalProject.Data.Repositories;
using FinalProject.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using FinalProject.Areas.Admin.Models.ViewModels;

namespace FinalProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class HomeController : Controller
    {
        // private readonly DocSearchContext _context;
        private IRepository<Report> _reports { get; set; }
        private IRepository<ReportType> _reportTypes { get; set; }

        public HomeController(IRepository<Report> rep, IRepository<ReportType> rt_rep)
        {
            _reports = rep;
            _reportTypes = rt_rep;
        }

        // GET: Admin/Home
        public IActionResult Index(IndexViewModel model)
        {
            var reports = _reports.List(new QueryOptions<Report>
            {
                Includes = "ReportType",
                OrderBy = r => r.Date
            })
                .Select(r => new ReportsDTO
                {
                    ID = r.ID,
                    Name = r.Name,
                    Author = r.Author,
                    Date = r.Date,
                    ReportType = r.ReportType.Name
                });

            model.Reports = reports;

            return View(model);
        }

        // GET: Admin/Home/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            EditViewModel model = new EditViewModel();
            var report = _reports.List(new QueryOptions<Report>
            {
                Includes = "ReportType",
                WhereClauses = new WhereClauses<Report>
                {
                    reports => reports.ID == id
                },
                OrderBy = r => r.Date
            })
                .Select(r => new ReportsDTO
                {
                    ID = r.ID,
                    Name = r.Name,
                    Author = r.Author,
                    Date = r.Date,
                    ReportType = r.ReportType.Name,
                    ReportTypeID = r.ReportTypeID
                });

            model.ReportTypes = _reportTypes.List(new QueryOptions<ReportType> { });

            if (report == null)
            {
                TempData["fail_message"] = "The given report was not found";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                model.ID = report.First().ID;
                model.Name = report.First().Name;
                model.Author = report.First().Author;
                model.Date = report.First().Date;
                model.ReportTypeID = report.First().ReportTypeID;
            }
            return View(model);
        }

        // POST: Admin/Home/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,ReportTypeID,Name,Author,Date")] EditViewModel model)
        {
            // Using the ReportsDTO so the full file contents aren't passed around each request
            if (id != model.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Report report = _reports.Get(id);
                    report.ID = model.ID;
                    report.Name = model.Name;
                    report.Author = model.Author;
                    report.Date = model.Date;
                    report.ReportTypeID = model.ReportTypeID;
                    // No need for the admin to be able to update the file contents or SearchIndex directly, so they are not included here

                    _reports.Save();
                    TempData["message"] = "The report was updated successfully";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportExists(model.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        TempData["fail_message"] = "The report was not updated successfully";
                        return RedirectToAction(nameof(Index));
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Admin/Home/Delete/5
        public IActionResult Delete(int id, DeleteViewModel model)
        {
            var report = _reports.List(new QueryOptions<Report>
            {
                Includes = "ReportType",
                WhereClauses = new WhereClauses<Report>
                {
                    reports => reports.ID == id
                },
                OrderBy = r => r.Date
            })
                .Select(r => new ReportsDTO
                {
                    ID = r.ID,
                    Name = r.Name,
                    Author = r.Author,
                    Date = r.Date,
                    ReportType = r.ReportType.Name
                });

            if (report == null)
            {
                return NotFound();
            }
            else
            {
                model.ID = report.First().ID;
                model.Name = report.First().Name;
                model.Author = report.First().Author;
                model.Date = report.First().Date;
                model.ReportType = report.First().ReportType;
            }
            return View(model);
        }

        // POST: Admin/Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var report = _reports.Get(id);
            _reports.Delete(report);
            try
            {
                _reports.Save();
                TempData["message"] = "The report was deleted successfully";
            }
            catch (Exception)
            {
                TempData["fail_message"] = "The report was not deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ReportExists(int id)
        {
            return _reports.Exists(id);
        }
    }
}
