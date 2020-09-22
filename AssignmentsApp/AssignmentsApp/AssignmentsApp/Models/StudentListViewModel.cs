using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentsApp.Models
{
    public class StudentListViewModel
    { 
        public List<Student> Students { get; set; }

        [Range(1, 10, ErrorMessage = "The access level must be between {1} and {2}")]
        public int AccessLevel { get; set; }
    }
}
