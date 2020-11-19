using AssignmentsApp.Areas.Module8.Models.DTOs;
using AssignmentsApp.Areas.Module8.Models.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentsApp.Areas.Module8.Models.Grid
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
                this[nameof(GridDTO.SortDirection)] = "desc";
            else
                this[nameof(GridDTO.SortDirection)] = "asc";
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

        public string TicketFilter
        {
            get => Get(nameof(TicketsGridDTO.Ticket))?.Replace(FilterPrefix.Ticket, "");
            set => this[nameof(TicketsGridDTO.Ticket)] = value;
        }

        public string StatusFilter
        {
            get => Get(nameof(TicketsGridDTO.Status))?.Replace(FilterPrefix.Status, "");
            set => this[nameof(TicketsGridDTO.Status)] = value;
        }

        public string SprintFilter
        {
            get => Get(nameof(TicketsGridDTO.Sprint))?.Replace(FilterPrefix.Sprint, "");
            set => this[nameof(TicketsGridDTO.Sprint)] = value;
        }

        public void ClearFilters() => TicketFilter = StatusFilter = SprintFilter = TicketsGridDTO.DefaultFilter;
    }
}
