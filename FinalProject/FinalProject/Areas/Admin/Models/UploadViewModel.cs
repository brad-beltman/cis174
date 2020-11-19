using FinalProject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Areas.Admin.Models
{
    public class UploadViewModel : Report
    {
        public IEnumerable<ReportType> ReportTypes { get; set; }
    }
}
