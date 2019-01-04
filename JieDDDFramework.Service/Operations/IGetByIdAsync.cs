using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JieDDDFramework.Service.Dtos;

namespace JieDDDFramework.Service.Operations
{
    /// <summary>
    /// 获取指定标识实体
    /// </summary>
    public interface IGetByIdAsync<TDto> where TDto : IResponse, new()
    {
        /// <summary>
        /// 通过编号获取
        /// </summary>
        /// <param name="id">实体编号</param>
        Task<TDto> GetByIdAsync(object id);
        /// <summary>
        /// 通过编号列表获取
        /// </summary>
        Task<List<TDto>> GetByIdsAsync(List<object> ids);
    }
}
