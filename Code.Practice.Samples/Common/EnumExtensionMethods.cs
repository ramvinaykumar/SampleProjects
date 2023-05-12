using System;
using System.ComponentModel;

namespace Code.Practice.Samples.Common
{
    public static class EnumExtensionMethods
    {
        public static string GetEnumDescription(this Enum enumValue)
        {
            DescriptionAttribute[] array = (DescriptionAttribute[])enumValue.GetType().GetField(enumValue.ToString())!.GetCustomAttributes(typeof(DescriptionAttribute), inherit: false);
            if (array.Length == 0)
            {
                return enumValue.ToString();
            }

            return array[0].Description;
        }

        public static int GetEnumCode(this Enum enumValue)
        {
            DescriptionWithValueAttribute[] array = (DescriptionWithValueAttribute[])enumValue.GetType().GetField(enumValue.ToString())!.GetCustomAttributes(typeof(DescriptionWithValueAttribute), inherit: false);
            if (array.Length == 0)
            {
                return int.Parse(enumValue.ToString());
            }

            return array[0].Value;
        }

        public static string GetEnumStringCode(this Enum enumValue)
        {
            DescriptionWithStringAttribute[] array = (DescriptionWithStringAttribute[])enumValue.GetType().GetField(enumValue.ToString())!.GetCustomAttributes(typeof(DescriptionWithStringAttribute), inherit: false);
            if (array.Length == 0)
            {
                return enumValue.ToString();
            }

            return array[0].Value.ToString();
        }
    }
}
