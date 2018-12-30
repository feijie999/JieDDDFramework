using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JieDDDFramework.Core.EntitySpecifications;

namespace JieDDDFramework.Data.EntityFramework
{
    public static class EntitySpecificationExtensions
    {
        /// <summary>
        /// 时间区间查询
        /// </summary>
        /// <param name="query"></param>
        /// <param name="createdTimeRange">创建时间区间</param>
        /// <returns></returns>
        public static IQueryable<T> WhereBetweenTime<T>(this IQueryable<T> query, DateTime[] createdTimeRange) where T : class, ICreatedTimeState
        {
            if (createdTimeRange == null || createdTimeRange.Contains(DateTime.MinValue))
                return query;

            var beginTime = createdTimeRange[0];
            var endTime = createdTimeRange[1];

            if (beginTime < endTime)
            {
                endTime = endTime.AddDays(1);
                return query.Where(x => x.CreatedTime >= beginTime && x.CreatedTime <= endTime);
            }
            if (beginTime > endTime)
            {
                beginTime = beginTime.AddDays(1);
                return query.Where(x => x.CreatedTime <= beginTime && x.CreatedTime >= endTime);
            }
            if (beginTime == endTime || createdTimeRange.Length == 1)
            {//查询某天数据
                endTime = endTime.AddDays(1);
                return query.Where(x => x.CreatedTime >= beginTime && x.CreatedTime <= endTime);
            }

            return query;
        }

        public static IQueryable<T> WhereNotDelete<T>(this IQueryable<T> query) where T : class, IDeletedState
        {
            return query.Where(x => x.Deleted == false);
        }

        public static IOrderedQueryable<T> OrderByCreatedTime<T>(this IQueryable<T> query, bool desc = true)
            where T : class, ICreatedTimeState
        {
            return query.OrderByDescending(x => x.CreatedTime);
        }
    }
}
