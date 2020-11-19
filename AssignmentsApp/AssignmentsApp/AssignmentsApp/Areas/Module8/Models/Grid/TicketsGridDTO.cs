using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssignmentsApp.Areas.Module8.Models.DTOs;
using Newtonsoft.Json;

namespace AssignmentsApp.Areas.Module8.Models.Grid
{
    public class TicketsGridDTO : GridDTO
    {
        [JsonIgnore]
        public const string DefaultFilter = "all";

        public string Ticket { get; set; } = DefaultFilter;
        public string Status { get; set; } = DefaultFilter;
        public string Sprint { get; set; } = DefaultFilter;
    }
}
