using System;
using System.Collections.Generic;
using System.Text;
using JieDDDFramework.Core.Models;
using JieDDDFramework.Service.Dtos;

namespace JieDDDFramework.Service.Operations
{
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <typeparam name="TDto">数据传输对象类型</typeparam>
    /// <typeparam name="TQueryParameter">查询参数类型</typeparam>
    public interface IPageQuery<TDto, in TQueryParameter>
        where TDto : IResponse, new()
        where TQueryParameter : IPagerQueryParameter
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="parameter">查询参数</param>
        List<TDto> Query(TQueryParameter parameter);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="parameter">查询参数</param>
        IPagedList<TDto> PagerQuery(TQueryParameter parameter);
    }
}
