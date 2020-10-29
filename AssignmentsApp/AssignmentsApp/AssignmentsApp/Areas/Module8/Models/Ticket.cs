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
        [StringLength(50)]
        [RegularExpression("[a-zA-Z0-9]", ErrorMessage = "Only alpha numberic characters are allowed")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a description")]
        [StringLength(500)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please select a sprint number")]
        [Range(1, 100, ErrorMessage = "The valid range is between 1 and 100")]
        public int SprintNumber { get; set; }

        [Required(ErrorMessage = "Please set a point value")]
        [Range(1, 100, ErrorMessage = "The valid range is between 1 and 100")]
        public int PointValue { get; set; }

        [Required(ErrorMessage = "Please select a status")]
        public string StatusID { get; set; }
        public Status Status { get; set; }
    }
}
