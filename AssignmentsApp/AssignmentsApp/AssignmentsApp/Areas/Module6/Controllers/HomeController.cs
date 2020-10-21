using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssignmentsApp.Areas.Module6.Data;
using AssignmentsApp.Areas.Module6.Models;
using AssignmentsApp.Areas.Module6.Session;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentsApp.Areas.Module6.Controllers
{
    [Area("Module6")]
    public class HomeController : Controller
    {
        private Module6Context context { get; set; }

        public HomeController(Module6Context ctx)
        {
            context = ctx;
        }

        public ViewResult Index(CountryListViewModel model)
        {
            List<Country> Countries = context.Countries.OrderBy(c => c.Name).ToList();
            List<string> games = new List<string>();
            List<string> categories = new List<string>();

            // Make a collection of all games and catgories in use
            foreach (Country country in Countries)
            {
                // Dynamically collect all possible games
                if (!games.Contains(country.Game))
                {
                    games.Add(country.Game);
                }

                // Dynamically collect all possible categories
                if (!categories.Contains(country.Category))
                {
                    categories.Add(country.Category);
                }
            }

            var session = new OlympicSession(HttpContext.Session);

            // If no count in session, get cookie and restore favorite teams in session
            int? count = session.GetMyCountriesCount();
            if (count == null)
            {
                var cookies = new FavoriteCookies(Request.Cookies);
                string[] ids = cookies.GetMyFavorites();

                List<Country> mycountries = new List<Country>();
                if (ids.Length > 0)
                {
                    foreach(string id in ids)
                    {
                        mycountries.Add(context.Countries.Where(c => c.Name == id).FirstOrDefault());
                    }
                }
                session.SetMyFavs(mycountries);
            }

            model.Countries = Countries;
            model.Games = games;
            model.Categories = categories;
            model.ActiveGame = RouteData.Values?["ActiveGame"]?.ToString();
            model.ActiveCategory = RouteData.Values?["ActiveCategory"]?.ToString();

            return View(model);
        }

        [HttpGet]
        [Route("[area]/[controller]/[action]/{id}")]
        public ViewResult Details(int id)
        {
            var session = new OlympicSession(HttpContext.Session);
            CountryViewModel model = new CountryViewModel();

            var country = context.Countries.Where(c => c.CountryID == id).FirstOrDefault();
            model.Name = country.Name;
            model.Abbr = country.Abbr;
            model.Game = country.Game;
            model.Sport = country.Sport;
            model.Category = country.Category;
            model.ActiveGame = session.GetActiveGame();
            model.ActiveCategory = session.GetActiveCategory();
            
            return View(model);
        }

        [HttpPost]
        [Route("[area]/[controller]/[action]")]
        public RedirectToActionResult Add(CountryViewModel model)
        {
            var country = context.Countries.Where(c => c.CountryID == model.CountryID).FirstOrDefault();
            model.Name = country.Name;
            model.Abbr = country.Abbr;
            model.Game = country.Game;
            model.Sport = country.Sport;
            model.Category = country.Category;

            var session = new OlympicSession(HttpContext.Session);

            if (ModelState.IsValid)
            {
                var favs = session.GetMyFavs();
                favs.Add(country);
                session.SetMyFavs(favs);

                var cookies = new FavoriteCookies(Response.Cookies);
                cookies.SetFavoriteIds(favs);

                TempData["message"] = $"{country.Name} added to favorites";

                return RedirectToAction("Index", new
                {
                    ActiveGame = session.GetActiveGame(),
                    ActiveCategory = session.GetActiveCategory()
                });
            }
            else
            {
                TempData["message"] = $"There was an error adding {country.Name} to favorites";
                return RedirectToAction("Index", new
                {
                    ActiveGame = session.GetActiveGame(),
                    ActiveCategory = session.GetActiveCategory()
                });
            }
        }
    }
}
