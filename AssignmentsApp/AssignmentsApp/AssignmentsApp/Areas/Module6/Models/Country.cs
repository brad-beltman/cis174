using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentsApp.Areas.Module6.Models
{
    public class Country
    {
        public Country()
        {

        }

        public Country(string name, string abbr, string game, string sport, string category)
        {
            Name = name;
            Abbr = abbr;
            Game = game;
            Sport = sport;
            Category = category;
        }

        public string Name { get; set; }

        public string Abbr { get; set; }

        public string Game { get; set; }

        public string Sport { get; set; }

        public string Category { get; set; }

        public static List<Country> GetCountries()
        {
            List<Country> Countries = new List<Country>();

            // Create an object for each country
            Countries.Add(new Country("Canada", "CA", "Winter Olympics", "Curling", "Indoor"));
            Countries.Add(new Country("Sweden", "SE", "Winter Olympics", "Curling", "Indoor"));
            Countries.Add(new Country("Great Britain", "GB", "Winter Olympics", "Curling", "Indoor"));
            Countries.Add(new Country("Jamaica", "JM", "Winter Olympics", "Bobsleigh", "Outdoor"));
            Countries.Add(new Country("Italy", "IT", "Winter Olympics", "Bobsleigh", "Outdoor"));
            Countries.Add(new Country("Japan", "JP", "Winter Olympics", "Bobsleigh", "Outdoor"));
            Countries.Add(new Country("Germany", "DE", "Summer Olympics", "Diving", "Indoor"));
            Countries.Add(new Country("China", "CN", "Summer Olympics", "Diving", "Indoor"));
            Countries.Add(new Country("Mexico", "MX", "Summer Olympics", "Diving", "Indoor"));
            Countries.Add(new Country("Brazil", "BR", "Summer Olympics", "Road Cycling", "Outdoor"));
            Countries.Add(new Country("Netherlands", "NL", "Summer Olympics", "Road Cycling", "Outdoor"));
            Countries.Add(new Country("United States", "US", "Summer Olympics", "Road Cycling", "Outdoor"));
            Countries.Add(new Country("Thailand", "TH", "Paralympics", "Archery", "Indoor"));
            Countries.Add(new Country("Uruguary", "UY", "Paralympics", "Archery", "Indoor"));
            Countries.Add(new Country("Ukraine", "UA", "Paralympics", "Archery", "Indoor"));
            Countries.Add(new Country("Austria", "AT", "Paralympics", "Canoe Sprint", "Outdoor"));
            Countries.Add(new Country("Pakistan", "PK", "Paralympics", "Canoe Sprint", "Outdoor"));
            Countries.Add(new Country("Zimbabwe", "ZW", "Paralympics", "Canoe Sprint", "Outdoor"));
            Countries.Add(new Country("France", "FR", "Youth Olympic Games", "Breakdancing", "Indoor"));
            Countries.Add(new Country("Cyprus", "CY", "Youth Olympic Games", "Breakdancing", "Indoor"));
            Countries.Add(new Country("Russia", "RU", "Youth Olympic Games", "Breakdancing", "Indoor"));
            Countries.Add(new Country("Finland", "FI", "Youth Olympic Games", "Skateboarding", "Outdoor"));
            Countries.Add(new Country("Slovakia", "SK", "Youth Olympic Games", "Skateboarding", "Outdoor"));
            Countries.Add(new Country("Portugal", "PT", "Youth Olympic Games", "Skateboarding", "Outdoor"));

            return Countries.OrderBy(c => c.Name).ToList();
        }
    }
}
