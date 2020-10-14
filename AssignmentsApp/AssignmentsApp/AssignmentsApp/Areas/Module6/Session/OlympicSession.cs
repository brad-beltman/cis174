using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssignmentsApp.Areas.Module6.Models;
using Microsoft.AspNetCore.Http;

namespace AssignmentsApp.Areas.Module6.Session
{
    public class OlympicSession
    {
        private const string FavoritesKey = "myfavorites";
        private const string CountKey = "countrycount";
        private const string GameKey = "game";
        private const string CategoryKey = "category";

        private ISession session { get; set; }
        public OlympicSession(ISession session)
        {
            this.session = session;
        }

        public void SetMyFavs(List<Country> countries)
        {
            session.SetObject(FavoritesKey, countries);
            session.SetInt32(CountKey, countries.Count);
        }
        public List<Country> GetMyFavs() => session.GetObject<List<Country>>(FavoritesKey) ?? new List<Country>();
        public int GetMyCountriesCount() => session.GetInt32(CountKey) ?? 0;

        public void SetActiveGame(string activeGame) => session.SetString(GameKey, activeGame);
        public string GetActiveGame() => session.GetString(GameKey);

        public void SetActiveCategory(string activeCategory) => session.SetString(CategoryKey, activeCategory);
        public string GetActiveCategory() => session.GetString(CategoryKey);

        public void RemoveMyFavs()
        {
            session.Remove(FavoritesKey);
            session.Remove(CountKey);
        }

        public int? GetMyFavCounty() => session.GetInt32(CountKey);
    }
}
