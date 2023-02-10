
using System;
using TechFix.Common.Constants;

namespace TechFix.Services.Helpers.Paging
{
    public class PagingParams
    {
        private int _pageSize = 10;
        const int MAX_PAGE_SIZE = 50;

        public string SearchField { get; set; } = "";
        public string Comparison { get; set; } = QueryComparison.Contains;
        public string SearchString { get; set; } = "";
        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MAX_PAGE_SIZE) ? MAX_PAGE_SIZE : value;
        }

        public DateTime? FromDate { get; set; } = DateTime.MinValue;
        public DateTime? ToDate { get; set; } = DateTime.MaxValue;
    }
}
