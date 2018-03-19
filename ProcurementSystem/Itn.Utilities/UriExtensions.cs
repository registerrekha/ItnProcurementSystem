using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace Itn.Utilities
{
   public static class UriExtensions
    {
        public static string ToQueryString(this NameValueCollection nvc)
        {
            if (nvc == null) return "";
            var array = (from key in nvc.AllKeys
                    from value in nvc.GetValues(key)
                    select string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value)))
                .ToArray();
            return "?" + string.Join("&", array);
        }

        public static string ToQueryString(this string[] queryStringElements)
        {
            var sb = new StringBuilder();
            foreach (var el in queryStringElements)
            {
                // strip any clauses of ?. We'll handle that ourselves
                var queryClause = el.Replace("?", "");
                if ((sb.Length > 0) && (!string.IsNullOrEmpty(queryClause)))
                {
                    sb.Append("&");
                }
                if (!string.IsNullOrEmpty(queryClause))
                {
                    sb.Append(queryClause);
                }
            }
            if (sb.Length > 0) sb.Insert(0, "?");
            return sb.ToString();
        }

    }
}
