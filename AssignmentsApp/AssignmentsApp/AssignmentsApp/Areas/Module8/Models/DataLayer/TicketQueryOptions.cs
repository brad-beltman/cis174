using AssignmentsApp.Areas.Module8.Models.ExtensionMethods;
using AssignmentsApp.Areas.Module8.Models.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentsApp.Areas.Module8.Models.DataLayer
{
    public class TicketQueryOptions : QueryOptions<Ticket>
    {
        public void SortFilter(TicketsGridBuilder builder)
        {
            if (builder.IsFilterByStatus)
            {
                Where = t => t.StatusID == builder.CurrentRoute.StatusFilter;
            }
            if (builder.IsFilterBySprint)
            {
                Where = t => t.SprintNumber.ToString() == builder.CurrentRoute.SprintFilter;
            }
            if (builder.IsFilterByTicket)
            {
                Where = t => t.Name == builder.CurrentRoute.TicketFilter;
            }

            if (builder.IsSortByStatus)
            {
                OrderBy = t => t.Status.Name;
            }
            else if (builder.IsSortBySprint)
            {
                OrderBy = t => t.SprintNumber;
            }
            else
            {
                OrderBy = t => t.Name;
            }
        }
    }
}
