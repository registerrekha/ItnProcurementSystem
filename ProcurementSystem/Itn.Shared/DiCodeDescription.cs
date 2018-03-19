using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itn.Shared
{
    public class DiCodeDescription
    {
        public string Code { get; set; }
        public string Descript { get; set; }

        public static DiCodeDescription Create(string code, string descript)
        {
            return new DiCodeDescription { Code = code, Descript = descript };
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}", Code, Descript);
        }

        public static List<DiCodeDescription> FromKeyValuePairs(List<KeyValuePair<string, string>> pairs)
        {
            return pairs.Select(pair => Create(pair.Key, pair.Value)).ToList();
        }
    }
}
