using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentsApp.Areas.Module5.Controllers
{
    [Area("Module5")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Custom()
        {
            return Content("This is the custom route for module 5");
        }

        //[Route([Controller]/)]
        public IActionResult Attribute()
        {
            return Content("This is the attribute route for module 5");
        }
    }
}
