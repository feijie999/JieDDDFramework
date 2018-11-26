using System;
using System.Collections.Generic;
using System.Text;

namespace JieDDDFramework.Core.Models
{
    public interface IResult
    {
        bool Success { get; set; }

        string Message { get; set; }

        void Succeed();

        void Fail();

        void Succeed(string message);

        void Fail(string message);
    }

    public interface IResult<T> : IResult
    {
        T Value { get; set; }
    }
}
