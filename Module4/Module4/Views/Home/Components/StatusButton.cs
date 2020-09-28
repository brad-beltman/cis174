using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Module4.Components
{
    public class StatusButton : ViewComponent
    {
        // String to hold our button style
        public string ButtonStyle { get; set; }

        public IViewComponentResult Invoke() => View(ButtonStyle);
    }
}
