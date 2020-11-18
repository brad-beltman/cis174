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
        public Expression<Func<T, bool>> Where { get; set; }
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

        // read-only properties
        public bool HasWhere => Where != null;
        public bool HasOrderBy => OrderBy != null;
        public bool HasPaging => PageNumber > 0 && PageSize > 0;
    }
}
