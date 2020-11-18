using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using FinalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Data
{
    public class Data
    {
        private DocSearchContext context { get; set; }
        public Data(DocSearchContext ctx) => context = ctx;

        public IEnumerable<Report> GetReports(QueryOptions<Report> options)
        {
            IQueryable<Report> query = context.Reports;
            foreach (string include in options.GetIncludes())
            {
                query = query.Include(include);
            }
            if (options.HasWhere)
            {
                query = query.Where(options.Where);
            }
            if (options.HasOrderBy)
            {
                query = query.OrderBy(options.OrderBy);
            }
            if (options.HasPaging)
            {
                query = query.PageBy(options.PageNumber, options.PageSize);
            }
            return query.ToList();
        }

        public IEnumerable<Report> GetSortedFilteredBooks(Expression<Func<Report, bool>> where,
            Expression<Func<Report, Object>> orderby)
        {
            return context.Reports
                .Where(where)
                .OrderBy(orderby)
                .ToList();
        }
    }
}
