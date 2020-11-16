using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Areas.Admin.Models
{
    public class EditViewModel : Report
    {
        public List<ReportType> ReportTypes { get; set; }
    }
}
