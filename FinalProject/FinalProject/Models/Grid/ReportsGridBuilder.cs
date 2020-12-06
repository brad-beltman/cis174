using FinalProject.Models.DTOs;
using FinalProject.Models.ExtensionMethods;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.Grid
{
    public class ReportsGridBuilder : GridBuilder
    {
        // this constructor gets route data from session state
        public ReportsGridBuilder(ISession sess) : base(sess) { }

        // this constructor stores filtering route/paging/sorting segments stored by the base constructor
        public ReportsGridBuilder(ISession sess, ReportsGridDTO values, string defaultSortField) : base(sess, values, defaultSortField)
        {
            bool isInitial = values.ReportType.IndexOf(FilterPrefix.ReportType) == -1;
            routes.ReportTypeFilter = (isInitial) ? FilterPrefix.ReportType + values.ReportType : values.ReportType;
            routes.SearchString = (isInitial) ? null : values.SearchString;
        }

        public void LoadFilterSegments(string[] filter, ReportType reportType)
        {
            if (reportType == null)
            {
                routes.ReportTypeFilter = FilterPrefix.ReportType + filter[0];
            }
            else
            {
                routes.ReportTypeFilter = FilterPrefix.ReportType + filter[0] + "-" + reportType.Name.Slug();
            }
        }

        public void SetSearchRoute(string? searchString)
        {
            if (searchString == null)
            {
                routes.SearchString = "none";
            }
            else
            {
                routes.SearchString = searchString;
            }
        }

        public void ClearFilterSegments() => routes.ClearFilters();

        // filter flags
        string defaultFilter = ReportsGridDTO.DefaultFilter;
        public bool IsFilterByReportType => routes.ReportTypeFilter != defaultFilter;

        // sort flags
        public bool IsSortByReportType =>
            routes.SortField.EqualsNoCase(nameof(ReportType.Name));

        public bool IsSortByName =>
            routes.SortField.EqualsNoCase(nameof(Report.Name));

        public bool IsSortByAuthor =>
            routes.SortField.EqualsNoCase(nameof(Report.Author));

        // search flag
        public bool IsSearchString => routes.SearchString != null;
    }
}
