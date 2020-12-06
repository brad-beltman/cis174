using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FinalProject.Models.DTOs
{
    public class ReportsGridDTO : GridDTO
    {
        [JsonIgnore]
        public const string DefaultFilter = "all";

        public string ReportType { get; set; } = DefaultFilter;
        public string SearchString { get; set; } = null;
    }
}
