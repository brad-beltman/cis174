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

namespace FinalProject.Areas.Admin.Controllers
{
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
        public IActionResult Index()
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
            return View(reports);
            //var reports = await _context.Reports
            //    .Include(rt => rt.ReportType)
            //    .Select(r => new ReportsDTO
            //    {
            //        ID = r.ID,
            //        Name = r.Name,
            //        Author = r.Author,
            //        Date = r.Date,
            //        ReportType = r.ReportType.Name
            //    })
            //    .ToListAsync();
            //return View(reports);
        }

        // GET: Admin/Home/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        // POST: Admin/Home/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ID,Type,Name,Author,Date,Content")] Report report)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(report);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(report);
        //}

        // GET: Admin/Home/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

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
            //var report = await _context.Reports
            //    .Include(rt => rt.ReportType)
            //    .Select(r => new ReportsDTO
            //    {
            //        ID = r.ID,
            //        Name = r.Name,
            //        Author = r.Author,
            //        Date = r.Date,
            //        ReportType = r.ReportType.Name
            //    })
            //    .Where(r => r.ID == id)
            //    .SingleOrDefaultAsync();

            ViewBag.ReportTypes = _reportTypes.List(new QueryOptions<ReportType> { });
            //ViewBag.ReportTypes = await _context.ReportTypes.ToListAsync();


            if (report == null)
            {
                return NotFound();
            }
            else
            {
                return View(report.First());
            }
        }

        // POST: Admin/Home/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,ReportTypeID,Name,Author,Date")] ReportsDTO reportdto)
        {
            // Using the ReportsDTO so the full file contents aren't passed around each request
            if (id != reportdto.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Report report = _reports.Get(id);
                    report.ID = reportdto.ID;
                    report.Name = reportdto.Name;
                    report.Author = reportdto.Author;
                    report.Date = reportdto.Date;
                    report.ReportTypeID = reportdto.ReportTypeID;
                    // No need for the admin to be able to update the file contents or SearchIndex directly, so they are not included here

                    _reports.Save();
                    TempData["message"] = "The report was updated successfully";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportExists(reportdto.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(reportdto);
        }

        // GET: Admin/Home/Delete/5
        public IActionResult Delete(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

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

            //var report = _context.Reports
            //    .Include(rt => rt.ReportType)
            //    .Select(r => new ReportsDTO
            //    {
            //        ID = r.ID,
            //        Name = r.Name,
            //        Author = r.Author,
            //        Date = r.Date,
            //        ReportType = r.ReportType.Name
            //    })
            //    .Where(r => r.ID == id)
            //    .SingleOrDefaultAsync();

            if (report == null)
            {
                return NotFound();
            }
            else
            {
                return View(report.First());
            }
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
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ReportExists(int id)
        {
            return _reports.Exists(id);
        }
    }
}
