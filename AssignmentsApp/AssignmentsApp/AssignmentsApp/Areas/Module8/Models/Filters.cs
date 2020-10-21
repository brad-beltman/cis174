using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentsApp.Areas.Module8.Models
{
    public class Filters
    {
        public Filters(string filterstring)
        {
            StatusID = filterstring ?? "all";
            //string[] filters = FilterString.Split('-');
            //StatusID = filters[0];
            //SprintNumber = filters[1];
            //PointValue = filters[2];

        }
        public string FilterString { get; }
        public string StatusID { get; }
        public string SprintNumber { get; }
        public string PointValue { get; }

        public bool HasStatus => StatusID.ToLower() != "all";
        public bool HasSprint => SprintNumber.ToLower() != "all";
        public bool HasPoint => PointValue.ToLower() != "all";
    }
}
