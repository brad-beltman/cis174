using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentsApp.Areas.Module8.Models
{
    public class Ticket
    {
        public int TicketID { get; set; }

        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please select a sprint number")]
        public int SprintNumber { get; set; }

        [Required(ErrorMessage = "Please set a point value")]
        public int PointValue { get; set; }

        [Required(ErrorMessage = "Please select a status")]
        public string StatusID { get; set; }
        public Status Status { get; set; }
    }
}
