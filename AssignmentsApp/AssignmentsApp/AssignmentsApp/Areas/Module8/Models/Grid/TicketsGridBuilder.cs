using AssignmentsApp.Areas.Module8.Models.ExtensionMethods;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentsApp.Areas.Module8.Models.Grid
{
    public class TicketsGridBuilder : GridBuilder
    {
        public TicketsGridBuilder(ISession sess) : base(sess) { }

        public TicketsGridBuilder(ISession sess, TicketsGridDTO values, string defaultSortField) : base(sess, values, defaultSortField)
        {
            bool isInitial = values.Ticket.IndexOf(FilterPrefix.Ticket) == -1;
            routes.TicketFilter = (isInitial) ? FilterPrefix.Ticket + values.Ticket : values.Ticket;
            routes.StatusFilter = (isInitial) ? FilterPrefix.Status + values.Status : values.Status;
            routes.SprintFilter = (isInitial) ? FilterPrefix.Sprint + values.Sprint : values.Sprint;
        }

        public void LoadFilterSegments(string[] filter, Ticket ticket)
        {
            if (ticket == null)
            {
                routes.TicketFilter = FilterPrefix.Ticket + filter[0];
            }
            else
            {
                routes.TicketFilter = FilterPrefix.Ticket + filter[0] + "-" + ticket.Name.Slug();
            }
            routes.StatusFilter = FilterPrefix.Status + filter[1];
            routes.SprintFilter = FilterPrefix.Sprint + filter[2];
        }

        public void ClearFilterSegments() => routes.ClearFilters();

        string defaultFilter = TicketsGridDTO.DefaultFilter;
        public bool IsFilterByTicket => routes.TicketFilter != defaultFilter;
        public bool IsFilterByStatus => routes.StatusFilter != defaultFilter;
        public bool IsFilterBySprint => routes.SprintFilter != defaultFilter;

        public bool IsSortByStatus => routes.SortField.EqualsNoCase(nameof(Status));
        public bool IsSortBySprint => routes.SortField.EqualsNoCase(nameof(Ticket.SprintNumber));
    }
}
