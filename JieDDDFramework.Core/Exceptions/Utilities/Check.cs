using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JieDDDFramework.Core.Exceptions.Utilities
{
    [DebuggerStepThrough]
    public static class Check
    {
        public static T NotNull<T>(T value,string parameterName)
        {
            if ((object)value == null)
            {
                Check.NotEmpty(parameterName, nameof(parameterName));
                throw new ArgumentNullException(parameterName);
            }
            return value;
        }
        
        public static IReadOnlyList<T> NotEmpty<T>(IReadOnlyList<T> value, string parameterName)
        {
            Check.NotNull<IReadOnlyList<T>>(value, parameterName);
            if (value.Count == 0)
            {
                Check.NotEmpty(parameterName, nameof(parameterName));
            }
            return value;
        }
        
        public static string NotEmpty(string value, string parameterName)
        {
            Exception exception = (Exception)null;
            if (value == null)
                exception = (Exception)new ArgumentNullException(parameterName);
            else if (value.Trim().Length == 0)
                exception = (Exception)new ArgumentException(parameterName);
            if (exception != null)
            {
                Check.NotEmpty(parameterName, nameof(parameterName));
                throw exception;
            }
            return value;
        }

        public static string NullButNotEmpty(string value,string parameterName)
        {
            if (value != null && value.Length == 0)
            {
                Check.NotEmpty(parameterName, nameof(parameterName));
                throw new ArgumentException(parameterName);
            }
            return value;
        }

        public static IReadOnlyList<T> HasNoNulls<T>(IReadOnlyList<T> value, string parameterName) where T : class
        {
            NotNull(value, parameterName);
            if (value.Any<T>(e => e == null))
            {
                Check.NotEmpty(parameterName, nameof(parameterName));
                throw new ArgumentException(parameterName);
            }
            return value;
        }

        public static T IsDefined<T>(T value, string parameterName) where T : struct
        {
            if (!Enum.IsDefined(typeof(T), (object)value))
            {
                Check.NotEmpty(parameterName, nameof(parameterName));
                throw new ArgumentException(parameterName);
            }
            return value;
        }

        public static Type ValidEntityType(Type value, string parameterName)
        {
            if (!value.GetTypeInfo().IsClass)
            {
                Check.NotEmpty(parameterName, nameof(parameterName));
                throw new ArgumentException(parameterName);
            }
            return value;
        }
    }
}
