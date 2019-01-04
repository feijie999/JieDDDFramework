using System;
using System.Collections.Generic;
using System.Text;
using JieDDDFramework.Service.Dtos;

namespace JieDDDFramework.Service.Operations
{
    /// <summary>
    /// 获取全部数据
    /// </summary>
    public interface IGetAll<TDto> where TDto : IResponse, new()
    {
        /// <summary>
        /// 获取全部
        /// </summary>
        List<TDto> GetAll();
    }
}
