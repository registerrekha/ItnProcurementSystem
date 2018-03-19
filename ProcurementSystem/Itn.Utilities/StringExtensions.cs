using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Itn.Utilities
{
    public static class StringExtensions
    {
        public static void FilterAppender(this StringBuilder sb, string filterVal, string sqlFldName, string filterFldName, string filterCompSymbol = "=", string sqlPrefix = "AND")
        {

            if (string.IsNullOrEmpty(filterVal)) return;
            if (filterVal.ToLower() == "all") return;
            sb.Append(string.Format(" {0} {1}{2}@{3} ", sqlPrefix, sqlFldName, filterCompSymbol, filterFldName));
        }

        public static string ReplaceFirstInstance(this string text, string search, string replace)
        {
            var r = new Regex(search, RegexOptions.IgnoreCase);
            return r.Replace(text, replace, 1);
        }

        public static NameValueCollection AddIfNotEmpty(this NameValueCollection nmc, string nmcKey, string nmcVal, Dictionary<string, string> valueAliases = null)
        {
            if (string.IsNullOrEmpty(nmcVal)) return nmc;
            if (valueAliases != null)
            {
                if (valueAliases.ContainsKey(nmcVal))
                {
                    nmc.Add(nmcKey, valueAliases[nmcVal]);
                    return nmc;
                }
            }
            nmc.Add(nmcKey, nmcVal);
            return nmc;
        }

        /// <summary>
        /// Concatenate any IEnumerable list to a concatenated string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string ToConcatString<T>(this IEnumerable<T> source, string separator)
        {
            if (source == null)
                throw new ArgumentException("source can not be null.");

            if (string.IsNullOrEmpty(separator))
                throw new ArgumentException(
                    "separator can not be null or empty.");

            // A LINQ query to call ToString on each elements
            // and constructs a string array.
            var array =
                (source.Where(s => !Equals(s, default(T))).Select(s => s.ToString())
                ).ToArray();

            // utilise builtin string.Join to concate elements with
            // customizable separator.
            return string.Join(separator, array);
        }
        public static string Left(this string s, int lenFromLeft)
        {
            if (lenFromLeft == 0 || s.Length == 0) return "";
            if (s.Length <= lenFromLeft) return s;
            return s.Substring(0, lenFromLeft);

        }
        public static List<string> SplitToList(this string splitString, char delimChar)
        {
            return new List<string>(splitString.Split(delimChar));
        }
        public static string RemoveSpecialCharacters(this string sourceString)
        {
            var sb = new StringBuilder();
            foreach (char c in sourceString)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') | (c == '.' || c == '_'))
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
        public static bool ToBool(this string b)
        {
            switch (b.ToLower())
            {
                case ("y"):
                case ("yes"):
                case ("t"):
                case ("true"):
                case ("1"):
                    return true;
                case ("n"):
                case ("no"):
                case ("f"):
                case ("false"):
                case ("0"):
                case (""):
                    return false;
                default:
                    throw new ArgumentException(string.Format("{0} is not a recognized string representation of a boolean value!", b));
            }
        }
    }
}
