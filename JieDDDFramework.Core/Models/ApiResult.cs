using System;
using System.Collections.Generic;
using System.Text;

namespace JieDDDFramework.Core.Models
{
    public class ApiResult : Result
    {
        /// <summary>
        /// Code定义规则请参加API文档
        /// </summary>
        public int Code { get; set; }
    }

    public class ApiResult<T> : ApiResult, IResult<T>
    {
        #region Implementation of ICustomResult<T>

        public T Value { get; set; }

        #endregion
    }
}
