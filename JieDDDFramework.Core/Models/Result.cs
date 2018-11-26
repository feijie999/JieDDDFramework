using System;
using System.Collections.Generic;
using System.Text;

namespace JieDDDFramework.Core.Models
{
    public class Result : IResult
    {
        #region Implementation of ICustomResult

        public bool Success { get; set; }

        public string Message { get; set; }

        public void Succeed()
        {
            Success = true;
        }

        public void Fail()
        {
            Success = false;
        }

        public void Succeed(string message)
        {
            Success = true;
            Message = message;
        }

        public void Fail(string message)
        {
            Success = false;
            Message = message;
        }

        #endregion
    }

    public class Result<T> : Result, IResult<T>
    {
        #region Implementation of ICustomResult<T>

        public T Value { get; set; }

        #endregion
    }
}
