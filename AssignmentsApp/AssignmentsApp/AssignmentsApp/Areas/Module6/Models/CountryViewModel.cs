using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentsApp.Areas.Module6.Models
{
    public class CountryViewModel : Country
    {
        public CountryViewModel()
        {

        }

        public string ActiveGame { get; set; } = "all";

        public string ActiveCategory { get; set; } = "all";
    }
}
