using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentsApp.Areas.Module6.Models
{
    public class CountryListViewModel
    {
        public List<Country> Countries { get; set; }

        public string ActiveGame { get; set; } = "all";

        public string ActiveCategory { get; set; } = "all";

        private List<string> games;
        public List<string> Games
        {
            get => games;
            set
            {
                games = value;
                games.Insert(0, "All");
            }
        }

        private List<string> categories;
        public List<string> Categories
        {
            get => categories;
            set
            {
                categories = value;
                categories.Insert(0, "All");
            }
        }

        // methods to help determine the active link
        public string CheckActiveGame(string g) => g.ToLower() == ActiveGame.ToLower() ? "active" : "";

        public string CheckActiveCategory(string c) => c.ToLower() == ActiveCategory.ToLower() ? "active" : "";
    }
}
