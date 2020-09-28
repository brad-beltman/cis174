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
            return View();
        }

        [Route("[area]/[controller]/[action]")]
        public IActionResult Attribute()
        {
            return View();
        }
    }
}
