using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AssignmentsApp.Areas.Module8.Models.DataLayer
{
    public class QueryOptions<T>
    {
        public Expression<Func<T, Object>> OrderBy { get; set; }
        public string OrderByDirection { get; set; } = "asc";
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        private string[] includes;
        public string Includes
        {
            set => includes = value.Replace(" ", "").Split(',');
        }
        public string[] GetIncludes() => includes ?? new string[0];

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
            // Only added this because of errors in other parts of the code that there was no accessor
            // This allows me to build the solution
            get { return Where; }
        }

        public bool HasWhere => WhereClauses != null;
        public bool HasOrderBy => OrderBy != null;
        public bool HasPaging => PageNumber > 0 && PageSize > 0;
    }

    public class WhereClauses<T> : List<Expression<Func<T, bool>>> { }
}
