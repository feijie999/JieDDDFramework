using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JieDDDFramework.Core.Models
{
    public class PagedList<T> : IPagedList<T>
    {
        public List<T> Rows { get; set; }

        public PagedList()
        {
            Rows = new List<T>();
        }

        public PagedList(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var total = source.Count();
            TotalCount = total;
            TotalPages = total / pageSize;

            if (total % pageSize > 0)
                TotalPages++;

            PageSize = pageSize;
            PageIndex = pageIndex;
            Rows = new List<T>();
            Rows.AddRange(source.Skip(pageIndex * pageSize).Take(pageSize).ToList());
        }

        public PagedList(IList<T> source, int pageIndex, int pageSize)
        {
            TotalCount = source.Count();
            TotalPages = TotalCount / pageSize;

            if (TotalCount % pageSize > 0)
                TotalPages++;

            PageSize = pageSize;
            PageIndex = pageIndex;
            Rows = new List<T>();
            Rows.AddRange(source.Skip(pageIndex * pageSize).Take(pageSize).ToList());
        }

        public PagedList(IEnumerable<T> source, int pageIndex, int pageSize, int totalCount)
        {
            TotalCount = totalCount;
            TotalPages = TotalCount / pageSize;

            if (TotalCount % pageSize > 0)
                TotalPages++;

            PageSize = pageSize;
            PageIndex = pageIndex;
            Rows = new List<T>();
            Rows.AddRange(source);
        }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public int TotalPages { get; set; }

        public bool HasPreviousPage => (PageIndex > 0);

        public bool HasNextPage => (PageIndex + 1 < TotalPages);
    }

    public static class PagedListExtensions
    {
        public static IPagedList<T> ToPageResult<T>(this IQueryable<T> query, int pageIndex, int pageSize, bool findTotalCount = true)
        {
            var pageResult = new PagedList<T>();
            if (findTotalCount)
            {
                var totalCount = query.Count();
                var totalPages = totalCount / pageSize;
                pageResult.TotalPages = totalCount % pageSize == 0 ? totalPages : totalPages + 1;
                pageResult.TotalCount = totalCount;
            }
            pageResult.Rows = query.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            pageResult.PageIndex = pageIndex;
            pageResult.PageSize = pageSize;
            return pageResult;
        }
    }
}
