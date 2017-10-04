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
            object[] attрs = field.GetCustomAttributes(typeof(DescriptionAttribute), true);
            if (attрs.Length > 0)
            {
                return ((DescriptionAttribute)attрs[0]).Description;
            }
            return string.Empty;
        }
    }
}
