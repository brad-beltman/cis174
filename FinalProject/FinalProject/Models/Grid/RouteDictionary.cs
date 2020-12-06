using FinalProject.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Models.ExtensionMethods;

namespace FinalProject.Models.Grid
{
    public class RouteDictionary : Dictionary<string, string>
    {
        public int PageNumber
        {
            get => Get(nameof(GridDTO.PageNumber)).ToInt();
            set => this[nameof(GridDTO.PageNumber)] = value.ToString();
        }

        public int PageSize
        {
            get => Get(nameof(GridDTO.PageSize)).ToInt();
            set => this[nameof(GridDTO.PageSize)] = value.ToString();
        }

        public string SortField
        {
            get => Get(nameof(GridDTO.SortField));
            set => this[nameof(GridDTO.SortField)] = value;
        }

        public string SortDirection
        {
            get => Get(nameof(GridDTO.SortDirection));
            set => this[nameof(GridDTO.SortDirection)] = value;
        }

        private string Get(string key) => Keys.Contains(key) ? this[key] : null;

        public void SetSortAndDirection(string fieldName, RouteDictionary current)
        {
            this[nameof(GridDTO.SortField)] = fieldName;

            if (current.SortField.EqualsNoCase(fieldName) && current.SortDirection == "asc")
            {
                this[nameof(GridDTO.SortDirection)] = "desc";
            }
            else
            {
                this[nameof(GridDTO.SortDirection)] = "asc";
            }
        }

        public RouteDictionary Clone()
        {
            var clone = new RouteDictionary();
            foreach (var key in Keys)
            {
                clone.Add(key, this[key]);
            }
            return clone;
        }

        public string ReportTypeFilter
        {
            get
            {
                string s = Get(nameof(ReportsGridDTO.ReportType))?.Replace(
                    FilterPrefix.ReportType, "");
                int index = s?.IndexOf('-') ?? -1;
                return (index == -1) ? s : s.Substring(0, index);
            }
            set => this[nameof(ReportsGridDTO.ReportType)] = value;
        }

        public string SearchString
        {
            get
            {
                string s = Get(nameof(ReportsGridDTO.SearchString))?.Replace(
                    FilterPrefix.SearchString, "");
                int index = s?.IndexOf('-') ?? -1;
                return (index == -1) ? s : s.Substring(0, index);
            }
            set => this[nameof(ReportsGridDTO.SearchString)] = value;
        }

        public void ClearFilters()
        {
            ReportTypeFilter = ReportsGridDTO.DefaultFilter;
            SearchString = null;
        }
    }
}
