using System;
using System.Collections.Generic;
using TechFix.Common.Constants;

namespace TechFix.Common.Paging
{
    public class PagingParams
    {
        private int _pageSize = 30;

        public List<FilterParam> FilterParams { get; set; }
        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > 100 ? 100 : value;
        }
    }

    public class FilterParam
    {
        public string PropertyName { get; set; } = "";
        public string Comparison { get; set; } = QueryComparison.Equal;
        public string Value { get; set; }
    }
}
