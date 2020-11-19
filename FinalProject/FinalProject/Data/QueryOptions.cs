using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace FinalProject.Data
{
    public class QueryOptions<T>
    {
        // public properties for sorting, filtering, and paging
        public Expression<Func<T, Object>> OrderBy { get; set; }
        public string OrderByDirection { get; set; } = "asc"; // Sort ascending by default
        //public Expression<Func<T, bool>> Where { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        // public write-only property for includes private string array
        private string[] includes;
        public string Includes
        {
            set => includes = value.Replace(" ", "").Split(',');
        }

        // public method returns includes array
        public string[] GetIncludes() => includes ?? new string[0]; // Return empty array if no value

        public WhereClauses<T> WhereClauses { get; set; }
        public Expression<Func<T, bool>> Where
        {
            set
            {
                if (WhereClauses == null)
                {
                    WhereClauses = new WhereClauses<T>();
                }
                WhereClauses.Add(value);
            }
            // Added this because Visual Studio was complaining about the lack of the get accessor
            get { return Where; }
        }

        public SelectClauses<T> SelectClauses { get; set; }
        public Expression<Func<T, bool>> Select
        {
            set
            {
                if (SelectClauses == null)
                {
                    SelectClauses = new SelectClauses<T>();
                }
                SelectClauses.Add(value);
            }
            get { return Select; }
        }

        // read-only properties
        public bool HasWhere => WhereClauses != null;
        public bool HasSelect => SelectClauses != null;
        public bool HasOrderBy => OrderBy != null;
        public bool HasPaging => PageNumber > 0 && PageSize > 0;
    }

    public class WhereClauses<T> : List<Expression<Func<T, bool>>> { }
    public class SelectClauses<T> : List<Expression<Func<T, bool>>> { }
}
