using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace JieDDDFramework.Core.Models
{
    public static class IQueryablePageListExtensions
    {
        public static async Task<IPagedList<T>> ToPageResult<T>(this IQueryable<T> query, int pageIndex, int pageSize, bool findTotalCount = true, CancellationToken cancellationToken = default(CancellationToken))
        {
            var pageResult = new PagedList<T>();
            if (findTotalCount)
            {
                var totalCount = await query.CountAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
                var totalPages = totalCount / pageSize;
                pageResult.TotalPages = totalCount % pageSize == 0 ? totalPages : totalPages + 1;
                pageResult.TotalCount = totalCount;
            }

            pageResult.Rows = await query.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
            pageResult.PageIndex = pageIndex;
            pageResult.PageSize = pageSize;
            return pageResult;
        }
    }
}
