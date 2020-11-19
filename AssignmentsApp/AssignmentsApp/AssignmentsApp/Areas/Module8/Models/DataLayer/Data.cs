using AssignmentsApp.Areas.Module8.Data;
using AssignmentsApp.Areas.Module8.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AssignmentsApp.Areas.Module8.Models.DataLayer
{
    public class Data
    {
        private Module8Context context { get; set; }
        public Data(Module8Context ctx) => context = ctx;

        public IEnumerable<Ticket> GetTickets(QueryOptions<Ticket> options)
        {
            IQueryable<Ticket> query = context.Tickets;
            foreach (string include in options.GetIncludes())
            {
                if (options.HasWhere)
                    query = query.Where(options.Where);
                if (options.HasOrderBy)
                    query = query.OrderBy(options.OrderBy);
                if (options.HasPaging)
                    query = query.PageBy(options.PageNumber, options.PageSize);
                return query.ToList();
            }
            return query;
        }

        public IEnumerable<Ticket> GetPageOfTickets(int pageNumber, int pageSize)
        {
            return context.Tickets.PageBy(pageNumber, pageSize).ToList();
        }

        public IEnumerable<Ticket> GetSortedFilteredTickets(Expression<Func<Ticket, bool>> where,
            Expression<Func<Ticket, Object>> orderby)
        {
            return context.Tickets
                .Where(where)
                .OrderBy(orderby)
                .ToList();
        }
    }
}
