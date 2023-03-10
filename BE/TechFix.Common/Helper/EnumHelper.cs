using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using TechFix.Common.Enums;

namespace TechFix.Common.Helper
{
    public static class EnumHelper
    {
        public static TAttribute GetAttribute<TAttribute>(this Enum value)
            where TAttribute : Attribute
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            return type.GetField(name) // I prefer to get attributes this way
                .GetCustomAttributes(false)
                .OfType<TAttribute>()
                .SingleOrDefault();
        }
    }
}
