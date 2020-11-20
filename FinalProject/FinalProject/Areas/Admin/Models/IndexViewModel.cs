using FinalProject.Areas.Admin.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Models;

namespace FinalProject.Areas.Admin.Models
{
    public class IndexViewModel : Report
    {
        public IEnumerable<ReportsDTO> Reports { get; set; }
    }
}
