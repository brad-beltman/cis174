using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssignmentsApp.Areas.Module6.Models;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentsApp.Areas.Module6.Controllers
{
    [Area("Module6")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            List<Country> Countries = Country.GetCountries();
            List<string> games = new List<string>();
            List<string> categories = new List<string>();

            // Make a collection of all games and catgories in use
            foreach (Country country in Countries)
            {
                // Dynamically collect all possible games
                if (games.Contains(country.Game))
                {
                    // Do nothing, we already have it in the list
                }
                else
                {
                    games.Add(country.Game);
                }

                // Dynamically collect all possible categories
                if (categories.Contains(country.Category))
                {
                    // Do nothing, we already have it in the list
                }
                else
                {
                    categories.Add(country.Category);
                }
            }

            // Create our model to pass to the view
            var model = new CountryListViewModel()
            {
                Countries = Countries,
                Games = games,
                Categories = categories,
                ActiveGame = RouteData.Values?["game"]?.ToString() ?? "all",
                ActiveCategory = RouteData.Values?["category"]?.ToString() ?? "all"
            };

            return View(model);
        }

        [HttpPost]
        public RedirectToActionResult Details(string Name, string? ActiveGame, string? ActiveCategory)
        {
            List<Country> countries = Country.GetCountries();
            CountryViewModel model = new CountryViewModel();

            // Figure out which country we're working with and add it to the model
            foreach (Country country in countries)
            {
                if (country.Name == Name)
                {
                    model.Name = country.Name;
                    model.Abbr = country.Abbr;
                    model.Game = country.Game;
                    model.Sport = country.Sport;
                    model.Category = country.Category;
                }
            }
            if (ActiveGame != null)
            {
                TempData["ActiveGame"] = ActiveGame;
            }
            if (ActiveCategory != null)
            {
                TempData["ActiveCateogry"] = ActiveCategory;
            }
            
            return RedirectToAction("Details", model);
        }

        [HttpGet]
        public ViewResult Details (CountryViewModel model)
        {
            model.ActiveGame = TempData?["ActiveGame"]?.ToString() ?? "all";
            model.ActiveCategory = TempData?["ActiveCategory"]?.ToString() ?? "all";

            return View(model);
        }
    }
}
