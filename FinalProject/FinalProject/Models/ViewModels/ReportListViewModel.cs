using FinalProject.Models.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.ViewModels
{
    public class ReportListViewModel
    {
        public IEnumerable<Report> Reports { get; set; }
        public RouteDictionary CurrentRoute { get; set; }
        public int TotalPages { get; set; }

        // for filter drop-down data
        public IEnumerable<ReportType> ReportTypes { get; set; }

        // data for pagesize drop-down - hardcoded
        public int[] PageSizes => new int[] { 5, 6, 7, 8, 9, 10 };
    }
}
