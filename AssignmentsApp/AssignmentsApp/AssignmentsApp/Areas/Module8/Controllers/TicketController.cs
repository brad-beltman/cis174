using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssignmentsApp.Areas.Module8.Data;
using AssignmentsApp.Areas.Module8.Models;
using AssignmentsApp.Areas.Module8.Models.DataLayer;
using AssignmentsApp.Areas.Module8.Models.DataLayer.Repositories;
using AssignmentsApp.Areas.Module8.Models.DTOs;
using AssignmentsApp.Areas.Module8.Models.ExtensionMethods;
using AssignmentsApp.Areas.Module8.Models.Grid;
using AssignmentsApp.Areas.Module8.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentsApp.Areas.Module8.Controllers
{
    public class TicketController : Controller
    {
        private IRepository<Ticket> data { get; set; }
        public TicketController(IRepository<Ticket> rep) =>
            data = rep;

        public IActionResult Index() => RedirectToAction("List");

        public ViewResult List(GridDTO vals)
        {
            string defaultSort = nameof(Ticket.Name);
            var builder = new GridBuilder(HttpContext.Session, vals, defaultSort);

            var options = new QueryOptions<Ticket>
            {
                Includes = "Status.Name",
                PageNumber = builder.CurrentRoute.PageNumber,
                PageSize = builder.CurrentRoute.PageSize,
                OrderByDirection = builder.CurrentRoute.SortDirection
            };

            if (builder.CurrentRoute.SortField.EqualsNoCase(defaultSort))
                options.OrderBy = t => t.Status;
            else
                options.OrderBy = t => t.Name;

            var vm = new TicketListViewModel
            {
                Tickets = data.List(options),
                CurrentRoute = builder.CurrentRoute,
                TotalPages = builder.GetTotalPages(data.Count)
            };
            return View(vm);
        }
    }
}
