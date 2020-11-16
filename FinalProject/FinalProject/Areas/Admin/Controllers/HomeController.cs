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

namespace FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly DocSearchContext _context;

        public HomeController(DocSearchContext context)
        {
            _context = context;
        }

        // GET: Admin/Home
        public async Task<IActionResult> Index()
        {
            var reports = await _context.Reports
                .Include(rt => rt.ReportType)
                .Select(r => new ReportsDTO
                {
                    ID = r.ID,
                    Name = r.Name,
                    Author = r.Author,
                    Date = r.Date,
                    ReportType = r.ReportType.Name
                })
                .ToListAsync();
            return View(reports);
        }

        //// GET: Admin/Home/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var report = await _context.Reports.Include(r => r.ReportType).FirstOrDefaultAsync(r => r.ID == id);
        //    if (report == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(report);
        //}

        // GET: Admin/Home/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Home/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Type,Name,Author,Date,Content")] Report report)
        {
            if (ModelState.IsValid)
            {
                _context.Add(report);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(report);
        }

        // GET: Admin/Home/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var report = await _context.Reports.FindAsync(id);
            var report = await _context.Reports
                .Include(rt => rt.ReportType)
                .Select(r => new ReportsDTO
                {
                    ID = r.ID,
                    Name = r.Name,
                    Author = r.Author,
                    Date = r.Date,
                    ReportType = r.ReportType.Name
                })
                .Where(r => r.ID == id)
                .SingleOrDefaultAsync();

            ViewBag.ReportTypes = await _context.ReportTypes.ToListAsync();

            if (report == null)
            {
                return NotFound();
            }
            return View(report);
        }

        // POST: Admin/Home/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ReportTypeID,Name,Author,Date")] ReportsDTO reportdto)
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
                    Report editReport = new Report
                    {
                        ID = reportdto.ID,
                        Name = reportdto.Name,
                        Author = reportdto.Author,
                        Date = reportdto.Date,
                        ReportTypeID = reportdto.ReportTypeID,
                        // No need for the admin to be able to update the file contents
                        Content = _context.Reports.Where(r => r.ID == reportdto.ID).Select(r => r.Content).SingleOrDefault()
                    };
                    _context.Update(editReport);
                    await _context.SaveChangesAsync();
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports
                .FirstOrDefaultAsync(m => m.ID == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // POST: Admin/Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            _context.Reports.Remove(report);
            try
            {
                await _context.SaveChangesAsync();
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
            return _context.Reports.Any(e => e.ID == id);
        }
    }
}
