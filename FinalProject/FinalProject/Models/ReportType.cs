using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class ReportType
    {
        public int ReportTypeID { get; set; }

        public string Name { get; set; }

        public ICollection<Report> Reports { get; set; }  // Navigation property
    }
}
