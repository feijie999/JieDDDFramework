using System;
using System.Collections.Generic;
using System.Text;

namespace JieDDDFramework.Core.Exceptions
{
    public static class ExceptionExtension
    {
        public static string GetAllMessages(this Exception ex)
        {
            var exception = ex;
            var sb = new StringBuilder();
            while (exception != null)
            {
                sb.AppendLine(exception.Message);
                exception = exception.InnerException;
            }
            return sb.ToString();
        }
    }
}
