using System;
using System.Collections.Generic;
using System.Text;

namespace JieDDDFramework.Core.Models
{
    public interface IPagedList<T>
    {
        int PageIndex { get; set; }

        int PageSize { get; set; }

        int TotalCount { get; set; }

        int TotalPages { get; set; }

        bool HasPreviousPage { get; }

        bool HasNextPage { get; }

        List<T> Rows { get; set; }
    }
}
