using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AssignmentsApp.Areas.Module6.Session;
using AssignmentsApp.Areas.Module6.Models;

namespace AssignmentsApp.Areas.Module6.Controllers
{
    [Area("Module6")]
    public class Favorites : Controller
    {
        [HttpGet]
        [Route("[Area]/[Controller]/[action]")]
        public ViewResult Index()
        {
            var session = new OlympicSession(HttpContext.Session);
            var model = new CountryListViewModel
            {
                ActiveGame = session.GetActiveGame(),
                ActiveCategory = session.GetActiveCategory(),
                Countries = session.GetMyFavs()
            };
            return View(model);
        }

        [HttpPost]
        [Route("[area]/[controller]/[action]")]
        public RedirectToActionResult Delete()
        {
            var session = new OlympicSession(HttpContext.Session);
            var cookies = new FavoriteCookies(Response.Cookies);

            session.RemoveMyFavs();
            cookies.RemoveMyTeamIds();

            TempData["message"] = "Favorites cleared";

            return RedirectToAction("Index", "Home", new
            {
                ActiveGame = session.GetActiveGame(),
                ActiveCategory = session.GetActiveCategory()
            });
        }
    }
}
