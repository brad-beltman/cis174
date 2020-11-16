using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Areas.Admin.Models.DTOs
{
    public class ReportsDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public string ReportType { get; set; }
        public int ReportTypeID { get; set; }
    }
}
