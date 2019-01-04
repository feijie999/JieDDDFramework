using System;
using System.Collections.Generic;
using System.Text;
using JieDDDFramework.Service.Dtos;

namespace JieDDDFramework.Service.Operations
{
    /// <summary>
    /// 获取指定标识实体
    /// </summary>
    public interface IGetById<TDto> where TDto : IResponse, new()
    {
        /// <summary>
        /// 通过编号获取
        /// </summary>
        /// <param name="id">实体编号</param>
        TDto GetById(object id);
        /// <summary>
        /// 通过编号列表获取
        /// </summary>
        List<TDto> GetByIds(List<object> ids);
    }
}
