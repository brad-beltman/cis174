using AssignmentsApp.Areas.Module8.Models.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentsApp.Areas.Module8.Models.ViewModels
{
    public class TicketListViewModel
    {
        public IEnumerable<Ticket> Tickets { get; set; }

        public RouteDictionary CurrentRoute { get; set; }

        public int TotalPages { get; set; }
    }
}
