using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssignmentsApp.Areas.Module6.Models;
using Microsoft.AspNetCore.Http;

namespace AssignmentsApp.Areas.Module6.Session
{
    public class FavoriteCookies
    {
        private const string Favorites = "favorites";
        private const string Delimiter = "-";

        private IRequestCookieCollection requestCookies { get; set; }
        private IResponseCookies responseCookies { get; set; }

        public FavoriteCookies(IRequestCookieCollection cookies)
        {
            requestCookies = cookies;
        }
        public FavoriteCookies(IResponseCookies cookies)
        {
            responseCookies = cookies;
        }

        public void SetFavoriteIds(List<Country> mycountries)
        {
            List<string> ids = mycountries.Select(c => c.Name).ToList();
            string idsString = String.Join(Delimiter, ids);
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(365)
            };
            RemoveMyTeamIds();  // Deletes old cookies
            responseCookies.Append(Favorites, idsString, options);
        }

        public string[] GetMyFavorites()
        {
            string cookie = requestCookies[Favorites];
            if (string.IsNullOrEmpty(cookie))
                return new string[] { };
            else
                return cookie.Split(Delimiter);
        }

        public void RemoveMyTeamIds()
        {
            responseCookies.Delete(Favorites);
        }
    }
}
