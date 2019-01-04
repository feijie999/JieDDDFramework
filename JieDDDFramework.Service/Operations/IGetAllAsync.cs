using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JieDDDFramework.Service.Dtos;

namespace JieDDDFramework.Service.Operations
{
    /// <summary>
    /// 获取全部数据
    /// </summary>
    public interface IGetAllAsync<TDto> where TDto : IResponse, new()
    {
        /// <summary>
        /// 获取全部
        /// </summary>
        Task<List<TDto>> GetAllAsync();
    }
}
