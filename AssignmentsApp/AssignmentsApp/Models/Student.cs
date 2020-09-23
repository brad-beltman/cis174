using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentsApp.Models
{
    public class Student
    {
        [Required(ErrorMessage = "Please include a first name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please include a last name")]
        public string LastName { get; set; }

        public string Grade { get; set; }
    }
}
