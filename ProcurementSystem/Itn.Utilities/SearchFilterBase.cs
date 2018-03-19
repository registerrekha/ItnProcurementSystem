using System.Collections.Specialized;
using System.Linq;

namespace Itn.Utilities
{
    public abstract class SearchFilterBase
    {
        public string MaxRecords { get; set; }
        public string ModifiedAfter { get; set; }

        public abstract NameValueCollection ToNameValueCollection();

        public bool HasKey(string key)
        {
            return (!string.IsNullOrEmpty(GetValue(key)));
        }

        public string GetValue(string key)
        {
            return ToNameValueCollection()[key.ToLower()] ?? string.Empty;
        }

        public bool HasOneOf(string key, string[] potentialValues)
        {
            // check for "all" which is a special case. It is true if they key contains the string "all" OR the key is empty.
            if (potentialValues.Contains("all"))
            {
                if ((!HasKey(key)) || (GetValue(key) == "all")) return true;
            }
            return HasKey(key) && potentialValues.Contains(GetValue(key));
        }

        public NameValueCollection RemoveKey(string key)
        {
            var result = ToNameValueCollection();
            result.Remove(key.ToLower());
            return result;
        }

        public NameValueCollection Replace(string key, string value)
        {
            var result = ToNameValueCollection();
            result.Remove(key.ToLower());
            result.Add(key.ToLower(), value);
            return result;
        }


    }
}
