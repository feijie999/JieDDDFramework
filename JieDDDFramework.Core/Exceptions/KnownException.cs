using System;
using System.Collections.Generic;
using System.Text;

namespace JieDDDFramework.Core.Exceptions
{
    public class KnownException : Exception
    {
        public int ErrorCode { get; set; }

        public KnownException(string message, Exception innerException,int errorCode) : base(message, innerException)
        {
            ErrorCode = errorCode;
        }

        public KnownException(string message,Exception innerException) : base(message,innerException)
        {

        }

        public KnownException(string message,int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public KnownException(string message) : base(message)
        {

        }
    }
}
