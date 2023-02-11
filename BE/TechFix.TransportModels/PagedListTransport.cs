using System.Collections.Generic;
using System.Linq;
using TechFix.Common.Paging;

namespace TechFix.TransportModels
{
	public class PagedListTransport<T> 
	{
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
		public int PageSize { get; set; }
		public int TotalCount { get; set; }
		public bool HasPrevious;
		public bool HasNext;
		public List<T> Result;

		public PagedListTransport(PagedList<T> pagedList) 
		{
			CurrentPage = pagedList.CurrentPage;
			TotalPages = pagedList.TotalPages;
			PageSize = pagedList.PageSize;
			TotalCount = pagedList.TotalCount;
			HasPrevious = pagedList.HasPrevious;
			HasNext = pagedList.HasNext;
			Result = pagedList.ToList();
		}
	}
}
