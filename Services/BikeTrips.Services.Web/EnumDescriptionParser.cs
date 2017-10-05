using System;
using System.ComponentModel;
using System.Reflection;

namespace BikeTrips.Services.Web
{
    public static class EnumDescriptionParser
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            object[] attrs = field.GetCustomAttributes(typeof(DescriptionAttribute), true);
            if (attrs.Length > 0)
            {
                return ((DescriptionAttribute)attrs[0]).Description;
            }
            return string.Empty;
        }
    }
}
