using FinalProject.Models;
using FinalProject.Models.ExtensionMethods;
using FinalProject.Models.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Data
{
    public class ReportQueryOptions : QueryOptions<Report>
    {
        public void SortFilter(ReportsGridBuilder builder)
        {
            if (builder.IsFilterByReportType)
            {
                Where = r => r.ReportTypeID == builder.CurrentRoute.ReportTypeFilter.ToInt();
            }

            if (builder.IsSearchString)
            {
                Where = r => r.SearchIndex.Contains(builder.CurrentRoute.SearchString.ToLower());
            }

            if (builder.IsSortByReportType)
            {
                OrderBy = r => r.ReportType.Name;
            }
            else if (builder.IsSortByName)
            {
                OrderBy = r => r.Name;
            }
            else if (builder.IsSortByAuthor)
            {
                OrderBy = r => r.Author;
            }
            else
            {
                OrderBy = r => r.Date;
            }
        }

        //public void SetSearchString(ReportsGridBuilder builder)
        //{
        //    if (builder.IsSearchString)
        //    {
        //        Where = r => r.SearchIndex.Contains(builder.CurrentRoute.SearchString.ToLower());
        //    }
        //}
    }
}
