using FinalProject.Areas.Admin.Models.DTOs;
using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Areas.Admin.Models.ViewModels
{
    public class EditViewModel
    {
        public int ID { get; set; }
        public int ReportTypeID { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<ReportType> ReportTypes { get; set; }
        public ReportsDTO Report { get; set; }
    }
}
