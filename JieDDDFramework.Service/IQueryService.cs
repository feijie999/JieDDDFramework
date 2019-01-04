using System;
using System.Collections.Generic;
using System.Text;
using JieDDDFramework.Core.Models;
using JieDDDFramework.Service.Dtos;
using JieDDDFramework.Service.Operations;

namespace JieDDDFramework.Service
{
    /// <summary>
    /// 查询服务
    /// </summary>
    /// <typeparam name="TDto">数据传输对象类型</typeparam>
    /// <typeparam name="TQueryParameter">查询参数类型</typeparam>
    public interface IQueryService<TDto, in TQueryParameter> : IService,
        IGetById<TDto>, IGetByIdAsync<TDto>,
        IGetAll<TDto>, IGetAllAsync<TDto>,
        IPageQuery<TDto, TQueryParameter>, IPageQueryAsync<TDto, TQueryParameter>
        where TDto : IResponse, new()
        where TQueryParameter : IPagerQueryParameter
    {
    }
}
