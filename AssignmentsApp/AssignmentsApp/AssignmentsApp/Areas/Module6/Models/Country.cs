using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentsApp.Areas.Module6.Models
{
    public class Country
    {
        public Country()
        {

        }

        public int CountryID { get; set; }

        public string Name { get; set; }

        public string Abbr { get; set; }

        public string Game { get; set; }

        public string Sport { get; set; }

        public string Category { get; set; }
    }
}
