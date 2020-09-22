using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssignmentsApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentsApp.Controllers
{
    public class Module3Controller : Controller
    {
        public IActionResult Index(int id)
        {
            // zero means no access level given, we still want to process this
            if (id >= 0 && id <= 10)
            {
                Student student1 = new Student();
                student1.FirstName = "Brad";
                student1.LastName = "Beltman";
                student1.Grade = "A";

                Student student2 = new Student();
                student2.FirstName = "George";
                student2.LastName = "Smith";
                student2.Grade = "B";

                Student student3 = new Student();
                student3.FirstName = "Thomas";
                student3.LastName = "Jones";
                student3.Grade = "C";

                Student student4 = new Student();
                student4.FirstName = "James";
                student4.LastName = "Johnson";
                student4.Grade = "D";

                List<Student> students = new List<Student>();
                students.Add(student1);
                students.Add(student2);
                students.Add(student3);
                students.Add(student4);

                var model = new StudentListViewModel
                {
                    Students = students,
                    AccessLevel = id
                };

                return View(model);
            }

            return NotFound();
        }
    }
}
