using System;
using System.Collections.Generic;
using System.Text;

namespace JieDDDFramework.Core.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsInherit(this Type type, Type c)
        {
            if (type == c) return true;
            if (c.IsInterface)
            {
                foreach (var @interface in type.GetInterfaces())
                {
                    if (@interface == c) return true;
                    if (@interface.IsGenericType && @interface.GetGenericTypeDefinition() == c) return true;
                    if (IsInherit(@interface,c)) return true;
                }
                return false;
            }
            if (type.BaseType == null) return false;
            if (type.BaseType == c) return true;
            if (type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == c) return true;
            return IsInherit(type.BaseType, c);
        }
    }
}
