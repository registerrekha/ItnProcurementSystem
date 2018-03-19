using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Itn.Utilities
{
    public static class EnumHelper
    {

        public static T ToEnum<T>(string str) where T : struct
        {
            try
            {
                T res = (T)Enum.Parse(typeof(T), str, true);
                if (!Enum.IsDefined(typeof(T), res)) return default(T);
                return res;
            }
            catch
            {
                return default(T);
            }
        }






        public static List<T> ToStringList<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }

        public static bool HasEnumValueFromDescription<T>(string description)
        {
            MemberInfo[] fis = typeof(T).GetFields();
            foreach (var fi in fis)
            {
                var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0 && attributes[0].Description == description)
                    return true;
            }
            return false;
        }

        public static string GetDescriptionFromEnumValue(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            var attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            return value.ToString();
        }

        public static T GetEnumValueFromDescription<T>(string description)
        {
            MemberInfo[] fis = typeof(T).GetFields();
            foreach (var fi in fis)
            {
                var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0 && attributes[0].Description == description)
                    return (T)Enum.Parse(typeof(T), fi.Name);
            }
            throw new ArgumentNullException(string.Format("Unable to find an enum for description {0}", description));
        }


    }
}

