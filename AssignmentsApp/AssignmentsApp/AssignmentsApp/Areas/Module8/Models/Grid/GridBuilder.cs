using AssignmentsApp.Areas.Module6.Session;
using AssignmentsApp.Areas.Module8.Models.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentsApp.Areas.Module8.Models.Grid
{
    public class GridBuilder
    {
        private const string RouteKey = "currentroute";

        protected RouteDictionary routes { get; set; }
        private ISession session { get; set; }

        // When just the route data is needed from the session
        public GridBuilder(ISession sess)
        {
            session = sess;
            routes = session.GetObject<RouteDictionary>(RouteKey) ?? new RouteDictionary();
        }

        // Used when storing the paging-sorting route segments is necessary
        public GridBuilder(ISession sess, GridDTO values, string defaultSortField)
        {
            session = sess;

            routes = new RouteDictionary();  //Clears the previous route segment values
            routes.PageNumber = values.PageNumber;
            routes.PageSize = values.PageSize;
            routes.SortField = values.SortField ?? defaultSortField;
            routes.SortDirection = values.SortDirection;

            SaveRouteSegments();
        }

        public void SaveRouteSegments() => session.SetObject<RouteDictionary>(RouteKey, routes);

        public int GetTotalPages(int count)
        {
            int size = routes.PageSize;
            return (count + size - 1) / size;
        }

        public RouteDictionary CurrentRoute => routes;
    }
}
