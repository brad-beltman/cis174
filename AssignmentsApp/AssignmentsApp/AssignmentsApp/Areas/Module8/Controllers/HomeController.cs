using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AssignmentsApp.Areas.Module8.Data;
using AssignmentsApp.Areas.Module8.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AssignmentsApp.Areas.Module8.Controllers
{
    [Area("Module8")]
    public class HomeController : Controller
    {
        private Module8Context context;
        public HomeController(Module8Context ctx) => context = ctx;

        public ViewResult Index(string id)
        {
            var filters = new Filters(id);
            ViewBag.Filters = filters;
            ViewBag.Statuses = context.Statuses.ToList();

            IQueryable<Ticket> query = context.Tickets
                .Include(t => t.Status);
            if (filters.HasStatus)
            {
                query = query.Where(t => t.StatusID == filters.StatusID);
            }
            var tasks = query.OrderBy(t => t.StatusID).ToList();
            return View(tasks);
        }

        [HttpGet]
        public ViewResult Add()
        {
            ViewBag.Statuses = context.Statuses.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Add(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                context.Tickets.Add(ticket);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Statuses = context.Statuses.ToList();
                return View(ticket);
            }
        }

        [HttpPost]
        public RedirectToActionResult Filter(string filter)
        {
            return RedirectToAction("Index", new { ID = filter });
        }

        [HttpPost]
        public RedirectToActionResult Edit([FromRoute] string id, Ticket selected)
        {
            if (selected.StatusID == null)
            {
                context.Tickets.Remove(selected);
            }
            else
            {
                string newStatusID = selected.StatusID;
                selected = context.Tickets.Find(selected.TicketID);
                selected.StatusID = newStatusID;
                context.Tickets.Update(selected);
            }
            context.SaveChanges();

            return RedirectToAction("Index", new { ID = id });
        }
    }
}
